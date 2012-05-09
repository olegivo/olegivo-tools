using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Oleg_ivo.Tools.Lists
{
    public class OneDictionary : Dictionary<object, object>
    {

        private PropertyInfo indexingValueProperty;
        protected string IndexingValuePropertyName;

        /// <summary>
        /// Свойство управления индексом. Позволяет генерировать индекс для объекта по его свойству.
        /// </summary>
        /// <returns></returns>
        public PropertyInfo IndexingValueProperty
        {
            get { return indexingValueProperty; }
            set { indexingValueProperty = value; }
        }

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public OneDictionary(string indexingValuePropertyName)
        {
            IndexingValuePropertyName = indexingValuePropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"></see> can contain.</param>
        /// <param name="indexingValueProperty"></param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public OneDictionary(int capacity, PropertyInfo indexingValueProperty)
            : base(capacity)
        {
            IndexingValueProperty = indexingValueProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"></see> for the type of the key.</param>
        /// <param name="indexingValueProperty"></param>
        public OneDictionary(IEqualityComparer<object> comparer, PropertyInfo indexingValueProperty)
            : base(comparer)
        {
            IndexingValueProperty = indexingValueProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"></see> can contain.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"></see> for the type of the key.</param>
        /// <param name="indexingValueProperty"></param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public OneDictionary(int capacity, IEqualityComparer<object> comparer, PropertyInfo indexingValueProperty)
            : base(capacity, comparer)
        {
            IndexingValueProperty = indexingValueProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"></see> and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"></see> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="indexingValueProperty"></param>
        /// <exception cref="T:System.ArgumentException">dictionary contains one or more duplicate keys.</exception>
        /// <exception cref="T:System.ArgumentNullException">dictionary is null.</exception>
        public OneDictionary(IDictionary<object, object> dictionary, PropertyInfo indexingValueProperty)
            : base(dictionary)
        {
            IndexingValueProperty = indexingValueProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"></see> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.
        /// </summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"></see> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"></see> for the type of the key.</param>
        /// <param name="indexingValueProperty"></param>
        /// <exception cref="T:System.ArgumentException">dictionary contains one or more duplicate keys.</exception>
        /// <exception cref="T:System.ArgumentNullException">dictionary is null.</exception>
        public OneDictionary(IDictionary<object, object> dictionary, IEqualityComparer<object> comparer, PropertyInfo indexingValueProperty)
            : base(dictionary, comparer)
        {
            IndexingValueProperty = indexingValueProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class with serialized data.
        /// </summary>
        /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"></see> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="indexingValueProperty"></param>
        protected OneDictionary(SerializationInfo info, StreamingContext context, PropertyInfo indexingValueProperty)
            : base(info, context)
        {
            IndexingValueProperty = indexingValueProperty;
        }

        private OneDictionary():this(null)
        {
        }

        #endregion

        /// <summary>
        /// Добавить объект с автогенерацией ключа
        /// </summary>
        /// <param name="value"></param>
        public void Add(object value)
        {
            object key = GetKeyForObject(value);
            if (key != null && !ContainsKey(key))
                Add(key, value);
        }

        /// <summary>
        /// Получить информацию о свойстве-ключегенераторе
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private PropertyInfo GetKeyGeneratorPropertyInfo(object value)
        {
            PropertyInfo propertyInfo = null;
            if(string.IsNullOrEmpty(IndexingValuePropertyName))
                throw new Exception("Не задано имя свойства управления индексом.");

            if(value!=null)
            {
                Type type = value.GetType();
                if (type != null)
                    propertyInfo = type.GetProperty(IndexingValuePropertyName);
            }
            return propertyInfo;
        }

        /// <summary>
        /// Получить ключ для объекта
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object GetKeyForObject(object value)
        {
            object key = null;
            if (IndexingValueProperty == null)
            {
                PropertyInfo propertyInfo = GetKeyGeneratorPropertyInfo(value);
                if (propertyInfo != null)
                    IndexingValueProperty = propertyInfo;
            }
            if (IndexingValueProperty != null)
                key = IndexingValueProperty.GetValue(value, null);
            return key;
        }
    }
}