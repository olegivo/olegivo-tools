using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.WPF.ViewModels;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public class DialogViewModel<TContentViewModel> : DialogViewModelBase where TContentViewModel : ViewModelBase
    {
        public DialogViewModel(TContentViewModel contentViewModel) : base(contentViewModel)
        {
            ContentViewModel = Enforce.ArgumentNotNull(contentViewModel, "contentViewModel");
        }

        public new TContentViewModel ContentViewModel { get; private set; }
    }
}