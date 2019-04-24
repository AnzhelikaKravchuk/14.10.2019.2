using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter Tests
        private static IEnumerable<TestCaseData> IntegerFilterTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x % 2 == 0), new int[] { 2, 4 });
                yield return new TestCaseData(new int[] { -123, 645, -3545, 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x < 0), new int[] { -123, -3545 });
                yield return new TestCaseData(new int[] { -123, 645, -3545, 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x / 5 == 1), new int[] { 5 });
            }
        }

        private static IEnumerable<TestCaseData> StringFilterTestCases
        {
            get
            {
                yield return new TestCaseData(new string[] { "11", "one", "two", "three", "four" }, new Func<string, bool>(x => x.Length > 3), new string[] { "three", "four" });
                yield return new TestCaseData(new string[] { "11", "one", "two", "three", "four" }, new Func<string, bool>(x => x.Contains("w")), new string[] { "two" });
                yield return new TestCaseData(new string[] { "11", "one", "two", "three", "four" }, new Func<string, bool>(x => x.IndexOf("o") == 0), new string[] { "one" });
            }
        }

        private static IEnumerable<TestCaseData> FilterExceptionTestCases
        {
            get
            {
                yield return new TestCaseData(null, new Func<string, bool>(x => x.IndexOf("o") == 0), "source cannot be null.");
                yield return new TestCaseData(new string[] { }, null, "predicate cannot be null.");
            }
        }

        [TestCaseSource(nameof(IntegerFilterTestCases))]
        public static void IntegerFilterTest(int[] array, Func<int, bool> predicate, int[] expected)
        {
            CollectionAssert.AreEqual(expected, array.Filter(predicate));
        }

        [TestCaseSource(nameof(StringFilterTestCases))]
        public static void StringFilterTest(string[] array, Func<string, bool> predicate, string[] expected)
        {
            CollectionAssert.AreEqual(expected, array.Filter(predicate));
        }

        [TestCaseSource(nameof(FilterExceptionTestCases))]
        public void FilterExceptionTests(string[] array, Func<string, bool> predicate, string expected)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => array.Filter(predicate));
            Assert.AreEqual(expected, exception.Message);
        }
        #endregion

        //[Test]
        //public void CastTest(object[] collection, )
        //{
        //    CollectionAssert.AreEqual(new string[] { "1", "2", "3", "4", "5" }, Enumerable.CastTo<string>(collection));
        //}

        [Test]
        public void InvalidCastTest()
        {
            Assert.Throws<InvalidCastException>(() => CollectionAssert.AreEqual(new List<string> { "1", "2", "3", "4", "5" }, Enumerable.CastTo<string>(new ArrayList() { 1, 2, 3, 4, 5 })));
        }

        private static IEnumerable<TestCaseData> IntegerSortByTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 5, 1, 2, 3, 4 }, new Func<int, int>(x => x + 2), new int[] { 1, 2, 3, 4, 5 });
            }
        }

        [TestCaseSource(nameof(IntegerSortByTestCases))]
        public static void IntegerSortByTest(int[] array, Func<int, int> key, int[] expected)
        {
            CollectionAssert.AreEqual(expected, array.SortBy(key));
        }

    }
}