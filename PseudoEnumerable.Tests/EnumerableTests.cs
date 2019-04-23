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
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6}, ExpectedResult = new int[] { 2, 4, 6})]
        [TestCase(new int[] { 0, 21, 41, 21, 76, 26}, ExpectedResult = new int[] { 0, 76, 26 })]
        public IEnumerable<int> Filter_TSourceIsInteger_Tests(IEnumerable<int> source)
        {
            Func<int, bool> func = delegate(int x) { return x % 2 == 0; };
            var result = new List<int>();
            foreach (var item in source.Filter(func))
            {
                result.Add(item);
            }

            return result;
        }

        [TestCase(new string[] { "1", "2", "123456", "wdwdwdwwa" }, 
            new string[] { "123456", "wdwdwdwwa" })]
        [TestCase(new string[] { "123456", "12345678", "3435erfe" },
            new string[] { "123456", "12345678", "3435erfe" })]
        public void Filter_TSourceIsString_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            Func<string, bool> func = delegate (string x) { return x.Length >= 5; };
            var result = new List<string>();
            foreach (var item in source.Filter(func))
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(result, expected);
        }
    }
}