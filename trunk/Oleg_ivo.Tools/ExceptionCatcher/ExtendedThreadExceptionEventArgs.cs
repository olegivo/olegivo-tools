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
        /// �������� ������
        /// </summary>
        public bool ShowError { get; set; }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.Threading.ThreadExceptionEventArgs"/>.
        /// </summary>
        /// <param name="t">������������ ���������� <see cref="T:System.Exception"/>. </param>
        public ExtendedThreadExceptionEventArgs(Exception t) : base(t)
        {
            ShowError = true;
        }
    }
}