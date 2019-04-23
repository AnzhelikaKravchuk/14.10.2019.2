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

        #region Filter
        [Test]
        public void Filter_for_int()
        {
            int[] array = new int[] { 1, 23, 32 };

            int[] expected = new int[] { 32 };

            int[] actual = array.Filter(el => el % 2 == 0).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Filter_for_byte()
        {
            byte[] array = new byte[] { 1, 23, 32 };

            byte[] expected = new byte[] { 32 };

            byte[] actual = array.Filter(el => el % 2 == 0).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Filter_for_double()
        {
            double[] array = new double[] { 1, 23, 32 };

            double[] expected = new double[] { 32 };

            double[] actual = array.Filter(el => el % 2 == 0).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Filter_for_string()
        {
            string[] array = new string[] { "1", "23", "32" };

            string[] expected = new string[] { "1" };

            string[] actual = array.Filter(el => el.Length == 1).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Filter_Throw()
        {
            string[] array = new string[] { "1", "23", "32" };

            string[] expected = new string[] { "1" };

            string[] actual = array.Filter(el => el.Length == 1).ToArray();

            Th.AreEqual(expected, actual);
        }

        #endregion

        #region Transform
        [Test]
        public void Transform_for_byte_to_int()
        {
            byte[] array = new byte[] { 1, 23, 32 };

            int[] expected = new int[] { 1, 23, 32 };

            int[] actual = array.Transform(el => (int)el).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Transform_for_int_to_string()
        {
            int[] array = new int[] { 1, 23, 32 };

            string[] expected = new string[] { "1", "23", "32" };

            string[] actual = array.Transform(el => el.ToString()).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Transform_for_byte_to_string()
        {
            byte[] array = new byte[] { 1, 23, 32 };

            string[] expected = new string[] { "1", "23", "32" };

            string[] actual = array.Transform(el => el.ToString()).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Transform_for_string_to_int()
        {
            string[] array = new string[] { "1", "23", "32" };

            int[] expected = new int[] { 1, 23, 32 };

            int[] actual = array.Transform(el => int.Parse(el)).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion
    }
}