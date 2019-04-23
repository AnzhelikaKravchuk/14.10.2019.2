using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        private IEnumerable<Func<int, bool>> TestCases
        {
            get
            {
                yield return x => x % 2 == 0;
            }
        }

        [Test]
        public static void FilterdelegateTest()
        {
            CollectionAssert.AreEqual(new int[] { 2, 4}, new int[] { 1, 2, 3, 4, 5 }.Filter(x => x % 2 == 0));
        }

        [Test]
        public void CastTest()
        {
            string[] array = new string[] { "1", "2", "3", "4", "5" };

            CollectionAssert.AreEqual(new string[] { "1", "2", "3", "4", "5" }, Enumerable.CastTo<string>(array));
        }

        //[Test]
        //public void InvalidCastTest()
        //{
        //    var exception = Assert.Throws<ArgumentNullException>(() => Enumerable.CastTo<int>(new string[] { "1", "2", "3", "4", "5" }));
        //    Assert.AreEqual("Array cannot be null. Parameter name: array", exception.Message);
        //}
    }
}