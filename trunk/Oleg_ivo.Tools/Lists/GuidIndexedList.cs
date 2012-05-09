using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// ������, ��������������� �� �����-�������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GuidIndexedList<T> : MultyIndexedList<T>
    {
        #region constructors
        ///<summary>
        /// ������, ��������������� �� �����-�������
        ///</summary>
        ///<param name="keyGeneratorPropertyInfoName"></param>
        public GuidIndexedList(string keyGeneratorPropertyInfoName)
            : this(keyGeneratorPropertyInfoName, new T[]{})
        {
        }

        ///<summary>
        /// ������, ��������������� �� �����-�������
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
                throw new Exception(string.Format("������������ ���������� ��������� �������� (��� ��� ���)"));
            }
        }

        #endregion

        /// <summary>
        /// ���������� �� �������
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
        /// ��� ��������� ��������
        /// </summary>
        protected override Type KeyType
        {
            get { return typeof (Guid); }
        }

        #endregion
    }
}