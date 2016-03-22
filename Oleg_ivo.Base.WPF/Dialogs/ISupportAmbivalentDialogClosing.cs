using Reactive.Bindings;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    /// <summary>
    /// Указывает, что объект поддерживает амбивалентное закрытие диалога (либо положительное, либо отрицательное)
    /// </summary>
    public interface ISupportAmbivalentDialogClosing
    {
        ReactiveProperty<bool> CanClosePositive { get; }
        ReactiveProperty<bool> CanCloseNegative { get; }
    }
}