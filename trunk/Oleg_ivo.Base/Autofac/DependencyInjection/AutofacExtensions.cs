using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using BI.Base.DependencyInjection;

namespace Oleg_ivo.PrismExtensions.Autofac.DependencyInjection
{
    public static class AutofacExtensions
    {
        public static IContainer Update(this IContainer container, Action<ContainerBuilder> buildActions)
        {
            Enforce.ArgumentNotNull(container, "container");
            if (buildActions != null)
            {
                var builder = new ContainerBuilder();
                buildActions(builder);
                builder.Update(container);
            }
            return container;
        }

        public static ILifetimeScope Update(this ILifetimeScope lifetimeScope, Action<ContainerBuilder> buildActions)
        {
            Enforce.ArgumentNotNull(lifetimeScope, "lifetimeScope");
            if (buildActions != null)
            {
                var sharingScope = lifetimeScope as ISharingLifetimeScope;
                if (sharingScope != null &&
                    sharingScope.ParentLifetimeScope != null &&
                    sharingScope.ComponentRegistry == sharingScope.ParentLifetimeScope.ComponentRegistry)
                    throw new ArgumentException("Cannot update lifetime scope that shares component registry with parent scope", "lifetimeScope");

                var builder = new ContainerBuilder();
                buildActions(builder);
                builder.Update(lifetimeScope.ComponentRegistry);
            }
            return lifetimeScope;
        }


        /// <summary>
        ///  Resolves instance of concrete type as if it is registered in context
        /// </summary>
        /// <typeparam name="TService">Instance type, must be concrete type with at lease one constructor</typeparam>
        /// <param name="context">Resolution context</param>
        /// <returns>Returns created instace of type <typeparamref name="TService"/></returns>
        /// <seealso cref="ResolveUnregistered(IComponentContext,Type,IEnumerable{Parameter})"/>
        public static TService ResolveUnregistered<TService>(this IComponentContext context)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), Enumerable.Empty<Parameter>());
        }

        public static TService ResolveUnregistered<TService>(this IComponentContext context, params Parameter[] parameters)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), (IEnumerable<Parameter>)parameters);
        }

        public static TService ResolveUnregistered<TService>(this IComponentContext context, IEnumerable<Parameter> parameters)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), parameters);
        }


        /// <summary>
        ///  Resolves instance of concrete type as if it is registered in context
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <param name="serviceType">Instance type, must be concrete type with at least one constructor</param>
        /// <returns>Returns created instace of type <paramref name="serviceType"/></returns>
        /// <remarks>
        ///  Creates nested lifetime, registers type in it's ComponentRegistry,
        ///  then resolves created registration in provided <paramref name="context"/>.
        /// </remarks>
        public static object ResolveUnregistered(this IComponentContext context, Type serviceType)
        {
            return ResolveUnregistered(context, serviceType, Enumerable.Empty<Parameter>());
        }

        public static object ResolveUnregistered(this IComponentContext context, Type serviceType, params Parameter[] parameters)
        {
            return ResolveUnregistered(context, serviceType, (IEnumerable<Parameter>)parameters);
        }

        public static object ResolveUnregistered(this IComponentContext context, Type serviceType, IEnumerable<Parameter> parameters)
        {
            var scope = context.Resolve<ILifetimeScope>();
            using (var innerScope = scope.BeginLifetimeScope(b => b.RegisterType(serviceType)))
            {
                IComponentRegistration reg;
                innerScope.ComponentRegistry.TryGetRegistration(new TypedService(serviceType), out reg);

                var component = context.ResolveComponent(reg, parameters);
                if (context.IsRegistered<PropertyInjectionModule>())
                    context.InjectAttributedProperties(component);
                return component;
            }
        }



        /// <summary>
        ///  Always resolves instance of the concrete type
        /// </summary>
        /// <typeparam name="TService">Instance type, must be concrete type with at least one constructor</typeparam>
        /// <param name="context">Resolution context</param>
        /// <returns>Returns created instace of type <typeparamref name="TService"/></returns>
        /// <remarks>
        /// Tries to resolve service in context, and if it's not registered, resolves it as unregistered
        /// </remarks>
        /// <seealso cref="ResolveUnregistered{TService}(IComponentContext)"/>
        public static TService ResolveAlways<TService>(this IComponentContext context)
        {
            return (TService)ResolveAlways(context, typeof(TService));
        }

        /// <summary>
        ///  Always resolves instance of the concrete type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <param name="serviceType">Instance type, must be concrete type with at least one constructor</param>
        /// <returns>Returns created instace of the type <paramref name="serviceType"/></returns>
        /// <remarks>
        /// Tries to resolve service in context, and if it's not registered, resolves it as unregistered
        /// </remarks>
        /// <seealso cref="ResolveUnregistered(IComponentContext,Type)"/>
        public static object ResolveAlways(this IComponentContext context, Type serviceType)
        {
            object service;
            return context.TryResolve(serviceType, out service)
                       ? service
                       : ResolveUnregistered(context, serviceType);
        }

        /// <summary>
        /// Set any properties attributed with <see cref="DependencyAttribute"/> on <paramref name="instance"/>.
        /// Values are resolved from <paramref name="context"/>.
        /// </summary>
        /// <typeparam name="TInstance">Type of instance. Used only to provide method chaining.</typeparam>
        /// <param name="context">The context from which to resolve the service.</param>
        /// <param name="instance">The instance to inject properties into.</param>
        /// <returns><paramref name="instance"/>.</returns>
        /// <remarks>
        ///  If context has <see cref="PropertyInjectionModule"/> registered, uses it's injector to do the work.
        /// </remarks>
        public static TInstance InjectAttributedProperties<TInstance>(this IComponentContext context, TInstance instance)
        {

            Enforce.ArgumentNotNull(context, "context");
            Enforce.ArgumentNotNull((object)instance, "instance");

            var injectionModule = context.ResolveOptional<PropertyInjectionModule>();
            var injector = injectionModule != null ? injectionModule.Injector : new AttributedPropertyInjector();

            injector.InjectProperties(context, instance, default(InjectionStages?));

            return instance;
        }




        public static IRegistrationBuilder<ServiceConfigurationAction<TService>, SimpleActivatorData, SingleRegistrationStyle>
            RegisterConfigurationAction<TService>(this ContainerBuilder builder, Action<TService> action)
        {
            return builder.RegisterInstance(new ServiceConfigurationAction<TService>(action));
        }

        public static IRegistrationBuilder<ServiceConfigurationAction<TService>, SimpleActivatorData, SingleRegistrationStyle>
            RegisterConfigurationAction<TService>(this ContainerBuilder builder, Action<IComponentRegistration, IComponentContext, TService> action)
        {
            return builder.RegisterInstance(new ServiceConfigurationAction<TService>(action));
        }


        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> Named<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder,
            string serviceName)
        {
            return builder.Named<TLimit>(serviceName);
        }

    }
}
