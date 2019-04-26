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
            Func<TSource,bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Filter_(source, predicate);
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
                throw new ArgumentNullException(nameof(source));
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            return Transform_(source, transformer);
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
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return source.SortByKey(key, Comparer<TKey>.Default, 1);
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
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return source.SortByKey(key, Comparer<TKey>.Default, -1);
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
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return source.SortByKey(key, comparer, 1);
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
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return source.SortByKey(key, comparer, -1);
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
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var item in source)
            {
                if (item is TResult)
                {
                    yield return (TResult)item;
                }
                else
                {
                    throw new InvalidCastException(nameof(item));
                }
            }
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
            foreach(var item in source)
            {
                if (!predicate.Invoke(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generators the specified sequence beginning from the start element.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="func">A function to test each element for a condition.</param>
        /// <returns>Generated sequence according with <paramref name="func"/></returns>
        /// <exception cref="ArgumentException">Throws if <paramref name="count"/> less than 1.</exception>
        public static IEnumerable<int> Generator(int start, int count, Func<int, int> func)
        {
            if (count <= 0)
            {
                throw new ArgumentException(nameof(count));
            }

            return Generator_(start, count, func);
        }

        #region Private methods

        private static IEnumerable<TSource> SortByKey<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer, int reverse)
        {
            IList<TSource> copySource = source as IList<TSource>;

            foreach (var i in copySource)
            {
                for (int j = 0; j < copySource.Count - 1; j++)
                {
                    if (reverse * comparer.Compare(key(copySource[j]), key(copySource[j + 1])) > 0)
                    {
                        TSource str = copySource[j];
                        copySource[j] = copySource[j + 1];
                        copySource[j + 1] = str;
                    }
                }
            }

            return copySource;
        }

        private static IEnumerable<TSource> Filter_<TSource>(IEnumerable<TSource> source,
           Func<TSource, bool> predicate)
        {
            foreach (var i in source)
            {
                if (predicate(i))
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<TResult> Transform_<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {            
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        private static IEnumerable<int> Generator_(int start, int count, Func<int, int> func)
        {
            while (count > 0)
            {
                yield return func(start);
                start++;
                count--;
            }
        }

        #endregion
    }
}