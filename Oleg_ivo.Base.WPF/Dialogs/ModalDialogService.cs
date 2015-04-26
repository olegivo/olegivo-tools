using System;
using System.Reactive.Disposables;
using System.Windows.Controls;
using Autofac;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.WPF.ViewModels;

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
        public void ShowDialog<TDialogViewModel>(IModalWindow<DialogViewModel<TDialogViewModel>> view, Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup, Action<DialogViewModel<TDialogViewModel>, bool?> onDialogClose) where TDialogViewModel : ViewModelBase
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
        public void ShowDialog<TDialogViewModel>(IModalWindow<DialogViewModel<TDialogViewModel>> view, Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup) where TDialogViewModel : ViewModelBase
        {
            this.ShowDialog(view, onSetup, null);
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="view">�������������</param>
        public void ShowDialog<TDialogViewModel>(IModalWindow<DialogViewModel<TDialogViewModel>> view) where TDialogViewModel : ViewModelBase
        {
            this.ShowDialog(view, null, null);
        }

        /// <summary>
        /// �������� ������ (������������� �������� ������)
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <param name="onSetup">������� ��� ��������� �������</param>
        /// <param name="onDialogClose">�������, ������������� ����� �������� �������</param>
        public void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup, Action<DialogViewModel<TDialogViewModel>, bool?> onDialogClose) where TDialogViewModel : ViewModelBase
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
        public void CreateAndShowDialog<TDialogViewModel>(Action<IModalWindow<DialogViewModel<TDialogViewModel>>> onSetup) where TDialogViewModel : ViewModelBase
        {
            CreateAndShowDialog(onSetup, null);
        }

        /// <summary>
        /// �������� ������ (������������� �������� ������)
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        public void CreateAndShowDialog<TDialogViewModel>() where TDialogViewModel : ViewModelBase
        {
            CreateAndShowDialog((Action<IModalWindow<DialogViewModel<TDialogViewModel>>>)null, null);
        }

        /// <summary>
        /// �������� ����� ���������� �������������
        /// </summary>
        /// <typeparam name="TDialogViewModel">��� ������ �������������</typeparam>
        /// <returns></returns>
        public IModalWindow<DialogViewModel<TDialogViewModel>> CreateDialog<TDialogViewModel>() where TDialogViewModel : ViewModelBase
        {
/*
            var modalWindowContentBase = context.Resolve<IModalWindowContent<TDialogViewModel>>();
            var dialogViewModelBase = context.Resolve<DialogViewModel<TDialogViewModel>>();
            return new ModalWindowProxy<TDialogViewModel>(dialogViewModelBase, modalWindowContentBase);
*/
            return context.ResolveUnregistered<ModalWindowProxy<TDialogViewModel>>();
            //return context.Resolve<IModalWindow<TDialogViewModel>>();
        }

        protected class ModalWindowProxy<TDialogViewModel> : IModalWindow<DialogViewModel<TDialogViewModel>> where TDialogViewModel : ViewModelBase
        {
            private readonly DialogViewModel<TDialogViewModel> dialogViewModel;
            private readonly DialogWindow dialogWindow;//TODO: �������� ���������
            private readonly CompositeDisposable disposer;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public ModalWindowProxy(DialogViewModel<TDialogViewModel> dialogViewModel, IModalWindowContent<TDialogViewModel> modalWindowContentControl)
            {
                this.dialogViewModel = Enforce.ArgumentNotNull(dialogViewModel, "dialogViewModel");
                var contentControl = Enforce.ArgumentNotNull(modalWindowContentControl, "modalWindowContentControl");
                dialogWindow = new DialogWindow((ContentControl) contentControl, dialogViewModel);//TODO: resolve
                disposer = new CompositeDisposable(dialogWindow, dialogViewModel);
            }

            public bool? DialogResult
            {
                get { return dialogWindow.DialogResult; }
                set { dialogWindow.DialogResult= value; }
            }

            public event EventHandler Closed
            {
                add { dialogWindow.Closed += value; }
                remove { dialogWindow.Closed -= value; }
            }

            public void Show()
            {
                dialogWindow.Show();
            }

            public bool? ShowDialog()
            {
                return dialogWindow.ShowDialog();
            }


            public DialogViewModel<TDialogViewModel> ViewModel
            {
                get { return dialogViewModel; }
                set { throw new NotImplementedException(); }
            }

            public string Title
            {
                get { return dialogViewModel.Caption; }
                set { dialogViewModel.Caption = value; }
            }

            public void Close()
            {
                dialogWindow.Close();
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                disposer.Dispose();
            }
        }
    }
}