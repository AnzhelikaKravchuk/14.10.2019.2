using System;
using System.Collections;
using System.Numerics;
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
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            Validation(source, predicate);

            return FilterEnumerator(source, predicate);
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
            Validation(source, transformer);

            return TransformEnumerator(source, transformer);
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
            Validation(source, key);

            return SortByEnumerator(source, key, Comparer<TKey>.Default);
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
            Validation(source, key);

            if (comparer is null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} is null.");
            }

            return SortByEnumerator(source, key, comparer);
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
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null.");
            }

            if (source is IEnumerable<TResult> result)
            {
                return result;
            }

            return CastToEnumerator<TResult>(source);
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
            Validation(source, predicate);

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
            Validation(source, key);

            return (TSource[])SortByEnumerator(source, key, Comparer<TKey>.Default).Reverse();
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according by using a specified comparer for a key.
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
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparer"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            Validation(source, key);

            if (comparer is null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} is null.");
            }

            return (TSource[])SortByEnumerator(source, key, comparer).Reverse();
        }

        /// <summary>
        /// Generates a sequence of integers from start value
        /// </summary>
        /// <param name="start">First element in sequence.</param>
        /// <param name="count">Count of integers in sequence.</param>
        /// <param name="generatorRule">Rule for generation next number.</param>
        /// <returns>A sequence of numbers which was generated with custom rure</returns>
        public static IEnumerable<int> GenerateNumbers(int start, uint count, Func<int, int> generatorRule)
        {
            int current = start;

            while (count-- > 0)
            {
                yield return current;
                current = generatorRule(current);
            }
        }

        #endregion

        #region Private Methods

        private static void Validation<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> @delegate)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null.");
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException($"{nameof(@delegate)} is null.");
            }
        }

        private static IEnumerable<TSource> FilterEnumerator<TSource>(IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (var element in source)
            {
                if (predicate(element))
                {
                    yield return element;
                }
            }
        }

        private static IEnumerable<TResult> TransformEnumerator<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            foreach (var element in source)
            {
                yield return transformer(element);
            }
        }

        private static IEnumerable<TSource> SortByEnumerator<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            for (int i = 0; i < source.Count() - 1; i++)
            {
                for (int j = i + 1; j < source.Count(); j++)
                {
                    if (comparer.Compare(key(source.ElementAt(i)), key(source.ElementAt(j))) > 0)
                    {
                        source = source.Swap(i, j);
                    }
                }
            }

            return source;
        }

        private static IEnumerable<TResult> CastToEnumerator<TResult>(IEnumerable source)
        {
            foreach (var element in source)
            {
                if (element is TResult)
                {
                    yield return (TResult)element;
                }
            }
        }

        private static IEnumerable<T> Swap<T>(this IEnumerable<T> source, int indexElement1, int indexElement2)
        {
            int currentIndex = 0;

            foreach (var item in source)
            {
                if (currentIndex == indexElement1)
                {
                    yield return source.ElementAt(indexElement2);
                }
                else if (currentIndex == indexElement2)
                {
                    yield return source.ElementAt(indexElement1);
                }
                else
                {
                    yield return item;
                }

                currentIndex++;
            }
        }

        private static int Count<T>(this IEnumerable<T> source)
        {
            int count = 0;

            foreach (var element in source)
            {
                count++;
            }

            return count;
        }

        private static T ElementAt<T>(this IEnumerable<T> source, int index)
        {
            foreach (var element in source)
            {
                if (index == 0)
                {
                    return element;
                }

                index--;
            }

            throw new ArgumentOutOfRangeException($"{index} was out of range.");
        }

        private static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
        {
            T[] result = new T[source.Count()];
            int i = result.Length - 1;
            foreach (var item in source)
            {
                result[i--] = item;
            }

            return result;
        }

        #endregion
    }
}