using Oleg_ivo.Base.WPF.ViewModels;
using Reactive.Bindings;

namespace Oleg_ivo.Base.WPF.Dialogs.SimpleDialogs
{
    public class StringInputViewModel : ViewModelBase
    {
        public StringInputViewModel()
        {
            Value = new ReactiveProperty<string>();
            Description = new ReactiveProperty<string>();
        }

        public ReactiveProperty<string> Value { get; private set; }
        public ReactiveProperty<string> Description { get; private set; }
    }
}
