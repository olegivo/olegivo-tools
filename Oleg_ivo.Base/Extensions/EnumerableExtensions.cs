using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.PrismExtensions.Autofac;

namespace Oleg_ivo.PrismExtensions.Extensions
{
    public static class EnumerableExtensions
    {

        /// <summary>
        ///  Функция проверяет, что все элементы последовательности равны
        /// </summary>
        /// <typeparam name="TSource">Тип элементов последовательности</typeparam>
        /// <param name="source">Последовательность элементов TSource</param>
        /// <returns>true, если все элементы последовательности равны, или же последовательность пустая</returns>
        /// <remarks>Элементы сравниваются, используя <see cref="EqualityComparer{T}.Default">EqualityComparer(TSource).Default</see></remarks>
        public static bool AllSame<TSource>(this IEnumerable<TSource> source)
        {

            return AllSame(source, null);
        }

        /// <summary>
        ///  Функция проверяет, что все элементы последовательности равны, используя переданный <paramref name="comparer"/>
        /// </summary>
        /// <typeparam name="TSource">Тип элементов последовательности</typeparam>
        /// <param name="source">Последовательность элементов TSource</param>
        /// <param name="comparer">Компарер для сравнения элементов. Если передан null, используется 
        ///     <see cref="EqualityComparer{T}.Default">EqualityComparer(TSource).Default</see>
        /// </param>
        /// <returns>true, если все элементы последовательности равны, или же последовательность пустая</returns>
        public static bool AllSame<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            Enforce.ArgumentNotNull(source, "this");

            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            var e = source.GetEnumerator();
            if (!e.MoveNext()) return true;

            var first = e.Current;

            while (e.MoveNext())
                if (!comparer.Equals(first, e.Current)) return false;

            return true;
        }

