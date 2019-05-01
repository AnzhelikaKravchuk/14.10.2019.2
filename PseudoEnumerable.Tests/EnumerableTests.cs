using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using static PseudoEnumerable.Enumerable;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {

        #region Tests for Filter
        private static IEnumerable<TestCaseData> DataCaseFilterInt
        {
            get
            {
                yield return new TestCaseData(new int[] {1, 2, 3, 4, 5 }, 3) .Returns(new List<int> { 4, 5 });
                yield return new TestCaseData(new int[] {1, 2, 3, 4, 5, 6, 7 }, 10).Returns(new List<int> { });
            }
        }

        [TestCaseSource(nameof(DataCaseFilterInt))]
        public List<int> Filter_ValidArgumentInt_ValidResult(int[] array, int i)
        {
            return new List<int>(array.Filter(item => item > i));
        }



        private static IEnumerable<TestCaseData> DataCaseFilterString
        {
            get
            {
                yield return new TestCaseData(new string[] { "a", "aa", "aaa", "aa", "ssss" }, 2).Returns(new List<string> { "aa", "aaa", "aa", "ssss" });
                yield return new TestCaseData(new string[] {"ss", "s", "ss", "fff",  }, 10).Returns(new List<string> { });
            }
        }

        [TestCaseSource(nameof(DataCaseFilterString))]
        public List<string> Filter_ValidArgumentString_ValidResult(string[] array, int i)
        {
            return new List<string>(array.Filter(item => item.Length >= i));
        }

        #endregion

        #region Tests for ForAll

        private static IEnumerable<TestCaseData> DataCaseForAllString
        {
            get
            {
                yield return new TestCaseData(new string[] { "as", "aa", "aaa", "aa", "ssss" }, 2).Returns(true);
                yield return new TestCaseData(new string[] { "ss", "s", "ss", "fff", }, 10).Returns(false);
            }
        }

        [TestCaseSource(nameof(DataCaseForAllString))]
        public bool ForAll_ValidArgumentString_ValidResult(string[] array, int i)
        {
            return array.ForAll(item => item.Length >= i);
        }

        [Test]
        public void ForAll_ArrayIsNull_ThrowArgumentNullException()
        {
            string[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.ForAll(item => item.Length >= 2));
        }

        [Test]
        public void ForAll_PredicateIsNull_ThrowArgumentNullException()
        {
            Func<string, bool> predicate = null;
            string[] array = { "asd", "awd" };
            Assert.Throws<ArgumentNullException>(() => array.ForAll(predicate));
        }

        #endregion

        #region Tests for SortBy

        private static IEnumerable<TestCaseData> DataCaseSortByAscendingInt
        {
            get
            {
                yield return new TestCaseData(new int[] { 5, 2, 3, 1, 4 }).Returns(new List<int> { 1, 2, 3, 4, 5 });
                yield return new TestCaseData(new int[] { 9, 7, 5, 3, 1, 8, 6, 4, 2 }).Returns(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                yield return new TestCaseData(new int[] { 1, 1, 1, 1, 1, 1 }).Returns(new List<int> { 1, 1, 1, 1, 1, 1 });
                yield return new TestCaseData(new int[] { -1, -2, -3, -4, -5 }).Returns(new List<int> { -5, -4, -3, -2, -1 });
            }
        }

        [TestCaseSource(nameof(DataCaseSortByAscendingInt))]
        public List<int> SotrBy_ValidArgumentString_ValidResult(int[] array)
        {
            return new List<int>( array.SortBy(i => i));
            
        }

        #endregion

    }
}