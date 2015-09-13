using System;
using Oleg_ivo.Base.WPF.ViewModels;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    public interface IModalDialogService
    {
        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        bool? ShowDialog<TDialogViewModel>(IModalWindow<DialogViewModel<TDialogViewModel>> view, Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup, Action<DialogViewModel<TDialogViewModel>, bool?> onDialogClose) where TDialogViewModel : ViewModelBase;

        /*/// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<TDialogViewModel, bool?> onDialogClose);*/

        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        bool? ShowDialog<TDialogViewModel>(IModalWindow<DialogViewModel<TDialogViewModel>> view, Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup) where TDialogViewModel : ViewModelBase;

        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        bool? ShowDialog<TDialogViewModel>(IModalWindow<DialogViewModel<TDialogViewModel>> view) where TDialogViewModel : ViewModelBase;

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        bool? CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup, Action<DialogViewModel<TDialogViewModel>, bool?> onDialogClose) where TDialogViewModel : ViewModelBase;

        /*/// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        void CreateAndShowDialog<TDialogViewModel>(Action<TDialogViewModel, bool?> onDialogClose);*/

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        bool? CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup) where TDialogViewModel : ViewModelBase;

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        bool? CreateAndShowDialog<TDialogViewModel>() where TDialogViewModel : ViewModelBase;
        
        /// <summary>
        /// Получить новое диалоговое представление
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <returns></returns>
        IModalWindow<DialogViewModel<TDialogViewModel>> CreateDialog<TDialogViewModel>() where TDialogViewModel : ViewModelBase;
    }
}