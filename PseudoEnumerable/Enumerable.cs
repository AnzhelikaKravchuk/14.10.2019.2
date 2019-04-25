using System;
using System.Collections;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class Enumerable
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">An <see cref="IEnumerable{TSource}"/> to filter.</param>
        /// <param name="predicate">A function to test each source element for a condition</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> that contains elements from the input
        ///     sequence that satisfy the condition.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Source cannot be null. Parameter name: { nameof(source) }.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"Predicate cannot be null. Parameter name: { nameof(predicate) }.");
            }

            return GetFilteredArray(source, predicate);
        }

        /// <summary>
        /// Transforms each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by transformer.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="transformer">A transform function to apply to each source element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TResult}"/> whose elements are the result of
        ///     invoking the transform function on each element of source.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="transformer"/> is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Array can not be null. Parameter name: { nameof(source) }.");
            }

            if (!source.GetEnumerator().MoveNext())
            {
                throw new ArgumentException($"The array should contain at least 1 element. Parameter name: { nameof(source) }");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"Predicate can not be null. Parameter name: { nameof(transformer) }.");
            }

            return GetTransformedArray(source, transformer);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            return SortBy(source, key, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according by using a specified comparer for a key .
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparer"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            if (comparer == null)
            {
                if (key is IComparable || key is IComparable<TKey>)
                {
                    comparer = Comparer<TKey>.Default;
                }
                {
                    throw new ArgumentNullException($"Comparer can not be null. Parameter name: { nameof(comparer) }.");
                }
            }

            if (source == null)
            {
                throw new ArgumentNullException($"Source can not be null. Parameter name: { nameof(source) }.");
            }

            if (key == null)
            {
                throw new ArgumentNullException($"Key cannot be null. Parameter name: { nameof(key) }.");
            }

            return SortByLazy(source, key, comparer);
        }

        private static IEnumerable<TSource> SortByLazy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            var sortedList = new List<TSource>(source);
            var comparerKeys = new ComparerStrategy<TSource, TKey>(key, comparer);
            sortedList.Sort(comparerKeys);

            foreach(var element in sortedList)
            {
                yield return element;
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            return SortByDescending(source, key, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according by using a specified comparer for a key .
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparer"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            IComparer<TKey> descendingComparer = new ReverseComparer<TKey>(comparer);

            return SortBy(source, key, descendingComparer);
        }

        /// <summary>
        /// Casts the elements of an IEnumerable to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <param name="source">The <see cref="IEnumerable"/> that contains the elements to be cast to type TResult.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains each element of the source sequence cast to the specified type.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidCastException">An element in the sequence cannot be cast to type TResult.</exception>
        public static IEnumerable<TResult> CastTo<TResult>(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Source cannot be null. Parameter name: { nameof(source) }.");
            }

            if (source is IEnumerable<TResult> resultSource)
            {
                return resultSource;
            }

            return CastToLazy<TResult>(source);
        }

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     true if every element of the source sequence passes the test in the specified predicate,
        ///     or if the sequence is empty; otherwise, false
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static bool ForAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Source cannot be null. Parameter name: { nameof(source) }.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"Predicate cannot be null. Parameter name: { nameof(predicate) }.");
            }

            foreach (var element in source)
            {
                if (!predicate.Invoke(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="getNext">The method to get next.</param>
        /// <returns></returns>
        public static IEnumerable<T> GenerateSequence<T>(T start, int count, Func<T,T> getNext)
        {
            if (count < 1)
            {
                throw new ArgumentException(nameof(count));
            }

            if (getNext == null)
            {
                throw new ArgumentNullException($"Generator cannot be null: { nameof(getNext) }.");
            }

            return GenerateSequenceLazy<T>(start, count, getNext);
        }

        #region private methods

        private static IEnumerable<T> GenerateSequenceLazy<T>(T start, int count, Func<T, T> getNext)
        {
            T current = start;
            for (int i = 0; i < count; i++)
            {
                T next = getNext(current);
                yield return next;
                current = next;
            }
        }

        private static IEnumerable<TResult> CastToLazy<TResult>(IEnumerable source)
        {
            foreach (var element in source)
            {
                yield return (TResult)element;
            }
        }

        private static IEnumerable<TSource> GetFilteredArray<TSource>(IEnumerable<TSource> array, Func<TSource, bool> filter)
        {
            foreach (var elemnt in array)
            {
                if (filter.Invoke(elemnt))
                {
                    yield return elemnt;
                }
            }
        }

        private static IEnumerable<TResult> GetTransformedArray<TSource, TResult>(IEnumerable<TSource> array, Func<TSource, TResult> transformer)
        {
            foreach (var element in array)
            {
                yield return transformer.Invoke(element);
            }
        }

        #endregion

        #region Adapters

        private class ReverseComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> comparer;

            public ReverseComparer(IComparer<T> comparer)
            {
                this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public int Compare(T x, T y)
            {
                return -comparer.Compare(x, y);
            }
        }

        private class ComparerStrategy<TSource, TKey> : IComparer<TSource>
        {
            private readonly Func<TSource, TKey> func;
            private readonly IComparer<TKey> comparer;

            public ComparerStrategy(Func<TSource, TKey> func, IComparer<TKey> comparer)
            {
                this.func = func ?? throw new ArgumentNullException(nameof(func));
                this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public int Compare(TSource x, TSource y)
            {
                return comparer.Compare(func(x), func(y));
            }
        }

        #endregion
    }
}