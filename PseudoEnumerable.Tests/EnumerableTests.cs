using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        [TestCase(new int[] { 309, 94, 8000, 26, -9, -119, 90, 21, 45, 6 }, ExpectedResult = new int[] { 309, 94, 8000, 26, 90, 21, 45, 6 })]
        public IEnumerable<int> Filter_FilterArrayByKey_ValidParametersDigitAsCharTest(int[] array)
        {
            return array.Filter(x => x > 0);
        }

        [TestCase(arg: new string[] { "sdfsdf", "sf", "", "yyyy" }, ExpectedResult = new [] { "sdfsdf", "yyyy" })]
        public IEnumerable<string> Filter_StringTest(string[] array)
        {
            return array.Filter(x => x.Length > 3);
        }

        [TestCase(new int[] { 309, 94, 8000, 26, -9, -119, 90, 21, 45, 6 }, ExpectedResult = false)]
        [TestCase(new int[] { 309, 94, 8000, 26, 90, 21, 45, 6 }, ExpectedResult = true)]
        public bool ForAllIntTest(int[] array)
        {
            return array.ForAll(x => x > 0);
        }

        [TestCase(arg: new string[] { "sdfsdf", "sf", "", "yyyy" }, ExpectedResult = false)]
        [TestCase(arg: new string[] { "sdfsdf", "yyyy" }, ExpectedResult = true)]
        public bool ForAllStringTest(string[] array)
        {
            return array.ForAll(x => x.Length > 3);
        }

        public void CastToStringTest(string[] array)
        {
            var result = Enumerable.CastTo<string>(new ArrayList() { "sdfsdf", "sf", "", "yyyy" });
            Assert.AreEqual(result, new List<string>() { "sdfsdf", "sf", "", "yyyy" });
            Assert.AreEqual(result.GetType(), typeof(IEnumerable<string>));
        }

        public void CastToIntTest(string[] array)
        {
            var result = Enumerable.CastTo<string>(new ArrayList() { 3, 4, -7, 6 });
            Assert.AreEqual(result, new List<int>() { 3, 4, -7, 6 });
            Assert.AreEqual(result.GetType(), typeof(IEnumerable<int>));
        }
    }
}