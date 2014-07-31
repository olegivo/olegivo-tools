using System.ComponentModel;
using System.Windows;
using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Prism.AutofacExtension;

namespace Oleg_ivo.Base.Autofac.Modules
{
    public class AutofacBootstrapperBase<TMainShell, TCommandLineOptions, TPrismModule, TAutofacModule> : AutofacBootstrapper
        where TMainShell : Window
        where TPrismModule : IModule
        where TAutofacModule : Module, new()
    {
        private readonly string[] args;

        protected AutofacBootstrapperBase(string[] args)
        {
            this.args = args;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<TMainShell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterModule<TAutofacModule>();
            builder.RegisterModule(new CommandLineHelperAutofacModule<TCommandLineOptions>(args));
            builder.RegisterType<TPrismModule>();
            builder.RegisterType<TMainShell>().OnActivated(e => e.Instance.Closing += (sender, eventArgs) => OnClosing(eventArgs));
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // register prism module
            var prismModuleType = typeof(TPrismModule);
            ModuleCatalog.AddModule(new ModuleInfo(prismModuleType.Name, prismModuleType.AssemblyQualifiedName));
        }
    }
}