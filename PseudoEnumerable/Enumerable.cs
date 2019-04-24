using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

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
            FilterCheckingExceptions(source, predicate);

            return FilterLazyEnumeration(source, predicate);
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
            TransformCheckingExceptions(source, transformer);

            return TransformLazyEnumeration(source, transformer);
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
            var comparer = Comparer<TKey>.Default;

            SortByCheckingExceptions(source, key, comparer);

            return SortBy(source, key, comparer);
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
            SortByCheckingExceptions(source, key, comparer);

            var list = new List<KeyValuePair<TKey, TSource>>();

            foreach (var item in source)
            {
                list.Add(new KeyValuePair<TKey, TSource>(key(item), item));
            }

            list.Sort((pair1, pair2) => comparer.Compare(pair1.Key, pair2.Key));

            return SortByLazyEnumeration(list);
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
            CastToCheckingExceptions(source);

            return CastToLazyEnumeration<TResult>(source); ;
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
            ForAllCheckingExceptions(source, predicate);

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
            var comparer = Comparer<TKey>.Default;

            SortByCheckingExceptions(source, key, comparer);

            return SortByDescending(source, key, comparer);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order descending by using a specified comparer for a key .
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
            SortByCheckingExceptions(source, key, comparer);

            var list = new List<KeyValuePair<TKey, TSource>>();

            foreach (var item in source)
            {
                list.Add(new KeyValuePair<TKey, TSource>(key(item), item));
            }

            list.Sort((pair1, pair2) => comparer.Compare(pair1.Key, pair2.Key) * (-1));

            return SortByLazyEnumeration(list);
        }

        /// <summary>
        /// Generates the sequence.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        ///     An <see cref="IEnumerable{TSource}"/> from start count times.
        /// <exception cref="ArgumentException">Throws if <paramref name="count"/> is less or equal then 0.</exception>
        public static IEnumerable<BigInteger> GenerateSequence(int start, int count)
        {
            GenerateSequenceCheckingExceptions(count);

            return GenerateSequenceLazyEnumeration(start, count);
        }

        #region Private Methods

        private static bool FilterCheckingExceptions<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} must be not null.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} must be not null.");
            }

            return true;
        }

        private static bool ForAllCheckingExceptions<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} must be not null.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} must be not null.");
            }

            return true;
        }

        private static IEnumerable<TSource> FilterLazyEnumeration<TSource>(this IEnumerable<TSource> source,
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

        private static IEnumerable<TResult> CastToLazyEnumeration<TResult>(IEnumerable source)
        {
            foreach (var item in source)
            {
                yield return (TResult)item;
            }
        }

        private static bool CastToCheckingExceptions(IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} must be not null.");
            }

            return true;
        }

        private static bool TransformCheckingExceptions<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} must be not null.");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"{nameof(transformer)} must be not null.");
            }

            return true;
        }

        private static IEnumerable<TResult> TransformLazyEnumeration<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        private static bool SortByCheckingExceptions<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} must be not null.");
            }

            if (key == null)
            {
                throw new ArgumentNullException($"{nameof(key)} must be not null.");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} must be not null.");
            }

            return true;
        }

        private static IEnumerable<TSource> SortByLazyEnumeration<TSource, TKey>(IList<KeyValuePair<TKey, TSource>> list)
        {
            foreach (var item in list)
            {
                yield return item.Value;
            }
        }

        private static IEnumerable<BigInteger> GenerateSequenceLazyEnumeration(int start, int count)
        {
            BigInteger begin = start;

            for (int i = 0; i < count; i++)
            {
                yield return begin++;
            }
        }

        private static bool GenerateSequenceCheckingExceptions(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException($"{nameof(count)} must be greater than 0.");
            }

            return true;
        }

        #endregion
    }
}