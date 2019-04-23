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
        [TestCase(new[] { 1, 2, 3, 14, 21, 1, -12, -5 }, ExpectedResult = new[] { 2, 14, -12 })]
        [TestCase(new[] { 2341, 227, -32, 33, 144, 21, 1, 212, -5 }, ExpectedResult = new[] { -32, 144, 212 })]
        public IEnumerable<int> Filter_FilterIsEvenWithConcreteIntArray(int[] array) => array.Filter(x => x % 2 == 0);

        public void Filter_FilterIsEvenWithConcreteStringArray()
        {
            string[] array = new[] { "aabcc", "ac", "aac", "aa" };
            CollectionAssert.AreEqual(new[] { "aabcc" }, array.Filter(x => x.Length > 3));
        }

        [TestCase(new[] { 1, 2, 3, 14, 21, 1, -12, -5 }, ExpectedResult = false)]
        [TestCase(new[] { 2341, 227, -32, 33, 144, 21, 1, 212, -5 }, ExpectedResult = false)]
        [TestCase(new[] { 12, 2, 888, 14 }, ExpectedResult = true)]
        public bool ForAll_ForAllIsEvenWithConcreteArray(int[] array) => array.ForAll(x => x % 2 == 0);

        [Test]
        public void Cast_CastToIntFromInt()
        {
            IEnumerable list = new ArrayList()
           {
               1,
               2,
               3,
               4
           };
            Enumerable.CastTo<int>(list);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4 }, list);

        }

        [Test]
        public void Cast_CastToStringFromIntThrowsArgumentNullException() => Assert.Throws<ArgumentNullException>(() => Enumerable.CastTo<string>(null));

        [Test]
        public void Cast_CastToStringFromIntThrowsInvalidCastException()
        {
            ArrayList list = new ArrayList()
           {
               1,
               2,
               3,
               32d,
               4
           };

            Assert.Throws<InvalidCastException>(() => CollectionAssert.AreEqual(new List<string> { "1", "2", "3", "4" }, Enumerable.CastTo<string>(list)));
        }

        [Test]
        public void Cast_CastToStringFromString()
        {
            ArrayList list = new ArrayList()
           {
               "1",
               "2",
               "3",
               "4"
           };

            CollectionAssert.AreEqual(new List<string> { "1", "2", "3", "4" }, Enumerable.CastTo<string>(list));
        }
    }
}