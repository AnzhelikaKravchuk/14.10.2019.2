using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region CastTo Tests

        [Test]
        public void CastTo_Test_1()
        {
            var source = new string[] { "qwe", "qweqwe", "jfhuh3" };
            var expected = new object[] { "qwe", "qweqwe", "jfhuh3" };

            var actual = Enumerable.CastTo<object>(source);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void CastTo_Test_2()
        {
            var source = new StringBuilder[] { new StringBuilder() };

            Assert.Throws<InvalidCastException>(() => Enumerable.CastTo<int>(source));
        }

        #endregion

    }
}