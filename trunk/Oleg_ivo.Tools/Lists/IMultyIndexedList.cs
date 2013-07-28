using System;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// ��������� ������, ���������������� �� ������ ��������� �����
    /// </summary>
    /// <typeparam name="T">��� ��������� ������</typeparam>
    public interface IMultyIndexedList<T> : IBindingList 
    {
        /// <summary>
        /// ��� ��������, ���������� � ������
        /// </summary>
        Type ElementsType { get; }

        /// <summary>
        /// ����� ��������
        /// </summary>
        int Count { get; }

        ///<summary>
        ///
        ///</summary>
        ///<param name="objects"></param>
        void AddRange(T[] objects);

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="value"></param>
        void Add(T value);

        /// <summary>
        /// ������� ������
        /// </summary>
        /// <param name="item"></param>
        void Remove(T item);

        /// <summary>
        /// �������� �� ������ ������� ������ ������?
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// �������
        /// </summary>
        void Clear();

        /// <summary>
        /// ������������� � ������ ���������
        /// </summary>
        /// <returns></returns>
        T[] ToArray();
    }
}