using System;
using System.Security;
using System.Threading;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    /// <summary>
    /// ���������� ���������� ����������
    /// </summary>
    public sealed class ExceptionHandler
    {
        public EventHandler<ExtendedThreadExceptionEventArgs> AdditionalErrorHandler { get; set; }

        private enum ApplicationType
        {
            WinForms,
            WPF
        }

        private ApplicationType AppType;

        /// <summary>
        /// Constructor
        /// </summary>
        [SecuritySafeCritical]
        public ExceptionHandler()
        {
            // ������������� �� ������� ��������� ���������� � ������� ������
            if (System.Windows.Application.Current == null)
            {
                System.Windows.Forms.Application.ThreadException += OnApplicationThreadException;
                AppType = ApplicationType.WinForms;
            }
            else
            {
                System.Windows.Application.Current.DispatcherUnhandledException += OnWPFApplicationDispatcherUnhandledException;
                AppType = ApplicationType.WPF;
            }
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
        }

        /// <summary>
        /// ���������� ���������� ����������
        /// </summary>
        /// <param name="additionalErrorHandler">�������������� (�������) �������-���������� ������</param>
        public ExceptionHandler(EventHandler<ExtendedThreadExceptionEventArgs> additionalErrorHandler)
            : this()
        {
            AdditionalErrorHandler = additionalErrorHandler;
        }

        /// <summary>
        /// ��������������� ��������� ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [SecuritySafeCritical]
        private void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            bool showError = true;

            try
            {
                if (AdditionalErrorHandler != null)
                {
                    var eventArgs = new ExtendedThreadExceptionEventArgs(e.Exception);
                    AdditionalErrorHandler(this, eventArgs);
                    showError = eventArgs.ShowError;
                }
            }
            finally
            {
                if (showError)
                {
                    System.Diagnostics.Trace.WriteLine(e.Exception.ToString(), "������");
                    if (!FormException.Execute(e.Exception))
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                }
            }

        }

        private void OnWPFApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            bool showError = true;

            try
            {
                if (AdditionalErrorHandler != null)
                {
                    var eventArgs = new ExtendedThreadExceptionEventArgs(e.Exception);
                    AdditionalErrorHandler(this, eventArgs);
                    showError = eventArgs.ShowError;
                }
            }
            finally
            {
                if (showError)
                {
                    System.Diagnostics.Trace.WriteLine(e.Exception.ToString(), "������");
                    if (!(e.Handled = FormException.Execute(e.Exception)))
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                }
            }

        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            bool showError = true;
            var exception = e.ExceptionObject as Exception;

            try
            {
                if (AdditionalErrorHandler != null)
                {
                    var eventArgs = new ExtendedThreadExceptionEventArgs(exception);
                    AdditionalErrorHandler(this, eventArgs);
                    showError = eventArgs.ShowError;
                }
            }
            finally
            {
                if (showError)
                {
                    System.Diagnostics.Trace.WriteLine(exception, "������");
                    if (!FormException.Execute(exception, !e.IsTerminating))
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                }
            }

        }
    }
}