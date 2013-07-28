using System;
using System.Windows;
using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Prism.AutofacExtension;

namespace Oleg_ivo.Base.Autofac.Prism
{
    public class Bootstrapper<Shell> : AutofacBootstrapper where Shell : Window
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterType<Shell>();

            // register autofac module
            builder.RegisterModule<MyModuleConfiguration>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // register prism module
            Type typeNewsModule = typeof(MyModule);
            ModuleCatalog.AddModule(new ModuleInfo(typeNewsModule.Name, typeNewsModule.AssemblyQualifiedName));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
    
    public class MyModuleConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MyModule>();
            builder.RegisterType<MyViewModel>();
            builder.RegisterType<MyView>().UsingConstructor(typeof(MyViewModel));
        }
    }
}