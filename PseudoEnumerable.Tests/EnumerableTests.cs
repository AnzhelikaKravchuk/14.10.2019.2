using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {

        #region Filter

        [TestCase(new int[] { 2, 3, 4, 6 }, new int[] { 2, 4, 6 })]
        public void Filter_IntegerArray_Modulo(int[] array, int[] expected)
        {
            var actual = array.Filter(x => x % 2 == 0);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 2, 3, 4, 6 }, new int[] { 4, 6 })]
        public void Filter_IntegerArray(int[] array, int[] expected)
        {
            var actual = array.Filter(x => x * x > 10);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        public void Filter_NullArray_Throws_ArgumentNullException(int[] array)
            => Assert.Throws<ArgumentNullException>(() => array.Filter(x => x % 2 == 0));

        [TestCase(null)]
        public void Filter_IntegerArrayAndNullFunc_Throws_ArgumentNullException(int[] array)
           => Assert.Throws<ArgumentNullException>(() => array.Filter(null));
        
        #endregion

        #region Transform

        [TestCase(new int[] { 2, 4, 6 }, new string[] { "2", "4", "6" })]
        public void Transform_IntegerArrayToStringArray(int[] array, string[] expected)
        {
            var actual = array.Transform(x => x.ToString());

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 2, 4, 6 }, new int[] { 4, 16, 36 })]
        public void Transform_IntegerArray_DoubledIntegerArray(int[] array, int[] expected)
        {
            var actual = array.Transform(x => Math.Pow(x, 2));

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        public void Transform_NullArrayAndFunc_Throws_ArgumentNullException(int[] array)
           => Assert.Throws<ArgumentNullException>(() => array.Transform(x => Math.Pow(x, 2)));

        [TestCase(null)]
        public void Transform_IntegerArrayAndNullFunc_Throws_ArgumentNullException(int[] array)
          => Assert.Throws<ArgumentNullException>(() => array.Transform<int, int>(null));

        #endregion

        #region SortBy

        [TestCase(new string[] { "abc", "ab", "abcd", "a" }, new string[] { "a", "ab", "abc", "abcd" })]
        public void SortBy_CorrectInputStringArray(string[] source, string[] expected)
        {
            var actual = source.SortBy(x => x.Length);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new string[] { "abc", "ab", "abcd", "a" }, new string[] {"abcd", "abc", "ab", "a" })]
        public void SortByDescending_CorrectInputStringArray(string[] source, string[] expected)
        {
            var actual = source.SortByDescending(x => x.Length);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 5, 1, 4 }, new int[] { 1, 4, 5 })]
        public void SortBy_CorrectInputIntegerArray(int[] source, int[] expected)
        {
            var actual = source.SortBy(x => Math.Pow(x, 2));

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 5, 1, 4 }, new int[] { 5, 4, 1 })]
        public void SortByDescending_CorrectInputIntegerArray(int[] source, int[] expected)
        {
            var actual = source.SortByDescending(x => Math.Pow(x, 2));

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 64, 48, 47 }, new char[] { '/', '0', '@' })]
        public void SortBy_CorrectInputIntegerArray_CharArray(int[] source, char[] expected)
        {
            var actual = source.SortBy(x => (char)x);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 5, 16, 4, 2 }, new int[] { 2, 5, 4, 16 })]
        public void SortBy_CorrectInputIntegerArray_(int[] source, int[] expected)
        {
            CustomComparer comparer = new CustomComparer();

            var actual = source.SortBy(x => (int)Math.Pow(x, 2), comparer);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 5, 16, 4, 2 }, new int[] { 16, 5, 4, 2 })]
        public void SortByDescending_CorrectInputIntegerArray_(int[] source, int[] expected)
        {
            CustomComparer comparer = new CustomComparer();

            var actual = source.SortByDescending(x => (int)Math.Pow(x, 2), comparer);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region ForAll

        [TestCase(new int[] { 2, 8, 4, 6 }, ExpectedResult = true)]
        public bool Filter_IntegerArray_(int[] array)
            => array.ForAll(x => x % 2 == 0);

        #endregion

        #region CastTo

        [Test]
        public void CastTo_GenericArray()
        {
            ArrayList list = new ArrayList
            {
                2,
                2
            };

            CollectionAssert.AreEqual(new int[] { 2, 2 }, list.CastTo<int>());
        }

        #endregion

        #region Generator

        [TestCase(2, 3)]
        public void Generator_CorrectStartIndexAndCount_GeneratedSequence(int start, int count)
        {
            CollectionAssert.AreEqual(new int[] { 4, 9, 16 }, Enumerable.Generator(start, count, x => x * x));
        }

        [Test]
        public void Generator_CorrectStartIndexAndIncorrectCount_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Enumerable.Generator(4, 0, null));
        }

        #endregion
    }
}