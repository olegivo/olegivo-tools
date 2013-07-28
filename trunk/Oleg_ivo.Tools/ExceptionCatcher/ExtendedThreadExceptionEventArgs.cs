using System;
using System.Threading;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtendedThreadExceptionEventArgs : ThreadExceptionEventArgs
    {
        /// <summary>
        /// Показать ошибку
        /// </summary>
        public bool ShowError { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Threading.ThreadExceptionEventArgs"/>.
        /// </summary>
        /// <param name="t">Произошедшее исключение <see cref="T:System.Exception"/>. </param>
        public ExtendedThreadExceptionEventArgs(Exception t) : base(t)
        {
            ShowError = true;
        }
    }
}