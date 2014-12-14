using Autofac;
using Codeplex.Reactive;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.WPF.ViewModels;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public class DialogViewModelBase : ViewModelBase
    {
        private string caption;
        public ReactiveCommand<bool> CommandClose { get; set; }

        [Dependency]
        public IComponentContext Context { get; set; }

        public string Caption
        {
            get { return caption; }
            set
            {
                if (caption == value) return;
                caption = value;
                RaisePropertyChanged(() => Caption);
            }
        }
    }
}