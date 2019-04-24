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

        #region CastTo Tests
        [Test]
        public void CastTest()
        {
            ArrayList collection = new ArrayList() { "1", "2", "3", "4", "5" };

            CollectionAssert.AreEqual(new string[] { "1", "2", "3", "4", "5" }, Enumerable.CastTo<string>(collection));
        }

        [Test]
        public void InvalidCastTest()
        {
            Assert.Throws<InvalidCastException>(() => CollectionAssert.AreEqual(new List<string> { "1", "2", "3", "4", "5" }, Enumerable.CastTo<string>(new ArrayList() { 1, 2, 3, 4, 5 })));
        } 
        #endregion

        #region SortBy Tests
        private static IEnumerable<TestCaseData> IntegerSortByTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 5, 1, 2, 3, 4 }, new Func<int, int>(x => x + 2), new int[] { 1, 2, 3, 4, 5 });
                yield return new TestCaseData(new int[] { 5, 1, 2, 3, 4 }, new Func<int, int>(x => x % 2), new int[] { 2, 4, 5, 1, 3 });
            }
        }

        private static IEnumerable<TestCaseData> StringSortByTestCases
        {
            get
            {
                yield return new TestCaseData(new string[] { "five", "sdfgsdfgfg", "one", "two", "three" }, new Func<string, int>(x => x.Length), new string[] { "one", "two", "five", "three", "sdfgsdfgfg" });
            }
        }

        private static IEnumerable<TestCaseData> SortByExceptionTestCases
        {
            get
            {
                yield return new TestCaseData(null, new Func<string, bool>(x => x.IndexOf("o") == 0), "source cannot be null.");
                yield return new TestCaseData(new string[] { }, null, "key cannot be null.");
            }
        }

        [TestCaseSource(nameof(IntegerSortByTestCases))]
        public static void IntegerSortByTest(int[] array, Func<int, int> key, int[] expected)
        {
            CollectionAssert.AreEqual(expected, array.SortBy(key));
        }

        [TestCaseSource(nameof(StringSortByTestCases))]
        public static void StringSortByTest(string[] array, Func<string, int> key, string[] expected)
        {
            CollectionAssert.AreEqual(expected, array.SortBy(key));
        }

        [TestCaseSource(nameof(SortByExceptionTestCases))]
        public void SortByExceptionTests(string[] array, Func<string, bool> key, string expected)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => array.SortBy(key));
            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void SortBy_Throws_ArgumentNullException_If_Comparer_IsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new int[] { }.SortBy(x => x, null));
            Assert.AreEqual("comparer cannot be null.", exception.Message);
        }
        #endregion

        #region SortByDescending Tests
        private static IEnumerable<TestCaseData> IntegerSortByDescendingTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 5, 1, 2, 3, 4 }, new Func<int, int>(x => x + 2), new int[] { 5, 4, 3, 2, 1 });
                yield return new TestCaseData(new int[] { 5, 1, 2, 3, 4 }, new Func<int, int>(x => x % 2), new int[] { 5, 1, 3, 2, 4 });
            }
        }

        private static IEnumerable<TestCaseData> StringSortByDescendingTestCases
        {
            get
            {
                yield return new TestCaseData(new string[] { "five", "sdfgsdfgfg", "one", "two", "three" }, new Func<string, int>(x => x.Length), new string[] { "sdfgsdfgfg", "three", "five", "one", "two", });
            }
        }

        private static IEnumerable<TestCaseData> SortByDescendingExceptionTestCases
        {
            get
            {
                yield return new TestCaseData(null, new Func<string, bool>(x => x.IndexOf("o") == 0), "source cannot be null.");
                yield return new TestCaseData(new string[] { }, null, "key cannot be null.");
            }
        } 

        [TestCaseSource(nameof(IntegerSortByDescendingTestCases))]
        public static void IntegerSortByDescendingTest(int[] array, Func<int, int> key, int[] expected)
        {
            CollectionAssert.AreEqual(expected, array.SortByDescending(key));
        }

        [TestCaseSource(nameof(StringSortByDescendingTestCases))]
        public static void StringSortByDescendingTest(string[] array, Func<string, int> key, string[] expected)
        {
            CollectionAssert.AreEqual(expected, array.SortByDescending(key));
        }

        [TestCaseSource(nameof(SortByDescendingExceptionTestCases))]
        public void SortByDescendingExceptionTests(string[] array, Func<string, bool> key, string expected)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => array.SortByDescending(key));
            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void SortByDescending_Throws_ArgumentNullException_If_Comparer_IsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new int[] { }.SortByDescending(x => x, null));
            Assert.AreEqual("comparer cannot be null.", exception.Message);
        }
        #endregion

        #region ForAll Tests
        private static IEnumerable<TestCaseData> ForAllTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x % 2 == 0), false);
                yield return new TestCaseData(new int[] { -123, -645, -3545, -1, -2, -3, -4, -5 }, new Func<int, bool>(x => x < 0), true);
                yield return new TestCaseData(new int[] { -123, 645, -3545, 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x / 5 == 1), false);
            }
        }

        [TestCaseSource(nameof(ForAllTestCases))]
        public static void ForAllTest(int[] array, Func<int, bool> predicate, bool expected)
        {
            Assert.AreEqual(expected, array.ForAll(predicate));
        }
        #endregion

        #region GetRangeTests
        [TestCase(100, 5, ExpectedResult = new int[] { 100, 101, 102, 103, 104 })]
        [TestCase(-90, 5, ExpectedResult = new int[] { -90, -89, -88, -87, -86 })]
        public IEnumerable<int> GetRangeTest(int start, int count)
        {
            return Enumerable.GetRange(start, count);
        }
        #endregion

        #region Transform Tests
        private static IEnumerable<TestCaseData> IntegerTransformTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x % 2 == 0), new bool[] { false, true, false, true, false });
                yield return new TestCaseData(new int[] { -123, 645, -3545, 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x < 0), new bool[] { true, false, true, false, false, false, false, false });
            }
        }

        private static IEnumerable<TestCaseData> BoolTransformTestCases
        {
            get
            {
                yield return new TestCaseData(new bool[] { false, true, false, true, false }, new Func<bool, int>(x => Convert.ToInt32(x) + 31 % 2), new int[] { 1, 2, 1, 2, 1 });
                yield return new TestCaseData(new bool[] { true, false, true, false, false, false, false, false }, new Func<bool, int>(x => x is true ? 1 : 0), new int[] { 1, 0, 1, 0, 0, 0, 0, 0 });
            }
        }

        [TestCaseSource(nameof(IntegerTransformTestCases))]
        public static void IntegerTransformTest(int[] array, Func<int, bool> predicate, bool[] expected)
        {
            CollectionAssert.AreEqual(expected, array.Transform(predicate));
        }

        [TestCaseSource(nameof(BoolTransformTestCases))]
        public static void BoolTransformTest(bool[] array, Func<bool, int> predicate, int[] expected)
        {
            CollectionAssert.AreEqual(expected, array.Transform(predicate));
        } 
        #endregion
    }
}