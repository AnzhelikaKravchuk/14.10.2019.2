using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter tests

        [Test]
        public void FilterTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Func<int, bool> predicate = x => x % 2 == 0;
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));
        }

        [Test]
        public void FilterTest_Predicate_IsNull_Throw_ArgumentNullException()
        {
            List<string> source = new List<string>() { "one", "two ", "forty two", "zero" };
            Func<string, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));
        }

        [TestCase(new int[] { }, ExpectedResult = new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = new int[] { 2, 4 })]
        [TestCase(new int[] { 1, 1, 1, 1 }, ExpectedResult = new int[0])]
        public IEnumerable<int> FilterTests_WithIntArray_IsEvenPredicate(int[] source)
        {
            return source.Filter(x => x % 2 == 0);
        }

        [TestCase(new object[0], ExpectedResult = new string[0])]
        [TestCase(new object[] { "one", "two ", "forty two", "zero" }, ExpectedResult = new string[0])]
        [TestCase(new object[] { "one", "two ", "Forty two", "zero" }, ExpectedResult = new string[] { "Forty two" })]
        public IEnumerable<string> FilterTests_WithStringList_StartsWithCapital(params string[] source)
        {
            List<string> list = new List<string>(source);
            return list.Filter(s => char.IsUpper(s[0]));
        }

        #endregion

        #region Transform tests

        [Test]
        public void TransformTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            string[] source = null;
            Func<string, int> transformer = s => s.Length;
            Assert.Throws<ArgumentNullException>(() => source.Transform(transformer));
        }

        [Test]
        public void TransformTest_Transformer_IsNull_Throw_ArgumentNullException()
        {
            List<string> source = new List<string>() { "one", "two ", "forty two", "zero" };
            Func<string, ulong> transformer = null;
            Assert.Throws<ArgumentNullException>(() => source.Transform(transformer));
        }

        [TestCase(new object[] { "1234", "321", "1", "555" }, ExpectedResult = new int[] { 4660, 801, 1, 1365 })]
        [TestCase(new object[] { "FFFF", "FFF", "FF", "F" }, ExpectedResult = new int[] { 65535, 4095, 255, 15 })]
        [TestCase(new object[] { "abcd", "a8b", "f", "a8" }, ExpectedResult = new int[] { 43981, 2699, 15, 168 })]
        public IEnumerable<int> TransformTests_StringArrayToDecimalIntArray(params string[] source)
        {
            return source.Transform(s => int.Parse(s, System.Globalization.NumberStyles.HexNumber));
        }

        #endregion

        #region SortBy with (Func<TSource, TKey> key) argument tests

        [Test]
        public void SortByWithKeyTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Func<int, int> key = i => i;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(key));
        }

        [Test]
        public void SortByWithKeyTest_Key_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Func<int, int> key = null;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(key));
        }

        [TestCase(new object[] { "aaa", "aa", "a" }, ExpectedResult = new string[] { "a", "aa", "aaa" })]
        [TestCase(new object[] { "b", "bb", "aaa", "aa", "a" }, ExpectedResult = new string[] { "b", "a", "bb", "aa", "aaa" })]
        [TestCase(new object[] { "a", "aa", "aaa", "bb", "b" }, ExpectedResult = new string[] { "a", "b", "aa", "bb", "aaa" })]
        public static IEnumerable<string> SortByTests_ByStringLength(params string[] source)
        {
            return source.SortBy((s) => s.Length);
        }

        [TestCase(new int[0], ExpectedResult = new int[0])]
        [TestCase(new int[] { 5, 4, 3, 2, 1 }, ExpectedResult = new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { -100, 100, 1, 1, 2, -100 }, ExpectedResult = new int[] { -100, -100, 1, 1, 2, 100 })]

        public static IEnumerable<int> SortByTests_Int_ByValue(int[] source)
        {
            return source.SortBy(x => x);
        }

        #endregion

        #region SortBy with (Func<TSource, TKey> key, IComparer<TKey> comparer) arguments tests

        [Test]
        public void SortByWithKeyAndComparerTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Func<int, int> key = i => i;
            IComparer<int> comparer = Comparer<int>.Default;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(key, comparer));
        }

        [Test]
        public void SortByWithKeyAndComparerTest_Key_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Func<int, int> key = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(key, comparer));
        }

        [Test]
        public void SortByWithKeyAndComparerTest_Comparer_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Func<int, int> key = i => i;
            IComparer<int> comparer = null;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(key, comparer));
        }

        [TestCase(new object[] { "aaa", "234", "baassd", "vbnf", "ohg" }, ExpectedResult = new string[] { "aaa", "ohg", "234", "baassd", "vbnf" })]
        [TestCase(new object[] { "red", "orange", "blue", "green", "aqua" }, ExpectedResult = new string[] { "orange", "aqua", "red", "blue", "green"})]
        public static IEnumerable<string> SortByTests_StartsWithVowelIsFirst(params string[] source)
        {
            return source.SortBy((s) => s[0], new TestComparers.VowelFirst());
        }

        #endregion

        #region Cast to tests

        [Test]
        public void CastToTest_Source_IsNull_Throws_ArgumentNullException()
        {
            IEnumerable<object> source = null;

            Assert.Throws<ArgumentNullException>(() => source.CastTo<object>());
        }

        [Test]
        public void CastToTest_SourceHasInvalidToCastElements_Throws_InvalidCastException()
        {
            IEnumerable<object> source = new object[] { 1, 2, "3", "42" };

            Assert.Throws<InvalidCastException>(() => source.CastTo<int>().ToArray());
        }

        [Test]
        public void CastToTest_SourceIsValidArrayOfInt()
        {
            IEnumerable<object> source = new object[] { 1, 2, 3, 42 };
            IEnumerable<int> expected = new int[] { 1, 2, 3, 42 };

            CollectionAssert.AreEqual(expected, source.CastTo<int>());
        }

        [Test]
        public void CastToTest_SourceIsValidArrayOfStrings()
        {
            IEnumerable<object> source = new object[] { "Foo", "Bar" };
            IEnumerable<string> expected = new string[] { "Foo", "Bar" };

            CollectionAssert.AreEqual(expected, source.CastTo<string>());
        }

        #endregion

        #region ForAll tests

        [Test]
        public void ForAllTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Func<int, bool> predicate = x => x % 2 == 0;
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));
        }

        [Test]
        public void ForAllTest_Predicate_IsNull_Throw_ArgumentNullException()
        {
            List<string> source = new List<string>() { "one", "two ", "forty two", "zero" };
            Func<string, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));
        }

        [TestCase(new int[] { }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = false)]
        [TestCase(new int[] { 1, 1, 1, 1 }, ExpectedResult = true)]
        public bool ForAllTests_WithIntArray_AllOdd(int[] source)
        {
            return source.ForAll(x => x % 2 != 0);
        }

        [TestCase(new object[0], ExpectedResult = true)]
        [TestCase(new object[] { "one", "two ", "forty two", "zero" }, ExpectedResult = true)]
        [TestCase(new object[] { "one", "two ", "Forty two", "zero", "three" }, ExpectedResult = false)]
        public bool ForAllTests_WithStringList_AllHasO(params string[] source)
        {
            List<string> list = new List<string>(source);
            return list.ForAll(s => s.Contains('o'));
        }

        #endregion

        #region SortByDescending with (Func<TSource, TKey> key) argument tests

        [Test]
        public void SortByDescendingWithKeyTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Func<int, int> key = i => i;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending(key));
        }

        [Test]
        public void SortByDescendingWithKeyTest_Key_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Func<int, int> key = null;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending(key));
        }

        [TestCase(new object[] { "aaa", "aa", "a" }, ExpectedResult = new string[] { "aaa", "aa", "a" })]
        [TestCase(new object[] { "b", "bb", "aaa", "aa", "a" }, ExpectedResult = new string[] { "aaa", "bb", "aa", "b", "a" })]
        [TestCase(new object[] { "a", "aa", "aaa", "bb", "b" }, ExpectedResult = new string[] { "aaa", "aa", "bb", "a", "b" })]
        public static IEnumerable<string> SortByDescendingTests_ByStringLength(params string[] source)
        {
            return source.SortByDescending((s) => s.Length);
        }

        [TestCase(new int[0], ExpectedResult = new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = new int[] { 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { -100, 100, 1, 1, 2, -100 }, ExpectedResult = new int[] { 100, 2, 1, 1, -100, -100 })]

        public static IEnumerable<int> SortByDescendingTests_Int_ByValue(int[] source)
        {
            return source.SortByDescending(x => x);
        }

        #endregion

        #region SortByDescending with (Func<TSource, TKey> key, IComparer<TKey> comparer) arguments tests

        [Test]
        public void SortByDescendingWithKeyAndComparerTest_Sourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Func<int, int> key = i => i;
            IComparer<int> comparer = Comparer<int>.Default;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending(key, comparer));
        }

        [Test]
        public void SortByDescendingWithKeyAndComparerTest_Key_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Func<int, int> key = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending(key, comparer));
        }

        [Test]
        public void SortByDescendingWithKeyAndComparerTest_Comparer_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Func<int, int> key = i => i;
            IComparer<int> comparer = null;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending(key, comparer));
        }

        [TestCase(new object[] { "aaa", "234", "baassd", "vbnf", "ohg" }, ExpectedResult = new string[] { "234", "baassd", "vbnf", "aaa", "ohg" })]
        [TestCase(new object[] { "red", "orange", "blue", "green", "aqua" }, ExpectedResult = new string[] { "red", "blue", "green", "orange", "aqua" })]
        public static IEnumerable<string> SortByDescendingTests_StartsWithVowelIsFirst(params string[] source)
        {
            return source.SortByDescending((s) => s[0], new TestComparers.VowelFirst());
        }

        #endregion

        #region Range tests

        [Test]
        public void RangeTest_Count_IsNegative_Throw_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(5, -5));
        }

        [Test]
        public void RangeTest_StartPlusCountMinusOne_LargerThanMaxValue_Throw_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(int.MaxValue, 2));
        }

        [TestCase(0, 0, ExpectedResult = new int[0])]
        [TestCase(0, 5, ExpectedResult = new int[] { 0, 1, 2, 3, 4 })]
        [TestCase(-100, 2, ExpectedResult = new int[] { -100, -99 })]
        [TestCase(int.MinValue, 2, ExpectedResult = new int[] { int.MinValue, int.MinValue + 1 })]
        public IEnumerable<int> RangeTests(int start, int count)
        {
            return new List<int>(Enumerable.Range(start, count));
        }

        #endregion
    }
}