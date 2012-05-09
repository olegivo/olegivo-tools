using System;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    ///<summary>
    /// Одиночка для Debug
    ///</summary>
    public class Debug
    {
        #region Singleton

        private static Debug _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static Debug Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Debug();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Debug" />.
        /// </summary>
        protected Debug()
        {
        }

        #endregion

        #region methods

        ///<summary>
        /// Вызывает исключение только в Debug-mode
        ///</summary>
        ///<param name="exception">Исключение</param>
        public void ThrowOnlyInDebug(Exception exception)
        {
#if DEBUG
            if (exception!=null)
            {
                string exString = exception.ToString();
                Logger.Print(exString);
                throw new Exception(exception.GetBaseException().Message, exception);
                //MessageBox.Show(exString, "Ошибочка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }
        #endregion

    }
}
