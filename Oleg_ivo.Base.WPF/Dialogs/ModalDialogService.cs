using System;
using Autofac;
using Oleg_ivo.Base.Autofac;

namespace Oleg_ivo.Base.WPF.Dialogs
{
    /// <summary>
    /// ������ �������������� � ������ ��������
    /// </summary>
    public class ModalDialogService : IModalDialogService
    {
        public ModalDialogService(IComponentContext context)
        {
            this.context = Enforce.ArgumentNotNull(context, "context");
        }

        private readonly IComponentContext context;

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="view">�������������</param>
        /// <param name="onSetup">������� ��� ��������� �������</param>
        /// <param name="onDialogClose">�������, ������������� ����� �������� �������</param>
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
        /// �������� ������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="view">�������������</param>
        /// <param name="onDialogClose">�������, ������������� ����� �������� �������</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<TDialogViewModel, bool?> onDialogClose)
        {
            this.ShowDialog(view, null, onDialogClose);
        }*/

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="view">�������������</param>
        /// <param name="onSetup">������� ��� ��������� �������</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view, Action<IModalWindow<TDialogViewModel>> onSetup)
        {
            this.ShowDialog(view, onSetup, null);
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="view">�������������</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<TDialogViewModel> view)
        {
            this.ShowDialog(view, null, null);
        }

        /// <summary>
        /// �������� ������ (������������� �������� ������)
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="onSetup">������� ��� ��������� �������</param>
        /// <param name="onDialogClose">�������, ������������� ����� �������� �������</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<TDialogViewModel>> onSetup, Action<TDialogViewModel, bool?> onDialogClose)
        {
            ShowDialog(CreateDialog<TDialogViewModel>(), onSetup, onDialogClose);
        }

        /*/// <summary>
        /// �������� ������ (������������� �������� ������)
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="onDialogClose">�������, ������������� ����� �������� �������</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<TDialogViewModel, bool?> onDialogClose)
        {
            ShowDialog(null, onDialogClose);
        }*/

        /// <summary>
        /// �������� ������ (������������� �������� ������)
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="onSetup">������� ��� ��������� �������</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<TDialogViewModel>> onSetup)
        {
            CreateAndShowDialog(onSetup, null);
        }

        /// <summary>
        /// �������� ������ (������������� �������� ������)
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        public void CreateAndShowDialog<TDialogViewModel>()
        {
            CreateAndShowDialog((Action<IModalWindow<TDialogViewModel>>)null, null);
        }

        /// <summary>
        /// �������� ����� ���������� �������������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <returns></returns>
        public IModalWindow<TDialogViewModel> CreateDialog<TDialogViewModel>()
        {
            return context.Resolve<IModalWindow<TDialogViewModel>>();
        }
    }
}