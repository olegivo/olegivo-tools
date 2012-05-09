using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// Список, индексированный по ключу-строке
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringIndexedList<T> : MultyIndexedList<T>
    {
        #region constructors
        ///<summary>
        /// Список, индексированный по ключу-реплике
        ///</summary>
        ///<param name="keyGeneratorPropertyInfoName"></param>
        public StringIndexedList(string keyGeneratorPropertyInfoName)
            : this(keyGeneratorPropertyInfoName, new T[]{})
        {
        }

        ///<summary>
        /// Список, индексированный по ключу-реплике
        ///</summary>
        ///<param name="keyPropertyName"></param>
        ///<param name="objects"></param>
        public StringIndexedList(string keyPropertyName, IEnumerable<T> objects)
            : base(objects)
        {
            //this.guidPropertyDescriptor = new GuidPropertyDescriptor(keyPropertyName, null);
            this.keyPropertyName = keyPropertyName;
            if (!CheckCorrectPropertyDescriptors())
            {
                throw new Exception(string.Format("Неправильный дескриптор ключевого свойства (тип или имя)"));
            }
        }

        #endregion

        /// <summary>
        /// Индексатор по строке
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[string index]
        {
            get
            {
                T ret = default(T);
                PropertyDescriptor propertyDescriptor = GetKeyPropertyDescriptor();
                int i = FindCore(propertyDescriptor, index);
                if(i>=0)
                    ret = base[FindCore(propertyDescriptor, index)];
                else
                {
                    
                }
                return ret;
            }
            set
            {
                PropertyDescriptor propertyDescriptor = GetKeyPropertyDescriptor();
                int i = FindCore(propertyDescriptor, index);
                if(i>=0)
                    base[FindCore(propertyDescriptor, index)] = value;
            }
        }

        #region Overrides of MultyIndexedList<T>

        /// <summary>
        /// Тип ключевого свойства
        /// </summary>
        protected override Type KeyType
        {
            get { return typeof (string); }
        }

        #endregion
    }
}