using System;
using System.Collections;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class Enumerable
    {
        #region Public Methods

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
        public static IEnumerable<TSource> Filter<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            CheckParams(source, predicate);

            return FilterInternal();

            IEnumerable<TSource> FilterInternal()
            {
                foreach (var element in source)
                {
                    if (predicate(element))
                    {
                        yield return element;
                    }
                }
            }
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
        public static IEnumerable<TResult> Transform<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }

            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer), $"{nameof(transformer)} is null.");
            }

            return TransformInternal();

            IEnumerable<TResult> TransformInternal()
            {
                foreach (var element in source)
                {
                    yield return transformer(element);
                }
            }
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
        public static IEnumerable<TSource> SortBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key), $"{nameof(key)} is null.");
            }

            return SortBy<TSource, TKey>(source, key, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according by using a specified comparer for a key.
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
        public static IEnumerable<TSource> SortBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> key,
            IComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key), $"{nameof(key)} is null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer), $"{nameof(comparer)} is null.");
            }

            var sorted = new SortedDictionary<TKey, TSource>(comparer);
            foreach (var element in source)
            {
                sorted.Add(key(element), element);
            }

            return SortByInternal();

            IEnumerable<TSource> SortByInternal()
            {
                foreach (var element in sorted)
                {
                    yield return element.Value;
                }
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
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key), $"{nameof(key)} is null.");
            }

            return SortByDescending<TSource, TKey>(source, key, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according by using a specified comparer for a key.
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
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> key,
            IComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key), $"{nameof(key)} is null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer), $"{nameof(comparer)} is null.");
            }

            var sorted = new SortedList<TKey, TSource>(comparer);
            foreach (var element in source)
            {
                sorted.Add(key(element), element);
            }

            return SortByInternal();

            IEnumerable<TSource> SortByInternal()
            {
                for (int i = sorted.Count - 1; i >= 0; i--)
                {
                    yield return sorted.Values[i];
                }
            }
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
        public static IEnumerable<TResult> CastTo<TResult>(IEnumerable source)
        {
            if (source is IEnumerable<TResult> resultSource)
            {
                return resultSource;
            }

            CheckParams(source);

            return CastToIterator();

            IEnumerable<TResult> CastToIterator()
            {
                foreach (var item in source)
                {
                    yield return (TResult)item;
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
            CheckParams(source, predicate);

            foreach (var element in source)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the sequence of integer numbers.
        /// </summary>
        /// <param name="start">The start number.</param>
        /// <param name="count">The count of numbers.</param>
        /// <returns>The sequence of integer numbers.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if sequence is out of range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="count"/> is out of range.</exception>
        public static IEnumerable<int> GetSequence(int start, int count)
        {
            long sum = (long)start + (long)count;
            if (sum > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException($"Sequence is out of range. Check {start} and {count} parameters.");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is out of range.");
            }

            return GetSequenceInternal();

            IEnumerable<int> GetSequenceInternal()
            {
                for (int i = start; i < start + count; i++)
                {
                    yield return i;
                }
            }
        }

        #endregion

        #region Internal Methods
        
        private static void CheckParams(IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }
        }

        private static void CheckParams<TSource>(IEnumerable<TSource> source, object predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} is null.");
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }
        }

        #endregion
    }
}