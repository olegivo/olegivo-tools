using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using BI.Base.DependencyInjection;

namespace Oleg_ivo.PrismExtensions.Autofac.DependencyInjection
{

    /// <summary>
    ///  ������, ��������� �������� ��������, ����������� ��� ��������� ����������,
    ///  ������, ��� ����� ������ ��� ��������������� ��� ���������.
    /// </summary>
    /// <remarks>
    ///  ������ ������ ���� �������������� �� ��� �� ������ �������� ����������� ��� ����, 
    ///  ��� � ��������.
    ///  ��������, ������������������ ������� ����, ��� ���� ������, ����� �� �����������.
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
        ///  ���������� ����, �� ����� ���, � ������� �������� <paramref name="instanceType"/>,
        ///  � ��� ������� � ������ ���������� ���� ����������������� ��������.
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
        ///  ���������� ����, ����������� ����� <paramref name="serviceType"/>, � �������:
        /// <list type="numbered">
        /// <item>��� ����������;</item>
        /// <item>������� ����, �� ����� ����� � ����� ����������;</item>
        /// <item>���������� <paramref name="serviceType"/>.</item>
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