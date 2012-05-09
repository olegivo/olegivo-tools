using System;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    ///<summary>
    /// �������� ��� Debug
    ///</summary>
    public class Debug
    {
        #region Singleton

        private static Debug _instance;

        ///<summary>
        /// ������������ ���������
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
        /// �������������� ����� ��������� ������ <see cref="Debug" />.
        /// </summary>
        protected Debug()
        {
        }

        #endregion

        #region methods

        ///<summary>
        /// �������� ���������� ������ � Debug-mode
        ///</summary>
        ///<param name="exception">����������</param>
        public void ThrowOnlyInDebug(Exception exception)
        {
#if DEBUG
            if (exception!=null)
            {
                string exString = exception.ToString();
                Logger.Print(exString);
                throw new Exception(exception.GetBaseException().Message, exception);
                //MessageBox.Show(exString, "��������...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }
        #endregion

    }
}
