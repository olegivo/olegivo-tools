using Autofac;
using Oleg_ivo.Base.WPF.Dialogs.SimpleDialogs;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public class DialogsAutofacModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        ///             registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ModalDialogService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterGeneric(typeof(DialogViewModel<>));
            
            builder.RegisterType<StringInputViewModel>();
            builder.RegisterType<StringInputControl>().AsImplementedInterfaces();
        }
    }
}