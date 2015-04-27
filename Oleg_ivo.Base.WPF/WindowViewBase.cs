using System.Windows;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.WPF.ViewModels;

namespace Oleg_ivo.Base.WPF
{
    public class WindowViewBase<TViewModel> : Window where TViewModel : ViewModelBase
    {

        [Dependency(Required = true)]
        public TViewModel ViewModel
        {
            get { return (TViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
