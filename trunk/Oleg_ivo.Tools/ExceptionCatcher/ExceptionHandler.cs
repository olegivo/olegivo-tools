using System;
using System.Threading;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    /// <summary>
    /// ���������� ���������� ����������
    /// </summary>
    public sealed class ExceptionHandler
    {
        private readonly EventHandler<ExtendedThreadExceptionEventArgs> _additionalErrorHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionHandler()
        {
            // ������������� �� ������� ��������� ���������� � ������� ������
            Application.ThreadException += OnApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
        }

        /// <summary>
        /// ���������� ���������� ����������
        /// </summary>
        /// <param name="additionalErrorHandler">�������������� (�������) �������-���������� ������</param>
        public ExceptionHandler(EventHandler<ExtendedThreadExceptionEventArgs> additionalErrorHandler)
            : this()
        {
            _additionalErrorHandler = additionalErrorHandler;
        }

        /// <summary>
        /// ��������������� ��������� ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            bool showError = true;

            try
            {
                if (_additionalErrorHandler != null)
                {
                    var eventArgs = new ExtendedThreadExceptionEventArgs(e.Exception);
                    _additionalErrorHandler(this, eventArgs);
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
                        Application.Exit();
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
                if (_additionalErrorHandler != null)
                {
                    var eventArgs = new ExtendedThreadExceptionEventArgs(exception);
                    _additionalErrorHandler(this, eventArgs);
                    showError = eventArgs.ShowError;
                }
            }
            finally
            {
                if (showError)
                {
                    System.Diagnostics.Trace.WriteLine(exception.ToString(), "������");
                    if (!FormException.Execute(exception, !e.IsTerminating))
                    {
                        Application.Exit();
                    }
                }
            }

        }
    }
}