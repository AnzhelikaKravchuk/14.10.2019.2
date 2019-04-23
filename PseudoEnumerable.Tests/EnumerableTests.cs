using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}