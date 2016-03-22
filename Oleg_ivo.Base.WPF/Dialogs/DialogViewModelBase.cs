using System;
using System.Windows.Media;
using Oleg_ivo.Base.WPF.ViewModels;
using Reactive.Bindings;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public abstract class DialogViewModelBase : ViewModelBase, ISupportAmbivalentDialogClosing
    {
        private string caption;
        private double height;
        private double width;
        private ImageSource icon;

        protected DialogViewModelBase(ViewModelBase contentViewModel)
        {
            ContentViewModel = contentViewModel;
            
            var supportCloseDialog = contentViewModel as ISupportAmbivalentDialogClosing;
            if (supportCloseDialog!=null)
            {
                CanClosePositive = supportCloseDialog.CanClosePositive;
                CanCloseNegative = supportCloseDialog.CanCloseNegative;
            }

            if(CanClosePositive==null) CanClosePositive = new ReactiveProperty<bool>(true);
            if(CanCloseNegative==null) CanCloseNegative = new ReactiveProperty<bool>(true);

            CommandClosePositive = new ReactiveCommand(CanClosePositive);
            CommandCloseNegative = new ReactiveCommand(CanCloseNegative);
            CommandClose = new ReactiveCommand<bool>();

            Disposer.Add(CommandClosePositive.Subscribe(_ => CommandClose.Execute(true)));
            Disposer.Add(CommandCloseNegative.Subscribe(_ => CommandClose.Execute(false)));

            Disposer.Add(CommandClose);
            Disposer.Add(CommandClosePositive);
            Disposer.Add(CommandCloseNegative);

            if (supportCloseDialog == null)
            {
                Disposer.Add(CanClosePositive);
                Disposer.Add(CanCloseNegative);
            }
        }

        public ViewModelBase ContentViewModel { get; private set; }

        public ReactiveProperty<bool> CanClosePositive { get; private set; }
        public ReactiveProperty<bool> CanCloseNegative { get; private set; }

        public ReactiveCommand CommandClosePositive { get; private set; }
        public ReactiveCommand CommandCloseNegative { get; private set; }
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

        public ImageSource Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                RaisePropertyChanged("Icon");
            }
        }
    }
}