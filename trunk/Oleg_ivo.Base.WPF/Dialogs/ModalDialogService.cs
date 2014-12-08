using System;
using Autofac;
using Oleg_ivo.Base.Autofac;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    /// <summary>
    /// Сервис предоставления и показа диалогов
    /// </summary>
    public class ModalDialogService : IModalDialogService
    {
        public ModalDialogService(IComponentContext context)
        {
            this.context = Enforce.ArgumentNotNull(context, "context");
        }

        private readonly IComponentContext context;

        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<IModalWindow<TDialogViewModel>> onSetup, Action<TDialogViewModel, bool?> onDialogClose)
        {
            if (onSetup != null)
            {
                onSetup(view);
            }
            if (onDialogClose != null)
            {
                view.Closed += (sender, e) => onDialogClose(view.ViewModel, view.DialogResult);
            }
            view.ShowDialog();
        }

        /*/// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<TDialogViewModel, bool?> onDialogClose)
        {
            this.ShowDialog(view, null, onDialogClose);
        }*/

        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<IModalWindow<TDialogViewModel>> onSetup)
        {
            this.ShowDialog(view, onSetup, null);
        }

        /// <summary>
        /// Показать диалог
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="view">Представление</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view)
        {
            this.ShowDialog(view, null, null);
        }

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<TDialogViewModel>> onSetup, Action<TDialogViewModel, bool?> onDialogClose)
        {
            ShowDialog(CreateDialog<TDialogViewModel>(), onSetup, onDialogClose);
        }

        /*/// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onDialogClose">Делегат, срабатывающий после закрытия диалога</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<TDialogViewModel, bool?> onDialogClose)
        {
            ShowDialog(null, onDialogClose);
        }*/

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <param name="onSetup">Делегат для настройки диалога</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<TDialogViewModel>> onSetup)
        {
            CreateAndShowDialog(onSetup, null);
        }

        /// <summary>
        /// Показать диалог (представление создаётся внутри)
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        public void CreateAndShowDialog<TDialogViewModel>()
        {
            CreateAndShowDialog((Action<IModalWindow<TDialogViewModel>>)null, null);
        }

        /// <summary>
        /// Получить новое диалоговое представление
        /// </summary>
        /// <typeparam name="TDialogViewModel">Тип модели представления</typeparam>
        /// <returns></returns>
        public IModalWindow<TDialogViewModel> CreateDialog<TDialogViewModel>()
        {
            return context.Resolve<IModalWindow<TDialogViewModel>>();
        }
    }
}