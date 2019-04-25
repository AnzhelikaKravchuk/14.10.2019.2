using System;
using System.Linq;
using System.Collections.Generic;
using PseudoEnumerable.Tests.Comparers;
using System.Numerics;
using NUnit;
using NUnit.Framework;
using System.Collections;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter tests
        [Test]
        public void Filter_SourceIsNull_ThrowsArgumentNullException()
        {
            int[] array = null;
            Assert.Catch<ArgumentNullException>(() => array.Filter(x => x > 1));
        }

        [Test]
        public void Filter_PredicateIsNull_ThrowsArgumentNullException()
        {
            int[] array = new int[] { 123, 456 };
            Assert.Catch<ArgumentNullException>(() => array.Filter(null));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6 })]
        [TestCase(new int[] { 0, 21, 41, 21, 76, 26 }, ExpectedResult = new int[] { 0, 76, 26 })]
        [TestCase(new int[] { 11, 21, 41, 21 }, ExpectedResult = new int[] { })]
        public IEnumerable<int> Filter_TSourceIsInteger_Tests(IEnumerable<int> source)
        {
            var result = new List<int>();
            foreach (var item in source.Filter(x => x % 2 == 0))
            {
                result.Add(item);
            }

            return result;
        }

        [TestCase(new string[] { "1", "2", "123456", "wdwdwdwwa" },
            new string[] { "123456", "wdwdwdwwa" })]
        [TestCase(new string[] { "123456", "12345678", "3435erfe" },
            new string[] { "123456", "12345678", "3435erfe" })]
        [TestCase(new string[] { "1", "2", "3" },
            new string[] { })]
        public void Filter_TSourceIsString_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var result = new List<string>();
            foreach (var item in source.Filter(x => x.Length >= 5))
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(result, expected);
        }
        #endregion

        #region Transform tests
        [Test]
        public void Transform_SourceIsNull_ThrowsArgumentNullException()
        {
            int[] array = null;
            Assert.Catch<ArgumentNullException>(() => array.Transform(x => x.ToString()));
        }

        [Test]
        public void Transform_TransformerIsNull_ThrowsArgumentNullException()
        {
            int[] array = new int[] { 123, 456};
            Assert.Catch<ArgumentNullException>(() => array.Transform<int, string>(null));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6, 8, 10, 12 })]
        [TestCase(new int[] { 0, 14, 21, 1, 3 }, ExpectedResult = new int[] { 0, 28, 42, 2, 6 })]
        [TestCase(new int[] { }, ExpectedResult = new int[] { })]
        public IEnumerable<int> Transform_TSourceIsInteger_Tests(IEnumerable<int> source)
        {
            var result = new List<int>();
            foreach (var item in source.Transform(x => x * 2))
            {
                result.Add(item);
            }

            return result;
        }

        [TestCase(new string[] { "ad", "aaa", "ddd", "ccccc" }, new int[] { 2, 3, 3, 5 })]
        [TestCase(new string[] { "", "efw", "1", "d3ac" }, new int[] { 0, 3, 1, 4 })]
        [TestCase(new string[] { "1", "333", "55555", "999999999", "" }, new int[] { 1, 3, 5, 9, 0 })]
        [TestCase(new string[] { }, new int[] { })]
        [TestCase(new string[] { "", "awdfgaa", "123456789" }, new int[] { 0, 7, 9 })]
        public void Transform_TSourceIsString_Tests(IEnumerable<string> source, IEnumerable<int> expected)
        {
            var result = new List<int>();
            foreach (var item in source.Transform(x => x.Length))
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(result, expected);
        }
        #endregion

        #region ForAll tests
        [Test]
        public void ForAll_SourceIsNull_ThrowsArgumentNullException()
        {
            int[] array = null;
            Assert.Catch<ArgumentNullException>(() => array.ForAll(x => x == 1));
        }

        [Test]
        public void ForAll_PredicateIsNull_ThrowsArgumentNullException()
        {
            int[] array = new int[] { 1, 2, 7};
            Assert.Catch<ArgumentNullException>(() => array.ForAll(null));
        }

        [TestCase(new string[] { "ad", "aaa", "ddd", "ccccc" }, true)]
        [TestCase(new string[] { "", "efw", "1", "d3ac" }, false)]
        [TestCase(new string[] { "asdasd", "123", " ", "    " }, true)]
        [TestCase(new string[] { }, true)]
        public void ForAll_Tests(IEnumerable<string> source, bool expected)
        {
            Assert.AreEqual(source.ForAll(x => x.Length != 0), expected);
        } 
        #endregion

        #region SortBy tests
        [TestCase(new string[] { "4444", "22", "666666", "1", "", "333" },
            new string[] { "", "1", "22", "333", "4444", "666666" })]
        [TestCase(new string[] { "a", "aaa", "a", "aa", "aaaaaaaaaaa", "aaaaaa" },
            new string[] { "a", "a", "aa", "aaa", "aaaaaa", "aaaaaaaaaaa" })]
        [TestCase(new string[] { },
            new string[] { })]
        [TestCase(new string[] { "12345", "54321" },
            new string[] { "12345", "54321" })]
        public void SortBy_KeyIsIntegerFedaultComparer_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var actual = source.SortBy(x => x.Length);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new string[] { "4444", "22", "666666", "1", "", "333" },
            new string[] { "666666", "4444", "333", "22", "1", "" })]
        [TestCase(new string[] { "abc", "a", "abcde", "abcdefg" },
            new string[] { "abcdefg", "abcde", "abc", "a" })]
        [TestCase(new string[] { },
            new string[] { })]
        [TestCase(new string[] { "one element" },
            new string[] { "one element" })]
        [TestCase(new string[] { "12345", "1234", "123", "12", "1", "" },
            new string[] { "12345", "1234", "123", "12", "1", "" })]
        public void SortBy_KeyIsIntegerCustomComparer_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var actual = source.SortBy(x => x.Length, new DescendingOrderComparer());
            CollectionAssert.AreEqual(expected, actual);
        } 
        #endregion

        #region SortByDescending tests
        [TestCase(new string[] { "4444", "22", "666666", "1", "", "333" },
            new string[] { "666666", "4444", "333", "22", "1", "" })]
        [TestCase(new string[] { "abc", "a", "abcde", "abcdefg" },
            new string[] { "abcdefg", "abcde", "abc", "a" })]
        [TestCase(new string[] { },
            new string[] { })]
        [TestCase(new string[] { "one element" },
            new string[] { "one element" })]
        [TestCase(new string[] { "12345", "1234", "123", "12", "1", "" },
            new string[] { "12345", "1234", "123", "12", "1", "" })]
        public void SortByDescending_KeyIsIntegerDefaultComparer_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var actual = source.SortByDescending(x => x.Length);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new string[] { "4444", "22", "666666", "1", "", "333" },
            new string[] { "", "1", "22", "333", "4444", "666666" })]
        [TestCase(new string[] { "a", "aaa", "a", "aa", "aaaaaaaaaaa", "aaaaaa" },
            new string[] { "a", "a", "aa", "aaa", "aaaaaa", "aaaaaaaaaaa" })]
        [TestCase(new string[] { },
            new string[] { })]
        public void SortByDescending_KeyIsIntegerCustomComparer_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var actual = source.SortByDescending(x => x.Length, new DescendingOrderComparer());
            CollectionAssert.AreEqual(expected, actual);
        } 
        #endregion

        #region IntegerGenerator tests
        [TestCase(0, 10)]
        [TestCase(126, -11)]
        [TestCase(9, 511)]
        [TestCase(1000, 100000)]
        [TestCase(1, 2)]
        [TestCase(0, 300)]
        [TestCase(40, 40)]
        [TestCase(11, 123)]
        public void IntegerGenerator_Tests(int count, int start)
        {
            var actual = Enumerable.IntegerGenerator(count, start);
            var expected = System.Linq.Enumerable.Range(start, count).Select(x => new BigInteger(x));
            CollectionAssert.AreEqual(expected, actual);
        }

        public void IntegerGenerator_CountIsNegative_ThrowArgumentOutOfRangeException(int count, int start) =>
            Assert.Catch<ArgumentOutOfRangeException>(() => Enumerable.IntegerGenerator(-1, 0));
        #endregion

        #region CastTo tests
        [TestCase(arg: new object[] { 12, "hi" })]
        [TestCase(arg: new object[] { "hi", 12 })]
        [TestCase(arg: new object[] { "hi", "12", "21", 123 })]
        [TestCase(arg: new object[] { "hi", "22", null })]
        public void CastToTests_WithNotStringElement_ThrowInvalidCastException(IEnumerable source)
        {
            
            using (var iterator = Enumerable.CastTo<string>(source).GetEnumerator())
            {
                try
                {
                    while (iterator.MoveNext()) { }
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.GetType() == typeof(InvalidCastException));
                }
            }
        }

        [TestCase(arg: new object[] { "hi", "22", "null" })]
        [TestCase(arg: new object[] { "123", "342", "adwwad", "wadaada" })]
        [TestCase(arg: new object[] { " ", "  ", "", "wadaada" })]
        [TestCase(arg: new object[] { })]
        public void CastTo_Tests(IEnumerable source)
        {
            var actual = source.CastTo<string>();
            var expected = source.Cast<string>();
            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion
    }
}