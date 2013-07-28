using System;
using System.Windows.Forms;
using Autofac;
using Oleg_ivo.PrismExtensions.Autofac.DependencyInjection;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.Base.Autofac.Modules
{
    public class BaseAutofacModule : Module
    {
        private readonly ExceptionHandler exceptionHandler;

        public BaseAutofacModule()
        {
            //Включить обработку исключений
#pragma warning disable 168
            exceptionHandler = new ExceptionHandler();
#pragma warning restore 168
            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();//TODO: в контекст
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(exceptionHandler);
            builder.RegisterModule<PropertyInjectionModule>();
            builder.RegisterModule<ConfigurationActionsModule>();
            // для совместимости со службами, зависящими от IServiceProvider
            builder.RegisterAdapter((ILifetimeScope s) => (IServiceProvider)s).InstancePerLifetimeScope();
            // дефолтный контекст для синхронизации
            System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            builder.RegisterInstance(System.Threading.SynchronizationContext.Current);
        }
    }
}