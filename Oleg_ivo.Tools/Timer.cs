using System;
using System.Runtime.InteropServices;

namespace Oleg_ivo.Tools
{
    /// <summary>
    /// ��� ��������� ��������� ���������� �������� ���������� ���� ����� ��
    /// �������� ������ ��������. ���������� ���������� ������������ � ������
    /// ����������, � ����� ����������� � ������������ (���������� ����� 
    /// �������� ������ �������).
    /// </summary>
    public struct Timer
    {
        Int64 _start;

        /// <summary>
        /// �������� ������� ������� ����������.
        /// </summary>
        public void Start()
        {
            _start = 0;
            QueryPerformanceCounter(ref _start);
        }

        /// <summary>
        /// ��������� ������� ������� ���������� � ���������� ����� � ��������.
        /// </summary>
        /// <returns>����� � �������� ���������� �� ���������� �������
        /// ����. ���������� ����� �������� ���� �������.</returns>
        public float Finish()
        {
            Int64 finish = 0;
            QueryPerformanceCounter(ref finish);

            Int64 freq = 0;
            QueryPerformanceFrequency(ref freq);
            return (((float)(finish - _start) / (float)freq));
        }

        [DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceCounter(ref Int64 performanceCount);

        [DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceFrequency(ref Int64 frequency);
    }

    /// <summary>
    /// ������, ���������� � ��������������� ������� �������
    /// </summary>
    public class Logger
    {
        #region fields
        Timer _timer = new Timer();
        private string _message;
        
        #endregion

        #region properties
        /// <summary>
        /// ���������
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        
        #endregion

        ///<summary>
        ///
        ///</summary>
        ///<param name="message"></param>
        public static void Print(string message)
        {
#if TRACE
            System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}", DateTime.Now, message));
#endif
        }

        ///<summary>
        ///
        ///</summary>
        public Logger()
        {
            _timer.Start();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="message"></param>
        public Logger(string message)
        {
            Message = message;
#if TRACE
            System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}: ����������...", DateTime.Now, message));
            _timer.Start();
#endif
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="additionalLinesCount"></param>
        public void End(int additionalLinesCount)
        {
#if TRACE
            System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}: ��������� �� {2} ������", DateTime.Now, Message, _timer.Finish().ToString("N5")));
            for (int i = 0; i < additionalLinesCount;i++ )
                System.Diagnostics.Trace.WriteLine("");
#endif
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="message"></param>
        ///<param name="additionalLinesCount"></param>
        public void End(string message, int additionalLinesCount)
        {
            Message = message;
            End(additionalLinesCount);
        }
    }
}