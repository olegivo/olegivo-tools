using Oleg_ivo.Base.WPF.ViewModels;
using Reactive.Bindings;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        private string caption;
        private double height;
        private double width;

        protected DialogViewModelBase(ViewModelBase contentViewModel)
        {
            ContentViewModel = contentViewModel;
            CommandClose = new ReactiveCommand<bool>();
            Disposer.Add(CommandClose);
        }

        public ViewModelBase ContentViewModel { get; private set; }
        
        public ReactiveCommand<bool> CommandClose { get; private set; }

        public string Caption
        {
            get { return caption; }
            set
            {
                if (caption == value) return;
                caption = value;
                RaisePropertyChanged("Caption");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged("Height");
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged("Width");
            }
        }
    }
}