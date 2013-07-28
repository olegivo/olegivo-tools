using System;
using Autofac;
using Autofac.Core;

namespace Oleg_ivo.PrismExtensions.Autofac.DependencyInjection
{
    public class ComponentRegistrationEventExtenderModule : Module
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public EventHandler<ActivatedEventArgs<object>> ActivatedHandler { get; set; }
        public EventHandler<ActivatingEventArgs<object>> ActivatingHandler { get; set; }
        public EventHandler<PreparingEventArgs> PreparingHandler { get; set; }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);
            if (registration.Ownership == InstanceOwnership.OwnedByLifetimeScope)
            {
                if (ActivatingHandler != null) registration.Activating += ActivatingHandler;
                if (ActivatedHandler != null) registration.Activated += ActivatedHandler;
                if (PreparingHandler != null) registration.Preparing += PreparingHandler;
                log.Trace("Attach to {0}", registration);
            }
            else
            {
                log.Trace("Do not attach to {0}", registration);
            }
        }

    }
}