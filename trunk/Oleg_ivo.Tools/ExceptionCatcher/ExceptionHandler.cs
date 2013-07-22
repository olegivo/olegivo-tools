using System;
using System.Threading;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    /// <summary>
    /// Обработчик исключений приложения
    /// </summary>
    public sealed class ExceptionHandler
    {
        public EventHandler<ExtendedThreadExceptionEventArgs> AdditionalErrorHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionHandler()
        {
            // Подписываемся на событие генерации исключения в текущем потоке
            Application.ThreadException += OnApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
        }

        /// <summary>
        /// Обработчик исключений приложения
        /// </summary>
        /// <param name="additionalErrorHandler">Дополнительный (внешний) делегат-обработчик ошибки</param>
        public ExceptionHandler(EventHandler<ExtendedThreadExceptionEventArgs> additionalErrorHandler)
            : this()
        {
            AdditionalErrorHandler = additionalErrorHandler;
        }

        /// <summary>
        /// Переопределение обработки исключения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    System.Diagnostics.Trace.WriteLine(e.Exception.ToString(), "Ошибка");
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
                    System.Diagnostics.Trace.WriteLine(exception.ToString(), "Ошибка");
                    if (!FormException.Execute(exception, !e.IsTerminating))
                    {
                        Application.Exit();
                    }
                }
            }

        }
    }
}