using System.Windows.Controls;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.WPF.ViewModels;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public abstract class DialogContentControl<TContentViewModel> : ContentControl, IModalWindowContent<TContentViewModel> /*, IModalWindow<TViewModel>*/ where TContentViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Window"/> class. 
        /// </summary>
        protected DialogContentControl(/*TContentViewModel viewModel*/)
        {
            //DataContext = Enforce.ArgumentNotNull(viewModel, "viewModel");
        }

        //[Dependency(Required = true)]
        public TContentViewModel ViewModel
        {
            get { return (TContentViewModel)DataContext; }
            set
            {
                if(DataContext == value) return;
                DataContext = value;
                //ViewModel.CommandClose = new ReactiveCommand<bool>().AddHandler(b => DialogResult = b);
            }
        }
    }
}