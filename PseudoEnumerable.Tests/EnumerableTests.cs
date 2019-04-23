using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
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
        public void Filter_InputInvalidArgumentPredicate_ResultArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => "string".Filter(null));

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
        [TestCase(new int[] { 9, 99, 14 }, ExpectedResult = true)]
        [TestCase(new int[] { 100, 500, int.MaxValue }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 2, 3, -4 }, ExpectedResult = false)]
        [TestCase(new int[] { 0, 0, 0, 0 }, ExpectedResult = false)]
        [TestCase(new int[] { -1, -3, -5, 10 }, ExpectedResult = false)]
        public bool ForAll_InputValidArgumentsPredicateMoreThanZero_ResultExpectedResult(IEnumerable<int> source) =>
            source.ForAll((x) => x > 0);

        #endregion 
    }
}