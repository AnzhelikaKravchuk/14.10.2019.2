using System;
using System.Collections;
using System.Collections.Generic;
namespace PseudoEnumerable
{
    public static class Enumerable
    {
        /// <summary>
        /// Generates <paramref name="count"/> integral numbers from number <paramref name="start"/>.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count of numbers.</param>
        /// <returns>
        ///    An <see cref="IEnumerable{int}"/> that contains <paramref name="count"/> numbers
        ///    from the <paramref name="start"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="count"/> is less or equals 0.
        /// </exception>
        public static IEnumerable<int> GenerateIntegralNumbers(int start, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException($"{count} is less or equals 0");
            }

            return GenerateIntegralNumbersCore();

            IEnumerable<int> GenerateIntegralNumbersCore()
            {
                for (int i = 0; i < count; i++)
                {
                    yield return start++;
                }
            }
        }


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
            return FilterCore();

            IEnumerable<TSource> FilterCore()
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
            Validation(source, transformer);
            return TransformCore();

            IEnumerable<TResult> TransformCore()
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
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            Validation(source, key);
            var comparer = GetDefaultComparer<TKey>();
            return SortByCore(source, key, comparer);
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
            var comparer = GetDefaultComparer<TKey>();
            return SortByCore(source, key, new ReverseComparer<TKey>(comparer));
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
            Validation(source, key, comparer);
            CheckIfComparable<TSource>();
            return SortByCore(source, key, comparer);
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
            Validation(source, key, comparer);
            CheckIfComparable<TSource>();
            return SortByCore(source, key, new ReverseComparer<TKey>(comparer));
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
                throw new ArgumentNullException($"{nameof(source)} is null");
            }

            if (source is IEnumerable<TResult> resultSource)
            {
                return resultSource;
            }

            return CastToCore();

            IEnumerable<TResult> CastToCore()
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
            Validation(source, predicate);
            bool result = true;
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private static void Validation<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }
        }

        private static void Validation<T>(IEnumerable<T> source, object predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null");
            }

            Validation(source);
        }

        private static void Validation<T>(IEnumerable<T> source, object predicate, object comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} is null");
            }

            Validation(source, predicate);
        }

        private static void CheckIfComparable<T>()
        {
            Comparer<T> defaultComparer = Comparer<T>.Default;
            if (defaultComparer is null)
            {
                throw new InvalidOperationException($"{nameof(defaultComparer)} can be null only if type has its own implementation of IComparable");
            }
        }

        private static Comparer<T> GetDefaultComparer<T>()
        {
            CheckIfComparable<T>();
            return Comparer<T>.Default;
        }

        private static IEnumerable<TSource> SortByCore<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer = null)
        {
            var _comparer = comparer ?? GetDefaultComparer<TKey>();
            List<TSource> list = new List<TSource>(source);
            list.Sort((x, y) => _comparer.Compare(key(x), key(y)));
            foreach (var element in list)
            {
                yield return element;
            }
        }

        internal class ReverseComparer<T> : IComparer<T>
        {
            private IComparer<T> comparer;

            internal ReverseComparer(IComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            public int Compare(T x, T y)
            {
                return -comparer.Compare(x, y);
            }
        }
    }
}