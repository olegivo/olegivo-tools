using System;

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
        void Close();
    }

    public interface IModalWindowContent<TViewModel>
    {
        TViewModel ViewModel { get; set; }
/* TODO:
        event EventHandler Closed;
        string Title { get; set; }
        void Close();
*/
    }
}
