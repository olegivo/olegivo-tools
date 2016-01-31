using System;
using System.Windows.Media;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public interface IModalWindow<TViewModel> : IDisposable
    {
        bool? DialogResult { get; set; }
        event EventHandler Closed;
        void Show();
        bool? ShowDialog();
        TViewModel ViewModel { get; set; }
        string Title { get; set; }
        double Height { get; set; }
        double Width { get; set; }
        ImageSource Icon { get; set; }
        void Close();
    }
}
