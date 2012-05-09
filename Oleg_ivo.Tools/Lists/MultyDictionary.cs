using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    ///<summary>
    ///
    ///</summary>
    public class MultyDictionary : Dictionary<Type, OneDictionary>, IBindingList
    {
        #region fields
        List<object> objects;
        BindingList<object> objectsBinding;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of items contained in the Oleg_ivo.Tools.Lists.MultyDictionary.
        /// </summary>
        public new int Count
        {
            get
            {
                CheckCounts();
                return ObjectsCount;
            }
        }

        /// <summary>
        /// Число словарей
        /// </summary>
        public int DictionariesCount
        {
            get { return base.Count; }
        }

        /// <summary>
        /// Проверка соответствия числа объектов в каждом словере и числа объектов в ObjectsCount
        /// </summary>
        private void CheckCounts()
        {
            int count = ObjectsCount;
            foreach (KeyValuePair<Type, OneDictionary> pair in this)
                if (count != pair.Value.Count)
                    throw new Exception(string.Format("Число объектов в словаре типа {0} не объективно", pair.Key));
        }

        /// <summary>
        /// Число объектов
        /// </summary>
        private int ObjectsCount
        {
            get
            {
                if (objectsBinding.Count != objects.Count)
                {
                    throw new Exception("Число объектов не объективно");
                }
                return objectsBinding.Count;
            }
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        public MultyDictionary()
        {
            objects = new List<object>();
            objectsBinding = new BindingList<object>();
            ListChanged += MultyDictionary_ListChanged;
        }

        private void MultyDictionary_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                    break;
                case ListChangedType.ItemAdded:
                    break;
                case ListChangedType.ItemDeleted:
                    break;
                case ListChangedType.ItemMoved:
                    break;
                case ListChangedType.ItemChanged:
                    break;
                case ListChangedType.PropertyDescriptorAdded:
                    break;
                case ListChangedType.PropertyDescriptorDeleted:
                    break;
                case ListChangedType.PropertyDescriptorChanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        #region Methods
        /// <summary>
        /// Добавить объект во все словари
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void AddObject(object value)
        {
            objects.Add(value);
            // добавляем во все словари
            foreach (KeyValuePair<Type, OneDictionary> pair in this)
                pair.Value.Add(value);
        }

        public new bool Remove(Type type)
        {
            throw new NotImplementedException("удаление словарей запрещено");
            //bool success = false;
            //if (ContainsKey(type))
            //{
            //    object value = this[type];
            //    success = base.Remove(type);
            //    if (success)
            //    {
            //        RemoveObject(value);
            //    }
            //}
            //return success;
        }

        /// <summary>
        /// Removes all keys and values from the System.Collections.Generic.Dictionary<TKey,TValue>
        /// </summary>
        public new void Clear()
        {
            objectsBinding.Clear();
            objects.Clear();
            foreach (KeyValuePair<Type, OneDictionary> pair in this)
                pair.Value.Clear();
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public IEnumerator GetObjectsEnumerator()
        {
            return objectsBinding.GetEnumerator();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public object[] ToArray()
        {
            return objects.ToArray();
        }

        #region Implementation of IList

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.IList"></see>.
        /// </summary>
        /// <returns>
        /// The position into which the new element was inserted.
        /// </returns>
        /// <param name="value">The <see cref="T:System.Object"></see> to add to the <see cref="T:System.Collections.IList"></see>. </param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception><filterpriority>2</filterpriority>
        public int Add(object value)
        {
            objectsBinding.Add(value);
            if (Contains(value))
            {
                AddObject(value);
            } 
            return IndexOf(value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.IList"></see> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Object"></see> is found in the <see cref="T:System.Collections.IList"></see>; otherwise, false.
        /// </returns>
        /// <param name="value">The <see cref="T:System.Object"></see> to locate in the <see cref="T:System.Collections.IList"></see>. </param><filterpriority>2</filterpriority>
        public bool Contains(object value)
        {
            return objectsBinding.Contains(value);
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.IList"></see>.
        /// </summary>
        /// <returns>
        /// The index of value if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="value">The <see cref="T:System.Object"></see> to locate in the <see cref="T:System.Collections.IList"></see>. </param><filterpriority>2</filterpriority>
        public int IndexOf(object value)
        {
            return objectsBinding.IndexOf(value);
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.IList"></see> at the specified index.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"></see> to insert into the <see cref="T:System.Collections.IList"></see>. </param>
        /// <param name="index">The zero-based index at which value should be inserted. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.IList"></see>. </exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception>
        /// <exception cref="T:System.NullReferenceException">value is null reference in the <see cref="T:System.Collections.IList"></see>.</exception><filterpriority>2</filterpriority>
        public void Insert(int index, object value)
        {
            objectsBinding.Insert(index, value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList"></see>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Object"></see> to remove from the <see cref="T:System.Collections.IList"></see>. </param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception><filterpriority>2</filterpriority>
        public void Remove(object value)
        {
            bool success = objectsBinding.Remove(value);
            if (success)
            {
                if (objects.Contains(value))
                {
                    objects.Remove(value);
                    // удаление из всех словарей
                    foreach (KeyValuePair<Type, OneDictionary> pair in this)
                    {
                        object key = pair.Value.GetKeyForObject(value);
                        if (key != null)
                            pair.Value.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.IList"></see> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.IList"></see>. </exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception><filterpriority>2</filterpriority>
        public void RemoveAt(int index)
        {
            objectsBinding.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.IList"></see>. </exception>
        /// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IList"></see> is read-only. </exception><filterpriority>2</filterpriority>
        public object this[int index]
        {
            get { return objectsBinding[index]; }
            set
            {
                if (AllowEdit)
                    objectsBinding[index] = value;
                else
                {
                    throw new Exception("Not allow edit Items in this list");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"></see> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.IList"></see> is read-only; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsReadOnly
        {
            get 
            { 
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"></see> has a fixed size.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.IList"></see> has a fixed size; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsFixedSize
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        #endregion

        #region Implementation of IBindingList
        /// <summary>
        /// Occurs when the list or an item in the list changes
        /// </summary>
        public event ListChangedEventHandler ListChanged
        {
            add 
            {
                objectsBinding.ListChanged += value;
            }
            remove 
            {
                objectsBinding.ListChanged -= value;
            }
        }

        /// <summary>
        /// Adds a new item to the list.
        /// </summary>
        /// <returns>
        /// The item added to the list.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.AllowNew"></see> is false. </exception>
        public object AddNew()
        {
            return objectsBinding.AddNew();
        }

        /// <summary>
        /// Adds the <see cref="T:System.ComponentModel.PropertyDescriptor"></see> to the indexes used for searching.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"></see> to add to the indexes used for searching. </param>
        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();// ToDo: Binding 
        }

        /// <summary>
        /// Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor"></see> and a <see cref="T:System.ComponentModel.ListSortDirection"></see>.
        /// </summary>
        /// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"></see> values. </param>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"></see> to sort by. </param>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting"></see> is false. </exception>
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();// ToDo: Binding 
        }

        /// <summary>
        /// Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor"></see>.
        /// </summary>
        /// <returns>
        /// The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor"></see>.
        /// </returns>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"></see> to search on. </param>
        /// <param name="key">The value of the property parameter to search for. </param>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSearching"></see> is false. </exception>
        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();// ToDo: Binding 
        }

        /// <summary>
        /// Removes the <see cref="T:System.ComponentModel.PropertyDescriptor"></see> from the indexes used for searching.
        /// </summary>
        /// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"></see> to remove from the indexes used for searching. </param>
        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();// ToDo: Binding 
        }

        /// <summary>
        /// Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"></see>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting"></see> is false. </exception>
        public void RemoveSort()
        {
            throw new NotImplementedException();// ToDo: Binding 
        }

        /// <summary>
        /// Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"></see>.
        /// </summary>
        /// <returns>
        /// true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"></see>; otherwise, false.
        /// </returns>
        public bool AllowNew
        {
            get
            {
                return objectsBinding.AllowNew;
            }
        }

        /// <summary>
        /// Gets whether you can update items in the list.
        /// </summary>
        /// <returns>
        /// true if you can update the items in the list; otherwise, false.
        /// </returns>
        public bool AllowEdit
        {
            get
            {
                return objectsBinding.AllowEdit;
            }
        }

        /// <summary>
        /// Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)"></see> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)"></see>.
        /// </summary>
        /// <returns>
        /// true if you can remove items from the list; otherwise, false.
        /// </returns>
        public bool AllowRemove
        {
            get
            {
                return objectsBinding.AllowRemove;
            }
        }

        /// <summary>
        /// Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged"></see> event is raised when the list changes or an item in the list changes.
        /// </summary>
        /// <returns>
        /// true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged"></see> event is raised when the list changes or when an item changes; otherwise, false.
        /// </returns>
        public bool SupportsChangeNotification
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        /// <summary>
        /// Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"></see> method.
        /// </summary>
        /// <returns>
        /// true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"></see> method; otherwise, false.
        /// </returns>
        public bool SupportsSearching
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        /// <summary>
        /// Gets whether the list supports sorting.
        /// </summary>
        /// <returns>
        /// true if the list supports sorting; otherwise, false.
        /// </returns>
        public bool SupportsSorting
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        /// <summary>
        /// Gets whether the items in the list are sorted.
        /// </summary>
        /// <returns>
        /// true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"></see> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort"></see> has not been called; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting"></see> is false. </exception>
        public bool IsSorted
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        /// <summary>
        /// Gets the <see cref="T:System.ComponentModel.PropertyDescriptor"></see> that is being used for sorting.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.PropertyDescriptor"></see> that is being used for sorting.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting"></see> is false. </exception>
        public PropertyDescriptor SortProperty
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        /// <summary>
        /// Gets the direction of the sort.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.ComponentModel.ListSortDirection"></see> values.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting"></see> is false. </exception>
        public ListSortDirection SortDirection
        {
            get
            {
                throw new NotImplementedException();// ToDo: Binding 
            }
        }

        #endregion
    }
}