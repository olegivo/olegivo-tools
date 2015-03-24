using System.Windows;
using Reactive.Bindings;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.WPF.Extensions;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public class ModalWindowBase<TViewModel> : Window, IModalWindow<TViewModel> where TViewModel:DialogViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Window"/> class. 
        /// </summary>
        public ModalWindowBase()
        {
            //TODO: caption binding?
        }

        [Dependency(Required = true)]
        public TViewModel ViewModel
        {
            get { return (TViewModel)DataContext; }
            set
            {
                if(DataContext == value) return;
                DataContext = value;
                ViewModel.CommandClose = new ReactiveCommand<bool>().AddHandler(b => DialogResult = b);
            }
        }
    }
}