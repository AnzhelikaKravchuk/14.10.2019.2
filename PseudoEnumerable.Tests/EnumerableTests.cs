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
        [TestCase(new int[] { 2, 3, 4, 6 }, new int[] { 2, 4, 6 })]
        public void Filter_IntegerArray(int[] array, int[] expected)
        {
            Func<int, bool> func = FiltersNumber;

            var actual = array.Filter(func);

            int i = 0;

            foreach (var item in actual)
            {
                Assert.AreEqual(expected[i++], item);
            }
        }

        public bool FiltersNumber(int number)
        {
            return number % 2 == 0;
        }
    }

    
}