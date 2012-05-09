using System;
using System.Runtime.InteropServices;

namespace Oleg_ivo.Tools
{
    /// <summary>
    /// Эта структура позволяет подсчитать скорость выполнения кода одним из
    /// наиболее точным способов. Фактически вычисления производятся в тактах
    /// процессора, а потом переводятся в миллисекунды (десятичная часть 
    /// является долями секунды).
    /// </summary>
    public struct Timer
    {
        Int64 _start;

        /// <summary>
        /// Начинает подсчет времени выполнения.
        /// </summary>
        public void Start()
        {
            _start = 0;
            QueryPerformanceCounter(ref _start);
        }

        /// <summary>
        /// Завершает подсчет времени исполнения и возвращает время в секундах.
        /// </summary>
        /// <returns>Время в секундах затраченое на выполнение участка
        /// кода. Десятичная часть отражает доли секунды.</returns>
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
    /// Объект, измеряющий и протоколирующий отрезки времени
    /// </summary>
    public class Logger
    {
        #region fields
        Timer _timer = new Timer();
        private string _message;
        
        #endregion

        #region properties
        /// <summary>
        /// Сообщение
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
            System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}: выполнение...", DateTime.Now, message));
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
            System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}: выполнено за {2} секунд", DateTime.Now, Message, _timer.Finish().ToString("N5")));
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