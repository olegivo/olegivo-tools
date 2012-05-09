using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// ������, ��������������� �� ������ ��������� �����
    /// </summary>
    /// <typeparam name="T">��� ��������� ������</typeparam>
    public abstract class MultyIndexedList<T> : BindingList<T>
    {
        #region fields

        protected string keyPropertyName;
        private bool _onlyUnique = true;

        #endregion

        #region properties

        #endregion


        #region constructors
        ///<summary>
        ///
        ///</summary>
        ///<param name="objects"></param>
        public MultyIndexedList(IEnumerable<T> objects):base(SafeObjects(objects))
        {
            
        }

        private static List<T> SafeObjects(IEnumerable<T> objects)
        {
            return objects!=null 
                ? new List<T>(objects)
                : new List<T>();
        }

        ///<summary>
        ///
        ///</summary>
        public MultyIndexedList():this(null)
        {
            
        }

        #endregion

        #region Overrides of BindingList<T>

        /// <summary>
        /// �������� �������  <see cref="E:System.ComponentModel.BindingList`1.AddingNew" />.
        /// </summary>
        /// <param name="e">������ <see cref="T:System.ComponentModel.AddingNewEventArgs" />, ���������� ������ �������. </param>
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        /// <summary>
        /// �������� ������� <see cref="E:System.ComponentModel.BindingList`1.ListChanged" />.
        /// </summary>
        /// <param name="e">������ <see cref="T:System.ComponentModel.ListChangedEventArgs" />, ���������� ������, ����������� � �������. </param>
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

        /// <summary>
        /// ������� �� ��������� ��� ��������.
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
        }

        /// <summary>
        /// ��������� �������� ������� � ������ �� ���������� �������.
        /// </summary>
        /// <param name="index">������������� �� ���� ������, �� �������� ������� �������� �������.</param>
        /// <param name="item">�������, ������� 
        /// ��������� �������� � ������.</param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
        }

        /// <summary>
        /// ������� ������� � ��������� ��������.
        /// </summary>
        /// <param name="index">������ (� ����) ���������� ��������. </param>
        /// <exception cref="T:System.NotSupportedException">������ ��� ����������� ������� ��������� � �������� <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> ��������������� ������ false. </exception>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        /// <summary>
        /// �������� ������� �� ��������� ������� ��������� ���������.
        /// </summary>
        /// <param name="index">������������� � ���� ������ ����������� ��������.</param>
        /// <param name="item">����� �������� ��� �������� � ��������� ��������. ����������� �������� null ��� ��������� �����.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///                 �������� <paramref name="index" /> ������ ����.����
        ///                 �������� ��������� <paramref name="index" /> ������ �������� �������� <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
        }

        /// <summary>
        /// ���������� ��������� ����� �������.
        /// </summary>
        /// <param name="itemIndex">������ ������������ ������ �������� </param>
        public override void CancelNew(int itemIndex)
        {
            base.CancelNew(itemIndex);
        }

        /// <summary>
        /// ��������� ��������� ����� ������� � ���������.
        /// </summary>
        /// <param name="itemIndex">������ ������������ ������ ��������.</param>
        public override void EndNew(int itemIndex)
        {
            base.EndNew(itemIndex);
        }

        /// <summary>
        /// ��������� ����� ������� � ����� ���������.
        /// </summary>
        /// <returns>
        /// �������, ����������� � ���������.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">��� ������ �������� �� ��������� � ����� ��������, ������������ � ������ <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
        protected override object AddNewCore()
        {
            return base.AddNewCore();
        }

        /// <summary>
        /// ��� ��������������� � ����������� ������ ��������� ��������; � ��������� ������ �������� ���������� <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <param name="prop">���������� <see cref="T:System.ComponentModel.PropertyDescriptor" />, ������������ �������� ��� ����������.</param>
        /// <param name="direction">���� �� �������� <see cref="T:System.ComponentModel.ListSortDirection" />.</param>
        /// <exception cref="T:System.NotSupportedException">����� �� ���������������� � ����������� ������. </exception>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            base.ApplySortCore(prop, direction);
        }

        /// <summary>
        /// ������� ����� ����������, 
        /// ����������� � ������� ������ <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />, ���� ���������� ����������� � ����������� ������; � ��������� ������ �������� ���������� <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">����� �� ���������������� � ����������� ������. </exception>
        protected override void RemoveSortCore()
        {
            base.RemoveSortCore();
        }

        /// <summary>
        /// ���������� ��������, ������������, �������� �� ������� <see cref="E:System.ComponentModel.BindingList`1.ListChanged" />.
        /// </summary>
        /// <returns>
        ///             �������� true, ���� ������� <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> ��������������, � ��������� ������ � �������� false. �������� �� ��������� � true.
        /// </returns>
        protected override bool SupportsChangeNotificationCore
        {
            get { return base.SupportsChangeNotificationCore; }
        }

        /// <summary>
        /// ���������� ��������, ������������, �������������� �� ���� ������� �����.
        /// </summary>
        /// <returns>
        ///             �������� true, ���� ������ ������������ �����; � ��������� ������ � �������� false. �������� �� ��������� � false.
        /// </returns>
        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        /// <summary>
        /// ���������� ��������, ������������, �������������� �� ���� ������� ����������.
        /// </summary>
        /// <returns>
        ///             �������� true, ���� ������ ������������ ����������; � ��������� ������ � �������� false. �������� �� ��������� � false.
        /// </returns>
        protected override bool SupportsSortingCore
        {
            get { return base.SupportsSortingCore; }
        }

        /// <summary>
        /// ���������� ��������, ������������, ����������� �� ������. 
        /// </summary>
        /// <returns>
        ///             �������� true, ���� ������ �����������; � ��������� ������ � �������� false. �������� �� ��������� � false.
        /// </returns>
        protected override bool IsSortedCore
        {
            get { return base.IsSortedCore; }
        }

        /// <summary>
        /// ���������� ���������� ��������, ������������ ��� ���������� ������, ���� ���������� ����������� � ����������� ������; � ��������� ������ ���������� null. 
        /// </summary>
        /// <returns>
        /// ���������� <see cref="T:System.ComponentModel.PropertyDescriptor" />, ������������ ��� ���������� ������.
        /// </returns>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return base.SortPropertyCore; }
        }

        /// <summary>
        /// ���������� ����������� ���������� ������.
        /// </summary>
        /// <returns>
        /// ���� �� �������� <see cref="T:System.ComponentModel.ListSortDirection" />. ��������� �� ��������� �������� <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />. 
        /// </returns>
        protected override ListSortDirection SortDirectionCore
        {
            get { return base.SortDirectionCore; }
        }

        /// <summary>
        /// ��� ��������� ��������
        /// </summary>
        protected abstract Type KeyType { get; }

        /// <summary>
        /// ��� �������� ���������� ��������
        /// </summary>
        /// <returns></returns>
        protected virtual string KeyPropertyName
        {
            get { return keyPropertyName; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("���������� = {0}", Count);
        }

        /// <summary>
        /// ��������� ����� ������� ��������, � �������� �������� ���������� ������� ����� ��������� ��������, ���� ����� ����������� � ����������� ������; � ��������� ������ � ���������� <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <returns>
        /// ������������� �� ���� ������ ��������, ���������������� ����������� ������� � ����������� �������� ��������.
        /// </returns>
        /// <param name="prop">������ <see cref="T:System.ComponentModel.PropertyDescriptor" /> ��� ���������� ������.</param>
        /// <param name="key">�������� �������� <paramref name="prop" /> ��� ������ ������������.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// ����� <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> �� ���������������� � ����������� ������.</exception>
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            try
            {
                // Simple iteration:
                for (int i = 0; i < Count; i++)
                {
                    T item = this[i];
                    if (prop.GetValue(item).Equals(key))
                    {
                        return i;
                    }
                }
                return -1; // Not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return -1;
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        ///<param name="objects"></param>
        public void AddRange(IEnumerable<T> objects)
        {
            foreach (T item in objects)
                Add(item);
        }

        ///<summary>
        /// 
        ///</summary>
        ///<param name="item"></param>
        public new void Add(T item)
        {
            if (!OnlyUnique || !Items.Contains(item))
            {
                base.Add(item);
            }
        }

        ///<summary>
        /// ������ ���������� ��������
        ///</summary>
        public bool OnlyUnique
        {
            get { return _onlyUnique; }
            set { _onlyUnique = value; }
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public T[] ToArray()
        {
            List<T> list = new List<T>(Items);
            return list.ToArray();
        }

        ///<summary>
        /// ������� �������� �������
        ///</summary>
        ///<param name="item"></param>
        ///<returns></returns>
        public bool TryAdd(T item)
        {
            try
            {
                Add(item);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// �������� ���������� ��������� ��������
        /// </summary>
        /// <returns></returns>
        protected PropertyDescriptor GetKeyPropertyDescriptor()
        {
            return TypeDescriptor.GetProperties(typeof(T))[KeyPropertyName];
        }

        ///<summary>
        /// �������� ������������ ��������� ����������� ��������
        ///</summary>
        ///<returns></returns>
        protected virtual bool CheckCorrectPropertyDescriptors()
        {
            PropertyDescriptor propertyDescriptor = GetKeyPropertyDescriptor();
            return propertyDescriptor != null && propertyDescriptor.PropertyType == KeyType;
        }


    }
}