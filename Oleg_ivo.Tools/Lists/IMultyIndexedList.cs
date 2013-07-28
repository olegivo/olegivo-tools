using System;
using System.ComponentModel;

namespace Oleg_ivo.Tools.Lists
{
    /// <summary>
    /// Интерфейс списка, индексированного по ключам различных типов
    /// </summary>
    /// <typeparam name="T">Тип элементов списка</typeparam>
    public interface IMultyIndexedList<T> : IBindingList 
    {
        /// <summary>
        /// Тип объектов, хранящихся в списке
        /// </summary>
        Type ElementsType { get; }

        /// <summary>
        /// Число объектов
        /// </summary>
        int Count { get; }

        ///<summary>
        ///
        ///</summary>
        ///<param name="objects"></param>
        void AddRange(T[] objects);

        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="value"></param>
        void Add(T value);

        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="item"></param>
        void Remove(T item);

        /// <summary>
        /// Содержит ли каждый словарь данный объект?
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// Очистка
        /// </summary>
        void Clear();

        /// <summary>
        /// Преобразовать в массив элементов
        /// </summary>
        /// <returns></returns>
        T[] ToArray();
    }
}