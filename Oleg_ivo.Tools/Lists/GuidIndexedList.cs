using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// Список, индексированный по ключу-реплике
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GuidIndexedList<T> : MultyIndexedList<T>
    {
        #region constructors
        ///<summary>
        /// Список, индексированный по ключу-реплике
        ///</summary>
        ///<param name="keyGeneratorPropertyInfoName"></param>
        public GuidIndexedList(string keyGeneratorPropertyInfoName)
            : this(keyGeneratorPropertyInfoName, new T[]{})
        {
        }

        ///<summary>
        /// Список, индексированный по ключу-реплике
        ///</summary>
        ///<param name="keyGeneratorPropertyInfoName"></param>
        ///<param name="objects"></param>
        public GuidIndexedList(string keyGeneratorPropertyInfoName, IEnumerable<T> objects)
            : base(objects)
        {
            //this.guidPropertyDescriptor = new GuidPropertyDescriptor(keyPropertyName, null);
            this.keyPropertyName = keyGeneratorPropertyInfoName;
            if (!CheckCorrectPropertyDescriptors())
            {
                throw new Exception(string.Format("Неправильный дескриптор ключевого свойства (тип или имя)"));
            }
        }

        #endregion

        /// <summary>
        /// Индексатор по реплике
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual T this[Guid index]
        {
            get
            {
                PropertyDescriptor propertyDescriptor = GetKeyPropertyDescriptor();
                int i = FindCore(propertyDescriptor, index);
                if (i >= 0) 
                    return base[i];

                return default(T);
            }
            set
            {
                PropertyDescriptor propertyDescriptor = GetKeyPropertyDescriptor();
                int i = FindCore(propertyDescriptor, index);
                if(i >= 0)
                    base[i] = value;
            }
        }

        #region Overrides of MultyIndexedList<T>

        /// <summary>
        /// Тип ключевого свойства
        /// </summary>
        protected override Type KeyType
        {
            get { return typeof (Guid); }
        }

        #endregion
    }
}