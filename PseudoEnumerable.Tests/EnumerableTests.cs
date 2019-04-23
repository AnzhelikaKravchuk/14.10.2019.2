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
    }
}