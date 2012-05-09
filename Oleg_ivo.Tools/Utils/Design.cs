using System.Reflection;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.Utils
{
    ///<summary>
    /// Утилита для дизайна
    ///</summary>
    public class Design
    {
        #region Singleton

        private static Design _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static Design Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Design();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Design" />.
        /// </summary>
        protected Design()
        {
        }

        #endregion


        #region methods

        ///<summary>
        /// Находится ли форма, на которой находится элемент управления (если она есть) 
        /// или сам элемент управления (если её нет) в режиме дизайнера
        ///</summary>
        ///<param name="control"></param>
        ///<returns></returns>
        public bool IsInDesignMode(Control control)
        {
            bool designMode = false;

            if (control!=null)
            {
                Form form = control.FindForm();
                if (form!=null)
                {
                    control = form;
                }

                PropertyInfo designModeProperty = typeof(Control).GetProperty("DesignMode", BindingFlags.NonPublic | BindingFlags.Instance);
                designMode = (bool)designModeProperty.GetValue(control, null);
            }

            return designMode;
        }
        #endregion

    }
}
