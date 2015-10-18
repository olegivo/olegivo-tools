using System;
using System.Reactive.Disposables;
using System.Windows.Controls;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : IDisposable
    {
        private readonly CompositeDisposable dispiser;

        public DialogWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Window"/> class. 
        /// </summary>
        public DialogWindow(ContentControl contentView, DialogViewModelBase dialogViewModel)
            : this()
        {
            this.DataContext = dialogViewModel;
            ContentView = contentView;
            ContentControl.Content = ContentView;
            dispiser = new CompositeDisposable(dialogViewModel.CommandClose.Subscribe(b => DialogResult = b));
        }

        public ContentControl ContentView { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            dispiser.Dispose();
        }
    }
}
