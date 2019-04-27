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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return FilterIterator();

            IEnumerable<TSource> FilterIterator()
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        yield return item;
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
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            return TransformIterator();

            IEnumerable<TResult> TransformIterator()
            {
                foreach (var item in source)
                {
                    yield return transformer(item);
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
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            TSource[] array = ToArray(source);
            Array.Sort(array, new ComparerAdapter<TSource, TKey>(key, comparer));

            return array;
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return CastIterator();

            IEnumerable<TResult> CastIterator()
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
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
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted in descending order according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return SortBy(source, key, new ReverseOrderComparerDecorator<TKey>(Comparer<TKey>.Default));
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
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted in descending order according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparer"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return SortBy(source, key, new ReverseOrderComparerDecorator<TKey>(comparer));
        }

        /// <summary>
        /// Generates a sequence of integral numbers within a specified range.
        /// </summary>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="count">The number of sequential integers to generate.</param>
        /// <returns>
        ///    An <see cref="IEnumerable{Int32}"/> that contains a range of sequential integral numbers. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0 or <paramref name="start"/> + 
        /// <paramref name="count"/> - 1 is larger than MaxValue.</exception>
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("Count is less than 0.");
            }

            if ((long)start + count - 1 > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException("start + count - 1 is larger than MaxValue.");
            }

            return RangeIterator();

            IEnumerable<int> RangeIterator()
            {
                int last = start + count;

                for(int i = start; i < last; i++)
                {
                    yield return i;
                }
            }
        }


        #region Private methods

        private static T[] ToArray<T>(IEnumerable<T> source)
        {
            T[] array = new T[0];
            int count = 0;

            foreach (var item in source)
            {
                if (array.Length == 0)
                {
                    array = new T[4];
                }
                else if (array.Length == count)
                {
                    T[] tempArray = new T[count << 1];
                    Array.Copy(array, tempArray, count);
                    array = tempArray;
                }

                array[count] = item;
                count++;
            }

            T[] resultArray = new T[count];
            Array.Copy(array, resultArray, count);

            return resultArray;
        }

        #endregion

        #region Nested classes

        private class ComparerAdapter<TSource, TKey> : IComparer<TSource>
        {
            private readonly Func<TSource, TKey> key;
            private readonly IComparer<TKey> comparer;

            public ComparerAdapter(Func<TSource, TKey> key, IComparer<TKey> comparer)
            {
                this.key = key ?? throw new ArgumentNullException(nameof(key));
                this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public int Compare(TSource x, TSource y)
            {
                return comparer.Compare(key(x), key(y));
            }
        }

        private class ReverseOrderComparerDecorator<T> : IComparer<T>
        {
            private readonly IComparer<T> comparer;

            public ReverseOrderComparerDecorator(IComparer<T> comparer)
            {
                this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public int Compare(T x, T y)
            {
                return comparer.Compare(y, x);
            }
        }

        #endregion
    }
}