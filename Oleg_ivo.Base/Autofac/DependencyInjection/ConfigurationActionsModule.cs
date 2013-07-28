using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using BI.Base.DependencyInjection;

namespace Oleg_ivo.PrismExtensions.Autofac.DependencyInjection
{

    /// <summary>
    ///  Модуль, позволяет задавать действия, выполняемые при активации компонента,
    ///  раньше, чем будет создан или зарегистрирован сам компонент.
    /// </summary>
    /// <remarks>
    ///  Модуль должен быть зарегистирован на том же уровне иерархии контейнеров или выше, 
    ///  что и действия.
    ///  Действия, зарегистрированные уровнем выше, чем этот модуль, могут не выполняться.
    /// </remarks>
    public class ConfigurationActionsModule : ComponentRegistrationEventExtenderModule
    {
        public ConfigurationActionsModule()  
        {
            ActivatedHandler = (s, e) =>
            {
                var suitableTypes = GetSuitableInstanceTypes(e.Instance.GetType());
                foreach (Type instanceType in suitableTypes)
                {
                    var configAction = (IServiceConfigurationAction) e.Context.ResolveOptionalService(
                        new TypedService(typeof(ServiceConfigurationAction<>).MakeGenericType(instanceType))
                    );
                    if (configAction != null)
                       configAction.ConfigureInstance(e.Component, e.Context, e.Instance);   
                }
             };
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);
            if (registration.Ownership == InstanceOwnership.OwnedByLifetimeScope)
            {
                lock (registeredTypes)
                    registeredTypes.AddRange(
                        from type in (
                            from service in registration.Services.OfType<IServiceWithType>() select service.ServiceType
                        )
                        where type.IsGenericType && 
                              type.GetGenericTypeDefinition() == typeof(ServiceConfigurationAction<>)
                        select type.GetGenericArguments()[0]);   
            }
        }


        private readonly List<Type> registeredTypes = new List<Type>();
        private readonly Dictionary<Type, List<Type>> implementedTypesDictionary = new Dictionary<Type, List<Type>>();


        /// <summary>
        ///  Возвращает типы, из числа тех, к которым приводим <paramref name="instanceType"/>,
        ///  и для которых в данном контейнере были зарегистирированы действия.
        /// </summary>
        /// <param name="instanceType"></param>
        /// <returns></returns>
        private IEnumerable<Type> GetSuitableInstanceTypes(Type instanceType)
        {
            var implementedTypes = GetImplementedTypes(instanceType);
            lock (registeredTypes)
                return
                    implementedTypes
                    .Intersect(registeredTypes)
                    .ToList();
        }

        /// <summary>
        ///  Возвращает типы, реализуемые типом <paramref name="serviceType"/>, в порядке:
        /// <list type="numbered">
        /// <item>все интерфейсы;</item>
        /// <item>базовые типы, от более общих к более конкретным;</item>
        /// <item>собственно <paramref name="serviceType"/>.</item>
        /// </list>
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        private IEnumerable<Type> GetImplementedTypes(Type serviceType)
        {
            List<Type> types;
            lock (implementedTypesDictionary)
                implementedTypesDictionary.TryGetValue(serviceType, out types);

            if (types == null)
            {
                types = new List<Type>();
                var baseType = serviceType;
                do
                {
                    types.Add(baseType);
                    baseType = baseType.BaseType;
                } while (baseType != null);
                types.Reverse();
                types.InsertRange(0, serviceType.GetInterfaces());
                lock (implementedTypesDictionary)
                    implementedTypesDictionary[serviceType] = types;
            }

            return types;
        }


    }
}