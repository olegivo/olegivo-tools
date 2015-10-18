namespace Oleg_ivo.Base.WPF.Dialogs
{
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