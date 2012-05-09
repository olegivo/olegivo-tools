using System.Collections.Generic;

namespace Oleg_ivo.Tools.Utils
{
    /// <summary>
    /// ������ �����
    /// </summary>
    public class Strings : List<string>
    {
        private readonly bool _uniques = true;

        #region constructors

        public Strings()
        {
        }

        /// <summary>
        /// ��������� ������������� ��������
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
        /// ��������� ������ (���� ��� �� ���������� � ������ � �� �����) � ����� ������
        /// </summary>
        /// <param name="item">������</param>
        /// <returns>true, ���� ���������� �������</returns>
        public new bool Add(string item)
        {
            return Add(item, _uniques, true);
        }

        /// <summary>
        /// ��������� ������ � ����� ������
        /// </summary>
        /// <param name="item">������</param>
        /// <param name="checkContains">��������� ������ ���� ������ �� ���������� � ������</param>
        /// <param name="checkEmpty">���������, ������ ���� ������ �� �����</param>
        /// <returns>true, ���� ���������� �������</returns>
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