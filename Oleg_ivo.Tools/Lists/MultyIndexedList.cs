using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// Список, индексированный по ключам различных типов
    /// </summary>
    /// <typeparam name="T">Тип элементов списка</typeparam>
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
        /// Вызывает событие  <see cref="E:System.ComponentModel.BindingList`1.AddingNew" />.
        /// </summary>
        /// <param name="e">Объект <see cref="T:System.ComponentModel.AddingNewEventArgs" />, содержащий данные события. </param>
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="E:System.ComponentModel.BindingList`1.ListChanged" />.
        /// </summary>
        /// <param name="e">Объект <see cref="T:System.ComponentModel.ListChangedEventArgs" />, содержащий данные, относящиеся к событию. </param>
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

        /// <summary>
        /// Удаляет из коллекции все элементы.
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
        }

        /// <summary>
        /// Вставляет заданный элемент в список по указанному индексу.
        /// </summary>
        /// <param name="index">Отсчитываемый от нуля индекс, по которому следует вставить элемент.</param>
        /// <param name="item">Элемент, который 
        /// требуется вставить в список.</param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Удаляет элемент с указанным индексом.
        /// </summary>
        /// <param name="index">Индекс (с нуля) удаляемого элемента. </param>
        /// <exception cref="T:System.NotSupportedException">Только что добавленный элемент удаляется и свойство <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> устанавливается равным false. </exception>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        /// <summary>
        /// Заменяет элемент по заданному индексу указанным элементом.
        /// </summary>
        /// <param name="index">Отсчитываемый с нуля индекс заменяемого элемента.</param>
        /// <param name="item">Новое значение для элемента с указанным индексом. Допускается значение null для ссылочных типов.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///                 Значение <paramref name="index" /> меньше нуля.–или–
        ///                 Значение параметра <paramref name="index" /> больше значения свойства <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
        }

        /// <summary>
        /// Уничтожает ожидающий новый элемент.
        /// </summary>
        /// <param name="itemIndex">Индекс добавляемого нового элемента </param>
        public override void CancelNew(int itemIndex)
        {
            base.CancelNew(itemIndex);
        }

        /// <summary>
        /// Фиксирует ожидающий новый элемент в коллекции.
        /// </summary>
        /// <param name="itemIndex">Индекс добавляемого нового элемента.</param>
        public override void EndNew(int itemIndex)
        {
            base.EndNew(itemIndex);
        }

        /// <summary>
        /// Добавляет новый элемент в конец коллекции.
        /// </summary>
        /// <returns>
        /// Элемент, добавленный в коллекцию.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">Тип нового элемента не совпадает с типом объектов, содержащихся в списке <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
        protected override object AddNewCore()
        {
            return base.AddNewCore();
        }

        /// <summary>
        /// При переопределении в производном классе сортирует элементы; в противном случае вызывает исключение <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <param name="prop">Дескриптор <see cref="T:System.ComponentModel.PropertyDescriptor" />, определяющий свойство для сортировки.</param>
        /// <param name="direction">Одно из значений <see cref="T:System.ComponentModel.ListSortDirection" />.</param>
        /// <exception cref="T:System.NotSupportedException">Метод не переопределяется в производном классе. </exception>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            base.ApplySortCore(prop, direction);
        }

        /// <summary>
        /// Удаляет любую сортировку, 
        /// применяемую с помощью метода <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />, если сортировка реализована в производном классе; в противном случае вызывает исключение <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">Метод не переопределяется в производном классе. </exception>
        protected override void RemoveSortCore()
        {
            base.RemoveSortCore();
        }

        /// <summary>
        /// Возвращает значение, показывающее, включены ли события <see cref="E:System.ComponentModel.BindingList`1.ListChanged" />.
        /// </summary>
        /// <returns>
        ///             Значение true, если события <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> поддерживаются, в противном случае — значение false. Значение по умолчанию — true.
        /// </returns>
        protected override bool SupportsChangeNotificationCore
        {
            get { return base.SupportsChangeNotificationCore; }
        }

        /// <summary>
        /// Возвращает значение, показывающее, поддерживается ли этим списком поиск.
        /// </summary>
        /// <returns>
        ///             Значение true, если список поддерживает поиск; в противном случае — значение false. Значение по умолчанию — false.
        /// </returns>
        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        /// <summary>
        /// Возвращает значение, показывающее, поддерживается ли этим списком сортировка.
        /// </summary>
        /// <returns>
        ///             Значение true, если список поддерживает сортировку; в противном случае — значение false. Значение по умолчанию — false.
        /// </returns>
        protected override bool SupportsSortingCore
        {
            get { return base.SupportsSortingCore; }
        }

        /// <summary>
        /// Возвращает значение, показывающее, сортируется ли список. 
        /// </summary>
        /// <returns>
        ///             Значение true, если список сортируется; в противном случае — значение false. Значение по умолчанию — false.
        /// </returns>
        protected override bool IsSortedCore
        {
            get { return base.IsSortedCore; }
        }

        /// <summary>
        /// Возвращает дескриптор свойства, используемый для сортировки списка, если сортировка реализуется в производном классе; в противном случае возвращает null. 
        /// </summary>
        /// <returns>
        /// Дескриптор <see cref="T:System.ComponentModel.PropertyDescriptor" />, используемая для сортировки списка.
        /// </returns>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return base.SortPropertyCore; }
        }

        /// <summary>
        /// Возвращает направление сортировки списка.
        /// </summary>
        /// <returns>
        /// Одно из значений <see cref="T:System.ComponentModel.ListSortDirection" />. Значением по умолчанию является <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />. 
        /// </returns>
        protected override ListSortDirection SortDirectionCore
        {
            get { return base.SortDirectionCore; }
        }

        /// <summary>
        /// Тип ключевого свойства
        /// </summary>
        protected abstract Type KeyType { get; }

        /// <summary>
        /// Имя свойства управления индексом
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
            return string.Format("Количество = {0}", Count);
        }

        /// <summary>
        /// Выполняет поиск индекса элемента, у которого заданный дескриптор свойств равен заданному значению, если поиск реализуется в производном классе; в противном случае — исключение <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <returns>
        /// Отсчитываемый от нуля индекс элемента, соответствующего дескриптору свойств и содержащему заданное значение.
        /// </returns>
        /// <param name="prop">Список <see cref="T:System.ComponentModel.PropertyDescriptor" /> для выполнения поиска.</param>
        /// <param name="key">Значение параметр <paramref name="prop" /> для поиска соответствия.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// Метод <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> не переопределяется в производном классе.</exception>
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
        /// Только уникальные элементы
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
        /// Попытка добавить элемент
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
        /// Получить дескриптор ключевого свойства
        /// </summary>
        /// <returns></returns>
        protected PropertyDescriptor GetKeyPropertyDescriptor()
        {
            return TypeDescriptor.GetProperties(typeof(T))[KeyPropertyName];
        }

        ///<summary>
        /// Проверка правильности ключевого дескриптора свойства
        ///</summary>
        ///<returns></returns>
        protected virtual bool CheckCorrectPropertyDescriptors()
        {
            PropertyDescriptor propertyDescriptor = GetKeyPropertyDescriptor();
            return propertyDescriptor != null && propertyDescriptor.PropertyType == KeyType;
        }


    }
}