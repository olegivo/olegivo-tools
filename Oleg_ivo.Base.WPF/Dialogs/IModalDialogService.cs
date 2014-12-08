using System;

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
        void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<IModalWindow<TDialogViewModel>> onSetup, Action<TDialogViewModel, bool?> onDialogClose);

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
        void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<IModalWindow<TDialogViewModel>> onSetup);

        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view);
        
        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<TDialogViewModel>> onSetup, Action<TDialogViewModel, bool?> onDialogClose);

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
        void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<TDialogViewModel>> onSetup);

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        void CreateAndShowDialog<TDialogViewModel>();
        
        /// <summary>
        /// Получить новое диалоговое представление
        /// </summary>
        /// <typeparam name="TViewModel">Тип модели представления</typeparam>
        /// <returns></returns>
        IModalWindow<TViewModel> CreateDialog<TViewModel>();
    }
}