using System.Windows.Controls;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.WPF.ViewModels;

namespace Oleg_ivo.Base.WPF
{
    public class ContentControlViewBase<TViewModel> : ContentControl where TViewModel : ViewModelBase
    {

        [Dependency(Required = true)]
        public TViewModel ViewModel
        {
            get { return (TViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}