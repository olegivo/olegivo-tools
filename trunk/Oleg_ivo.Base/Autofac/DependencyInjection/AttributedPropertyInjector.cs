using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using BI.Base.DependencyInjection;
using Oleg_ivo.PrismExtensions.Extensions;
using Oleg_ivo.PrismExtensions.NamedObject;

namespace Oleg_ivo.PrismExtensions.Autofac.DependencyInjection
{
    class AttributedPropertyInjector
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();


        public void InjectProperties(IComponentContext context, object instance, InjectionStages? stage)
        {
            Enforce.ArgumentNotNull(context, "context");
            Enforce.ArgumentNotNull(instance, "instance");

            Type instanceType = instance.GetType();

            log.Trace("injecting properties in {0} at stage {1}", instance, stage);


            foreach (var attrProperty in GetTypeDependencyProperties(instanceType))
            {
                DependencyAttribute attr = attrProperty.Attribute;
                PropertyInfo property = attrProperty.PropertyInfo;

                if (stage.HasValue && attr.Stage != stage)
                    continue;

                Type propertyType = attrProperty.PropertyInfo.PropertyType;

                var service = ((attr.ServiceName != null)
                                   ? new KeyedService(attr.ServiceName, propertyType) as Service
                                   : new TypedService(propertyType) as Service);

                if (!attr.Required && !context.IsRegisteredService(service))
                    continue;

                var accessors = property.GetAccessors(false);
                if (accessors.Length == 1 && accessors[0].ReturnType != typeof(void))
                    continue;

                object propertyValue;
                if (attr.DefaultType == null)
                    propertyValue = context.ResolveService(service);
                else
                {
                    if (!context.TryResolveService(service, out propertyValue))
                        propertyValue = context.ResolveAlways(attr.DefaultType);
                }

                NamedObjectExtensions.SetParent(propertyValue as INamedObject, instance);

                property.SetValue(instance, propertyValue, null);
            }
        }


        private struct AttributedPropertyInfo {
            public PropertyInfo PropertyInfo;
            public DependencyAttribute Attribute; 
        }

        private readonly IDictionary<Type, IEnumerable<AttributedPropertyInfo>> typeDependencyProperties = new Dictionary<Type,IEnumerable<AttributedPropertyInfo>>();

        IEnumerable<AttributedPropertyInfo> GetTypeDependencyProperties(Type type)
        {
            lock (typeDependencyProperties)
            {
                var properties = typeDependencyProperties.GetValueOrDefault(type);
                if (properties == null)
                {
                    properties = 
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Select(pi => new AttributedPropertyInfo()
                        {
                            PropertyInfo = pi,
                            Attribute = pi.GetCustomAttributes(typeof(DependencyAttribute), true).FirstOrDefault() as DependencyAttribute
                        })
                        .Where(api =>
                            api.Attribute != null &&
                            !api.PropertyInfo.PropertyType.IsValueType &&
                            api.PropertyInfo.GetIndexParameters().Length == 0
                        ).ToArray();

                    typeDependencyProperties[type] = properties;
                }
                return properties;
            }
        }
    }
}