        /// <summary>
        ///  Для каждого элемента последовательности выполняет действие <paramref name="action"/>, 
        ///  и возвращает последовательность тех же элементов
        /// </summary>
        /// <typeparam name="TSource">Тип элементов последовательности</typeparam>
        /// <param name="source">Последовательность</param>
        /// <param name="action">Действие, выполняемое над каждым элементом</param>
        /// <returns></returns>
        /// <remarks>
        ///  Действие применяется не сразу, а по мере перечисления элементов.
        ///  При повторном перечислении возвращенной последовательности действие будет применено повторно.
        ///  Эквивалентно <c>source.Select(v => { action(v); return v; })</c>
        /// </remarks>
        public static IEnumerable<TSource> Apply<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            return source.Select(v => { action(v); return v; });
        }


        /// <summary>
        ///  Исключает из последовательности неопределенные элементы.
        /// </summary>
        /// <typeparam name="TSource">Тип элементов последовательности</typeparam>
        /// <param name="source">Последовательность</param>
        /// <returns>Последовательность, не содержащую элементов, равных null</returns>
        public static IEnumerable<TSource> ExcludeNull<TSource>(this IEnumerable<TSource> source)
        {
            return source.Where(v => v != null);
        }

        /// <summary>
        ///  Исключает из последовательности элементы, для которых переданный 
        ///  селектор возвращает null.
        /// </summary>
        /// <typeparam name="TSource">Тип элементов последовательности</typeparam>
        /// <typeparam name="TItem">Тип возвращаемый селектором</typeparam>
        /// <param name="source">Последовательность</param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ExcludeNull<TSource, TItem>(this IEnumerable<TSource> source, Func<TSource, TItem> selector)
        {
            return source.Where(v => selector(v) != null);
        }


        /// <summary>
        ///  Исключает из последовательности, содержащей результаты группировки,
        ///  группы с ключами, равными null.
        /// </summary>
        /// <typeparam name="TKey">Тип ключа группы</typeparam>
        /// <typeparam name="TElement">Тип элемента группы</typeparam>
        /// <param name="source">Результат группировки</param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey, TElement>> ExcludeWithNullKey<TKey, TElement>(
            this IEnumerable<IGrouping<TKey, TElement>> source)
        {
            return source.ExcludeNull(g => g.Key);
        }
 

        /// <summary>
        ///  Исключает из последовательности <see cref="System.Nullable"/> элементов неопределенные элементы
        ///  и возвращает последовательность не-Nullable элементов.
        /// </summary>
        /// <typeparam name="TSource">Базовый Value-тип для Nullable-элемента последовательности</typeparam>
        /// <param name="source">Последовательность Nullable элементов</param>
        /// <returns>Последовательность не-Nullable элементов, из которой исключены элементы, равные null</returns>
        public static IEnumerable<TSource> ExcludeNullable<TSource>(this IEnumerable<Nullable<TSource>> source) where TSource : struct
        {
            return from s in source where s.HasValue select s.Value;
        }


        /// <summary>
        /// Returns the elements of the specified sequence or the specified 
        /// value in a singleton collection if the sequence is empty.
        /// </summary>
        /// <param name="source" />
        /// <param name="defaultValueProvider">The function to be called for default value when the sequence is empty</param>
        /// <remarks>
        ///  Default value is obtained only if the sequence is empty.
        /// </remarks>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource> defaultValueProvider)
        {
            Enforce.ArgumentNotNull(source, "source");

            if (defaultValueProvider == null) 
                return source.DefaultIfEmpty();

            return DefaultIfEmptyYield(source, defaultValueProvider);
        }

        private static IEnumerable<TSource> DefaultIfEmptyYield<TSource>(
            IEnumerable<TSource> source,
            Func<TSource> defaultValueProvider)
        {
            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                    yield return defaultValueProvider.Invoke();
                else
                    do { yield return e.Current; } while (e.MoveNext());
            }
        }


        /// <summary>
        ///  Возвращает значение, связанное с ключом <paramref name="key"/>, или default(TValue), если ключ отсутствует в словаре
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>В отличие от TryGetValue этому методу не требуется out-параметр</remarks>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }

        /// <summary>
        ///  Возвращает значение, связанное с ключом <paramref name="key"/>, или <paramref name="defaultValue"/>, если ключ отсутствует в словаре
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">Значение, возвращаемое, если ключ не найден.</param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }


        /// <summary>
        ///  Возвращает значение связанное с ключом <paramref name="key"/> в словаре, где значениями являются value-типы,
        ///  или null, если ключ отсутствует в словаре.
        /// </summary>
        /// <typeparam name="TKey">Тип ключа в словаре</typeparam>
        /// <typeparam name="TValue">Тип значения в словаре, value-тип</typeparam>
        /// <param name="dictionary">Словарь, где значениями выступают value-типы</param>
        /// <param name="key">Ключ искомого значения</param>
        /// <returns>Возвращает значение в словаре, соответствующее ключу, или null, если ключ не найден. 
        /// При этом значение типа <typeparamref name="TValue"/> преобразуется в <see cref="Nullable{T}">TValue?</see></returns>
        public static TValue? GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue: struct 
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? (TValue?)value : null;
        }

        #region Memoized Enumerable

        /// <summary>
        ///  Мемоизирует (лениво буферизует) последовательность
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">Исходная последовательность</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>
        ///  Также, как и большинство методов <see cref="System.Linq.Enumerable"/> этот выполняется отложенно, 
        ///  считывая элементы из исходной последовательности по мере необходимости, однако при повторных 
        ///  проходах по мемоизованной последовательности элементы считываются уже из буфера, а не из
        ///  исходной последовательности.
        /// </para>
        /// <para>
        ///  Таким образом, гарантируется что при повторных перечислениях мемоизованной последовательности, будут
        ///  возвращены те же элементы, что и в первый раз, даже если перечисление исходной последовательности 
        ///  волатильно. 
        /// </para>
        /// <para>
        ///  Если перечисление исходной последовательности связано с большими вычислительными затратами,
        ///  они затрачиваются только один раз, а не при каждом перечислении.
        /// </para>
        /// <para>
        ///  Если при перечислении исходной последовательности происходит исключение, то в мемоизованной последовательности
        ///  будет происходить исключение при каждом проходе, на одном и том же месте и того же типа, что и в исходной.
        /// </para>
        /// <para>
        ///  Буферизация элементов исходной последовательности производится лениво, по мере обращения к ним, 
        ///  что в отличие от метода <see cref="System.Linq.Enumerable.ToArray{T}"/> позволяет мемоизовывать и 
        ///  бесконечные последовательности.
        /// </para>
        /// </remarks>
        public static IEnumerable<TSource> Memoize<TSource>(this IEnumerable<TSource> source)
        {
            return (source is MemoizedEnumerable<TSource>) ? source : new MemoizedEnumerable<TSource>(source);
        }


        private class MemoizedEnumerable<T> : IEnumerable<T>, IDisposable
        {

            public MemoizedEnumerable(IEnumerable<T> source)
            {
                this.source = source;
                buffer = new List<T>();
            }

            private IEnumerable<T> source;
            private IEnumerator<T> sourceEnum;
            private List<T> buffer;
            private Exception enumerationException;
            private bool isDisposed;

            public IEnumerator<T> GetEnumerator()
            {
                lock (this)
                {
                    if (isDisposed)
                        throw new ObjectDisposedException("MemoizedEnumerable");

                    if (sourceEnum == null && source != null)
                        sourceEnum = source.GetEnumerator();
                }
                return new Enumerator(this);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private bool TryGetValueAt(int index, out T value)
            {
                value = default(T);

                lock (this)
                {
                    if (isDisposed)
                        throw new ObjectDisposedException("MemoizedEnumerable");

                    if (index < buffer.Count)
                    {
                        value = buffer[index];
                        return true;
                    }

                    if (sourceEnum == null)
                        return false;

                    if (enumerationException != null)
                        throw enumerationException;

                    try
                    {
                        while (index >= buffer.Count)
                        {
                            if (!sourceEnum.MoveNext())
                            {
                                DisposeSource();
                                return false;
                            }
                            buffer.Add(sourceEnum.Current);
                        }
                    }
                    catch (Exception ex)
                    {
                        enumerationException = ex;
                        throw;
                    }

                    value = sourceEnum.Current;
                    return true;
                }

            }

            public void Dispose()
            {
                lock (this)
                {
                    isDisposed = true;
                    DisposeSource();
                    buffer = null;
                }
            }

            private void DisposeSource() {
                DisposeObject(ref sourceEnum);
                source = null;
            }

            private static void DisposeObject<TDisposable>(ref TDisposable disposable) where TDisposable : IDisposable
            {
                if (disposable != null)
                {
                    disposable.Dispose();
                    disposable = default(TDisposable);
                }
            }




            public class Enumerator : IEnumerator<T>
            {
                public Enumerator(MemoizedEnumerable<T> source)
                {
                    this.source = source;
                    Reset();
                }

                private MemoizedEnumerable<T> source;
                private int index;
                private T m_current;

                public T Current
                {
                    get { return m_current; }
                }

                object System.Collections.IEnumerator.Current
                {
                    get { return m_current; }
                }

                public void Dispose()
                {
                }


                public bool MoveNext()
                {
                    index += 1;
                    return source.TryGetValueAt(index, out m_current);
                }

                public void Reset()
                {
                    index = -1;
                    m_current = default(T);
                }

            }
        }

        #endregion


        #region Lazy Enumerable

        /// <summary>
        ///  Интерпретирует функцию <paramref name="sourceProvider"/>, возвращающую последовательность <typeparamref name="TSource"/>, 
        ///  саму как последовательность.
        /// </summary>
        /// <typeparam name="TSource">Тип элементов последовательности</typeparam>
        /// <param name="sourceProvider">Функция, возвращающая последовательность <see cref="IEnumerable{TSource}"/></param>
        /// <returns>
        ///  Возвращает последовательность элементов <typeparamref name="TSource"/>, при первом перечислении которой 
        ///  будет произведен вызов функции <paramref name="sourceProvider"/> для получения исходной последовательности.
        /// </returns>
        public static IEnumerable<TSource> ToEnumerable<TSource>(this Func<IEnumerable<TSource>> sourceProvider)
        {
            return new LazyEnumerable<TSource>(sourceProvider);
        }


        /// <seealso cref="EnumerableExtensions.ToEnumerable{TSource}"/>
        private class LazyEnumerable<T> : IEnumerable<T>
        {

            public LazyEnumerable(Func<IEnumerable<T>> sourceProvider)
            {
                m_SourceProvider = Enforce.ArgumentNotNull(sourceProvider, "sourceProvider");
            }

            // NOTE: Use Lazy<IEnumerable<T>>
            private readonly Func<IEnumerable<T>> m_SourceProvider;
            private IEnumerable<T> source;

            #region Implementation of IEnumerable

            public IEnumerator<T> GetEnumerator()
            {
                if (source == null)
                {
                    source = m_SourceProvider.Invoke();
                    Enforce.ArgumentNotNull(source, "source");
                }
                return source.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        } 

        #endregion

        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int length)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (length <= 0) throw new ArgumentOutOfRangeException("length", length, "Partition length must be greater or equal to 1");

            var partition = new List<T>(length);

            foreach (var item in source)
            {
                partition.Add(item);

                if (partition.Count == length)
                {
                    yield return partition.AsReadOnly();
                    partition = new List<T>(length);
                }
            }

            if (partition.Count > 0)
                yield return partition.AsReadOnly();
        }


        #region JoinToString

        public static string JoinToString<T>(this IEnumerable<T> source, string separator)
        {
            return source.JoinToString(separator, v => v.ToString());
        }

        [Obsolete("Use JoinToStringFormat")]
        public static string JoinToString<T>(this IEnumerable<T> source, string itemFormat, string separator) where T : IFormattable
        {
            return JoinToStringFormat(source, separator, itemFormat);
        }

        [Obsolete("Use JoinToStringFormat")]
        public static string JoinToString<T>(this IEnumerable<T> source, string itemFormat, IFormatProvider formatProvider, string separator) where T : IFormattable
        {
            return JoinToStringFormat(source, separator, itemFormat, formatProvider);
        }

        public static string JoinToStringFormat<T>(this IEnumerable<T> source, string separator, string format) 
        {
            return source.JoinToStringFormat(separator, format, null);
        }

        public static string JoinToStringFormat<T>(this IEnumerable<T> source, string separator, string format, IFormatProvider formatProvider)
        {
            if (format.IndexOf("{0", StringComparison.InvariantCulture) >= 0 && format.IndexOf("}", StringComparison.InvariantCulture) > 0)
            {
                return source.JoinToString(separator, v => String.Format(formatProvider, format, v));
            }

            return source.JoinToString(separator, v =>
            {
                var f = v as IFormattable;
                return f != null ? f.ToString(format, formatProvider) : v.ToString();
            });
        }

        [Obsolete("Use overload with different order of parameters")]
        public static string JoinToString<T>(this IEnumerable<T> source, Func<T, string> _converter, string _separator)
        {
            return JoinToString(source, _separator, _converter);
        }

        public static string JoinToString<T>(this IEnumerable<T> source, string separator, Func<T, string> converter)
        {
            if (typeof(T) == typeof(string))
                return String.Join(separator, source.Select(converter).ToArray());
            else
            {
                var e = source.GetEnumerator();
                if (!e.MoveNext())
                    return String.Empty;
                var sb = new StringBuilder();
                sb.Append(converter(e.Current));
                while (e.MoveNext())
                {
                    sb.Append(separator);
                    sb.Append(converter(e.Current));
                }
                return sb.ToString();
            }
        } 
        #endregion

        #region Min/Max or default

        /// <summary>
        ///  Возвращает минимальное значение последовательности или default(TSource), если последовательность пуста.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns>Минимальное значение последовательности или default(TSource)</returns>
        /// <remarks>
        ///  В отличие от стандартной функции <see cref="Enumerable.Min{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> 
        ///  не выбрасывает исключение, если последовательность пуста, а возвращает значение элемента по умолчанию.
        /// </remarks>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty().Min();
        }

        /// <summary>
        ///  Применяет к каждому элементу последовательности преобразование и возвращает минимальное результирующее 
        ///  значение или default(TResult), если последовательность пуста.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <remarks>
        ///  В отличие от стандартной функции <see cref="Enumerable.Min{TSource,TResult}"/> 
        ///  не выбрасывает исключение, если последовательность пуста, а возвращает значение типа результата по умолчанию.
        /// </remarks>
        public static TResult MinOrDefault<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            return source.Select(selector).DefaultIfEmpty().Min();
        }

        /// <summary>
        ///  Возвращает максимальное значение последовательности или default(TSource), если последовательность пуста.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns>Максимальное значение последовательности или default(TSource)</returns>
        /// <remarks>
        ///  В отличие от стандартной функции <see cref="Enumerable.Max{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> 
        ///  не выбрасывает исключение, если последовательность пуста, а возвращает значение элемента по умолчанию.
        /// </remarks>
        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty().Max();
        }

        /// <summary>
        ///  Применяет к каждому элементу последовательности преобразование и возвращает максимальное результирующее 
        ///  значение или default(TResult), если последовательность пуста.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <remarks>
        ///  В отличие от стандартной функции <see cref="Enumerable.Max{TSource,TResult}"/> 
        ///  не выбрасывает исключение, если последовательность пуста, а возвращает значение типа результата по умолчанию.
        /// </remarks>
        public static TResult MaxOrDefault<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            return source.Select(selector).DefaultIfEmpty().Max();
        } 

        #endregion

        #region Min/Max over

        public static TSource MinOver<TSource, TMember>(this IEnumerable<TSource> source, Func<TSource, TMember> selector)
        {
            return FindMaxOrMinElement(source, selector, -1);
        }

        public static TSource MaxOver<TSource, TMember>(this IEnumerable<TSource> source, Func<TSource, TMember> selector)
        {
            return FindMaxOrMinElement(source, selector, 1);
        }

        private static TSource FindMaxOrMinElement<TSource, TMember>(this IEnumerable<TSource> source, Func<TSource, TMember> selector, int comparisonDirection)
        {
            var found = default(KeyValuePair<TSource, TMember>?);
            var comparer = Comparer<TMember>.Default;
            foreach (var s in source)
                if (found.HasValue)
                {
                    var m = selector(s);
                    if (comparer.Compare(m, found.Value.Value) * comparisonDirection > 0)
                        found = new KeyValuePair<TSource, TMember>(s, m);
                }
                else
                    found = new KeyValuePair<TSource, TMember>(s, selector(s));

            if (found.HasValue)
                return found.Value.Key;

            var noResult = default(TSource);
            if (noResult == null)
                return noResult;
            else
                throw new InvalidOperationException("Enumerable contains no elements");
        }

        #endregion

        #region Shuffling

        /// <summary>
        ///  Возвращает элементы последовательности в случайном порядке
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>
        ///  Используется алгоритм Fisher–Yates shuffle.
        ///  Производится начальная буферизация и затем внутренний буфер перемешивается по мере
        ///  продвижения по итератору.
        /// </returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(
            this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");
            return source.ShuffleIterator(rng);
        }


        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (int n = 0; n < buffer.Count; n++)
            {
                int k = rng.Next(n, buffer.Count);
                yield return buffer[k];
                buffer[k] = buffer[n];
            }
        }
        #endregion

    }



}
