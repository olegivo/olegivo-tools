using System.Collections.Generic;

namespace Oleg_ivo.Tools.Utils
{
    /// <summary>
    /// —писок строк
    /// </summary>
    public class Strings : List<string>
    {
        private readonly bool _uniques = true;

        #region constructors

        public Strings()
        {
        }

        /// <summary>
        /// ƒопустимы повтор€ющиес€ значени€
        /// </summary>
        /// <param name="uniques"></param>
        public Strings(bool uniques)
        {
            this._uniques = uniques;
        }

        public Strings(IEnumerable<string> collection)
            : base(collection)
        {
        }

        #endregion

        /// <summary>
        /// ƒобавл€ет строку (если она не содержитс€ в списке и не пуста) в конец списка
        /// </summary>
        /// <param name="item">—трока</param>
        /// <returns>true, если добавление успешно</returns>
        public new bool Add(string item)
        {
            return Add(item, _uniques, true);
        }

        /// <summary>
        /// ƒобавл€ет строку в конец списка
        /// </summary>
        /// <param name="item">—трока</param>
        /// <param name="checkContains">ƒобавл€ть только если строка не содержитс€ в списке</param>
        /// <param name="checkEmpty">ƒобавл€ть, только если строка не пуста</param>
        /// <returns>true, если добавление успешно</returns>
        public bool Add(string item, bool checkContains, bool checkEmpty)
        {
            bool result = (!checkContains || !this.Contains(item)) 
                          && (!checkEmpty || !string.IsNullOrEmpty(item));
            if(result)
                base.Add(item);
            return result;
        }

        public new void AddRange(IEnumerable<string> collection)
        {
            if(collection!=null)
                foreach (string s in collection)
                    Add(s);
        }

        public static Strings operator +(Strings strings1, Strings strings2)
        {
            Strings strings = new Strings(strings1);
            strings.AddRange(strings2);
            return strings;
        }

        public string ToStringSeparating(string separator)
        {
            if (this.Count > 0)
                return Oleg_ivo.Tools.Utils.Utils.ListUtils.ToStringSeparating(this.ToArray(), separator);
            return null;
        }
    }
}