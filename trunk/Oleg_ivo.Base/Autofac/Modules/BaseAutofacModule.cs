using System;
using System.Windows.Forms;
using Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.Base.Autofac.Modules
{
    public class BaseAutofacModule : Module
    {
        private readonly ExceptionHandler exceptionHandler;

        public BaseAutofacModule()
        {
            exceptionHandler = new ExceptionHandler();
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(DbConnectionProvider.Instance)
                .OnActivated(args => args.Instance.SetupConnectionStringFromConfigurationFile());
            builder.RegisterModule<PropertyInjectionModule>();
            builder.RegisterModule<ConfigurationActionsModule>();
            // для совместимости со службами, зависящими от IServiceProvider
            builder.RegisterAdapter((ILifetimeScope s) => (IServiceProvider)s).InstancePerLifetimeScope();
            // дефолтный контекст для синхронизации
            System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            builder.RegisterInstance(System.Threading.SynchronizationContext.Current);

            //exceptions
            builder.RegisterInstance(exceptionHandler);
        }
    }
}