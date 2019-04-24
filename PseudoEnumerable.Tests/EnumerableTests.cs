using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    /// <summary>
    /// Tests for Enumerable class 
    /// </summary>
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter tests

        [Test]
        public void Filter_InputInvalidArgumentSource_ResultArgumentNullException()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.Filter((x) => x > 0));
        }

        [Test]
        public void Filter_InputInvalidArgumentPredicate_ResultArgumentNullException()
        {
            string[] source = new string[] { "Bard De Smet", "Jhon Skit" };
            Assert.Throws<ArgumentNullException>(() => source.Filter(null));
        }

        [TestCase(new int[] { }, ExpectedResult = new int[] { })]
        [TestCase(new int[] { -1, -2, -3, 4, 5, 0 }, ExpectedResult = new int[] { -1, -2, -3 })]
        public IEnumerable<int> Filter_InputIntegerArray_ResultLessThanZero(int[] source) =>
            source.Filter<int>((x) => x < 0);

        [TestCase(arg: new string[] { "abc", "12345", "1234567890", "12345678910-11" }, ExpectedResult = new string[] { "12345678910-11" })]
        [TestCase(arg: new string[] { "Bard De Smet", "Jhon Skit", "Ben Albahari", "Eric Lippert" }, ExpectedResult = new string[] { "Bard De Smet", "Ben Albahari", "Eric Lippert" })]
        public IEnumerable<string> Filter_InputStringQueue_ResultLengthMoreThanTen(string[] source)
        {
            Queue<string> newQueue = new Queue<string>(source);
            return newQueue.Filter((x) => x.Length > 10);
        }

        #endregion

        #region Transform tests

        [Test]
        public void Transform_InvalidArgumentSource_ThrowArgumentNullException()
        {
            string[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Transform((x) => x.ToString()));
        }

        [Test]
        public void Transform_InvalidTransform_ThrowArgumentNullException()
        {
            string[] source = { "AAA", "bbb" };
            Func<string, string> transformer = null;
            Assert.Throws<ArgumentNullException>(() => source.Transform(transformer));
        }

        [TestCase(new int[] { 1, 2, 3 }, ExpectedResult = new string[] { "1", "2", "3" })]
        public IEnumerable<string> Transform_IntegerArray_StringArray(int[] source) =>
            source.Transform((x) => x.ToString());

        [TestCase(new int[] { 1, 2, 3, -1 }, ExpectedResult = new double[] { 1.5, 2.5, 3.5, -0.5 })]
        public IEnumerable<double> Transform_IntegerArray_DoubleArray(int[] source) =>
            source.Transform((x) => x + 0.5);

        #endregion

        #region SortBy test

        [Test]
        public void SortBy_InputInvalidArgumentSource_ResultArgumentNullException()
        {
            string[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.SortBy((x) => x.ToString()));
        }

        [Test]
        public void SortBy_InputInvalidArgumentPredicate_ResultArgumentNullException()
        {
            string[] source = new string[] { "Bard De Smet", "Jhon Skit" };
            Func<string, int> key = null;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(key));
        }

        [TestCase(arg: new string[] { }, ExpectedResult = new int[] { })]
        [TestCase(arg: new string[] { "11", "2", "-3" }, ExpectedResult = new string[] { "-3", "2", "11" })]
        [TestCase(arg: new string[] { "0", "-1000", "234" }, ExpectedResult = new string[] { "-1000", "0", "234" })]
        public IEnumerable<string> SortBy_InputStringArrayIntegerKey_ResultExpectedResult(string[] source) =>
            source.SortBy((x) => int.Parse(x));

        [TestCase(arg: new string[] { "nnnnnn", "11", "323", "a", "abng", "vdfdd" })]
        public void SortBy_InputStackKeyStringLength_ResulSortByKey(string[] source)
        {
            Stack<string> stack = new Stack<string>(source);
            IEnumerable<string> sortedStack = stack.SortBy((x) => x.Length);
            Stack<string> expected = new Stack<string>(new string[] { "nnnnnn", "vdfdd", "abng", "323", "11", "a" });
            CollectionAssert.AreEqual(expected, sortedStack);
        }

        #endregion

        #region SortBy tests with comparer

        [Test]
        public void SortBy_InputInvalidArgumentComparer_ResultArgumentNullException()
        {
            string[] source = new string[] { "a", "b" };
            Assert.Throws<ArgumentNullException>(() => source.SortBy((x) => x.ToString(), null));
        }

        #endregion

        #region SortByDescending

        [Test]
        public void SortDescending_InputInvalidArgumentSource_ResultArgumentNullException()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending((x) => x.ToString()));
        }

        [Test]
        public void SortByDescending_InputInvalidArgumentPredicate_ResultArgumentNullException()
        {
            string[] source = new string[] { "Bard De Smet", "Jhon Skit" };
            Func<string, int> key = null;
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending(key));
        }

        [TestCase(arg: new string[] { }, ExpectedResult = new int[] { })]
        [TestCase(arg: new string[] { "11", "2", "-3" }, ExpectedResult = new string[] { "11", "2", "-3" })]
        [TestCase(arg: new string[] { "0", "-1000", "234" }, ExpectedResult = new string[] { "234", "0", "-1000" })]
        public IEnumerable<string> SortByDescending_InputStringArrayIntegerKey_ResultExpectedResult(string[] source) =>
            source.SortByDescending((x) => int.Parse(x));

        [TestCase(arg: new string[] { "nnnnnn", "11", "323", "a", "abng", "vdfdd" })]
        public void SortByDescending_InputStackKeyStringLength_ResulSortByKey(string[] source)
        {
            Stack<string> stack = new Stack<string>(source);
            IEnumerable<string> sortedStack = stack.SortByDescending((x) => x.Length);
            Stack<string> expected = new Stack<string>(new string[] { "a", "11", "323", "abng", "vdfdd", "nnnnnn" });
            CollectionAssert.AreEqual(expected, sortedStack);
        }

        #endregion

        #region SortByDescending tests with comparer

        [Test]
        public void SortByDescending_InputInvalidArgumentComparer_ResultArgumentNullException()
        {
            string[] source = new string[] { "a", "b" };
            Assert.Throws<ArgumentNullException>(() => source.SortByDescending((x) => x.ToString(), null));
        }

        #endregion

        #region CastTo tests

        [Test]
        public void CastTo_InvalidArgument_ThrowArgumetnException()
        {
            object[] source = null;
            Assert.Throws<ArgumentNullException>(() => Enumerable.CastTo<int>(source));
        }

        [TestCase(arg: new object[] { 12, "hi" })]
        public void CastToTests_WithNotStringElement_ThrowInvalidCastException(IEnumerable source)
        {
            using (var iterator = Enumerable.CastTo<string>(source).GetEnumerator())
            {
                Assert.Throws<InvalidCastException>(() => iterator.MoveNext());
            }
        }

        [TestCase(arg: new object[] { 12, 20, -100 })]
        public void CastTo_ListOfIntegerObject_ListOfIntegers(object[] source)
        {
            List<object> listOfObject = new List<object>(source);
            List<int> expected = new List<int> { 12, 20, -100 };
            CollectionAssert.AreEqual(expected, Enumerable.CastTo<int>(listOfObject));
        }

        [TestCase(arg: new object[] { 'v', 'a', 'd' })]
        public void CastTo_QueueOfObject_IEnumerableOfChars(object[] source)
        {
            Queue<object> queue = new Queue<object>(source);
            List<char> expected = new List<char> { 'v', 'a', 'd' };
            CollectionAssert.AreEqual(expected, Enumerable.CastTo<char>(queue));
        }

        #endregion

        #region ForAll tests

        [Test]
        public void ForAll_InputInvalidArgumentSource_ResultArgumentNullException()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.ForAll((x) => x > 0));
        }

        [Test]
        public void ForAll_InputInvalidArgumentPredicate_ResultArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => "string".Filter(null));

        [TestCase(new int[] { }, ExpectedResult = true)]
        [TestCase(new int[] { 100, 500, int.MaxValue }, ExpectedResult = true)]
        [TestCase(new int[] { 0, 0, 0, 0 }, ExpectedResult = false)]
        [TestCase(new int[] { -1, -3, -5, 10 }, ExpectedResult = false)]
        public bool ForAll_InputIntegerArgumentsPredicateMoreThanZero_ResultExpectedResult(IEnumerable<int> source) =>
            source.ForAll((x) => x > 0);

        [TestCase(arg: new string[] { "CSharp", "Java" }, ExpectedResult = true)]
        [TestCase(arg: new string[] { "C", "F#" }, ExpectedResult = false)]
        public bool ForAll_InputStringArgumentsPredicate_ResultExpectedResult(IEnumerable<string> source) =>
            source.ForAll((x) => x.Length > 3);

        [TestCase(arg: new char[] { 'w', 's', '4', '2' }, ExpectedResult = false)]
        [TestCase(arg: new char[] { 'd', 'H', 'D' }, ExpectedResult = true)]
        public bool ForAll_InputCharArgumentsPredicate_ResultExpectedResult(IEnumerable<char> source) =>
            source.ForAll((x) => char.IsLetter(x));

        #endregion

        #region IntegerSequence tests

        [Test]
        public void IntegerSequence_InvalidArgumentCount_ThrowArgumentException() =>         
            Assert.Throws<ArgumentException>(() => Enumerable.IntegerSequence(1, -1, (x) => x > 0));

        [Test]
        public void IntegerSequence_InvalidArgumentPredicate_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.IntegerSequence(1, -1, null));

        [TestCase(1, 10)]
        [TestCase(1, 100000)]
        public void IntegerSequence_ValidArguments_Console(int start, int count)
        {
            IEnumerable<int> result = Enumerable.IntegerSequence(start, count, (x) => x % 2 == 0);
            foreach (int element in result)
            {
                Console.WriteLine(element);
            }
        }

        #endregion
    }
}