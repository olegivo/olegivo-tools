using System;
using Autofac;
using Autofac.Core;

namespace Oleg_ivo.Base.Autofac
{

    /// <summary>
    ///  Non-generic интерфейс для работы с <see cref="ServiceConfigurationAction{TService}"/>.
    /// </summary>
    public interface IServiceConfigurationAction
    {
        void ConfigureInstance(IComponentRegistration component, IComponentContext context, object instance);
    }


    /// <summary>
    ///  Класс, оборачивает действие по настройке.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public class ServiceConfigurationAction<TService>: IServiceConfigurationAction
    {
        private readonly Action<IComponentRegistration, IComponentContext, TService> m_ConfigurationAction;
        private readonly Action<TService> m_Action;

        public ServiceConfigurationAction(Action<TService> action)
        {
            m_Action = Enforce.ArgumentNotNull(action, "action");
        }

        public ServiceConfigurationAction(Action<IComponentRegistration, IComponentContext, TService> action)
        {
            m_ConfigurationAction = Enforce.ArgumentNotNull(action, "action");
        }

        void ConfigureInstance(IComponentRegistration component, IComponentContext context, TService instance)
        {
            if (m_Action != null)
                m_Action.Invoke(instance);
            else if (m_ConfigurationAction != null)
                m_ConfigurationAction.Invoke(component, context, instance);
        }

        void IServiceConfigurationAction.ConfigureInstance(IComponentRegistration component, IComponentContext context, object instance)
        {
            ConfigureInstance(component, context, (TService)instance);
        }
    }
}