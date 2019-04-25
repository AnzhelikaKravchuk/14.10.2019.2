using System;
using System.Collections;
using System.Numerics;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class Enumerable
    {
        #region Public Api
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
            ValidateFilter(source, predicate);
            return FilterHelper(source, predicate);
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
            ValidateTransform(source, transformer);
            return TransformHelper(source, transformer);
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
            ValidateSortBy(source, key, comparer);
            var sourceArray = new List<TSource>(source).ToArray();
            var keys = new List<TKey>(sourceArray.Transform<TSource, TKey>(x => key(x))).ToArray();
            Array.Sort(keys, sourceArray, comparer);
            return sourceArray;
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
            ValidateCastTo(source);
            foreach (var item in source)
            {
                yield return (TResult)item;
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
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null");
            }

            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates a sequence of integers.
        /// </summary>
        /// <param name="count">Count of numbers to generate.</param>
        /// <param name="start">The number from which the sequence is generated.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{BigInteger}"/> that contains <paramref name="count"/> integers
        ///     starting with <paramref name="start"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="count"/> is less than zero.</exception>
        public static IEnumerable<BigInteger> IntegerGenerator(BigInteger count, BigInteger start)
        {
            ValidateIntegerGenerator(count);
            for (BigInteger i = 0; i < count; i++)
            {
                yield return start++;
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
            var result = new List<TSource>(SortBy(source, key, Comparer<TKey>.Default));
            result.Reverse();
            return result;
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
            var result = new List<TSource>(SortBy(source, key, comparer));
            result.Reverse();
            return result;
        }
        #endregion

        #region Private methods
        private static IEnumerable<TSource> FilterHelper<TSource>(IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<TResult> TransformHelper<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        private static void ValidateFilter<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null");
            }
        }

        private static void ValidateTransform<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"{nameof(transformer)} is null");
            }
        }

        private static void ValidateCastTo(IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }
        }

        private static void ValidateSortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }

            if (key == null)
            {
                throw new ArgumentNullException($"{nameof(key)} is null");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} is null");
            }
        }

        private static void ValidateIntegerGenerator(BigInteger count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(count)} cannot be less than zero");
            }
        }
        #endregion
    }
}