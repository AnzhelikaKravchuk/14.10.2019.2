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

        #region Filter

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
                
        [TestCase(null)]
        public void Filter_IntegerArray_Throws_ArgumentNullException(int[] array)
            => Assert.Throws<ArgumentNullException>(() => array.Filter(FiltersNumber));

        #endregion

        #region ForAll

        [TestCase(new int[] { 2, 8, 4, 6 }, ExpectedResult = true)]
        public bool Filter_IntegerArray_(int[] array)
            => array.ForAll(FiltersNumber);

        #endregion

        #region CastTo

        [Test]
        public void CastTo_GenericArray()
        {
            ArrayList list = new ArrayList
            {
                "ds",
                2
            };

            Assert.Throws<InvalidCastException>(() => Enumerable.CastTo<int>(list));
        }

        #endregion

        public bool FiltersNumber(int number)
        {
            return number % 2 == 0;
        }
    }       
}