using System;
using System.Collections.Generic;
using PseudoEnumerable.Tests.Comparers;
using NUnit;
using NUnit.Framework;



namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6 })]
        [TestCase(new int[] { 0, 21, 41, 21, 76, 26 }, ExpectedResult = new int[] { 0, 76, 26 })]
        [TestCase(new int[] { 11, 21, 41, 21}, ExpectedResult = new int[] { })]
        public IEnumerable<int> Filter_TSourceIsInteger_Tests(IEnumerable<int> source)
        {
            var result = new List<int>();
            foreach (var item in source.Filter(new Func<int, bool>(x => x % 2 == 0)))
            {
                result.Add(item);
            }

            return result;
        }

        [TestCase(new string[] { "1", "2", "123456", "wdwdwdwwa" },
            new string[] { "123456", "wdwdwdwwa" })]
        [TestCase(new string[] { "123456", "12345678", "3435erfe" },
            new string[] { "123456", "12345678", "3435erfe" })]
        [TestCase(new string[] { "1", "2", "3" },
            new string[] { })]
        public void Filter_TSourceIsString_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var result = new List<string>();
            foreach (var item in source.Filter(new Func<string, bool>(x => x.Length >= 5)))
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(result, expected);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6, 8, 10, 12 })]
        [TestCase(new int[] { 0, 14, 21, 1, 3 }, ExpectedResult = new int[] { 0, 28, 42, 2, 6 })]
        [TestCase(new int[] {  }, ExpectedResult = new int[] { })]
        public IEnumerable<int> Transform_TSourceIsInteger_Tests(IEnumerable<int> source)
        {
            var result = new List<int>();
            foreach (var item in source.Transform(new Func<int, int>( x => x * 2)))
            {
                result.Add(item);
            }

            return result;
        }

        [TestCase(new string[] { "ad", "aaa", "ddd", "ccccc" }, new int[] { 2, 3, 3, 5 })]
        [TestCase(new string[] { "", "efw", "1", "d3ac" }, new int[] { 0, 3, 1, 4 })]
        public void Transform_TSourceIsString_Tests(IEnumerable<string> source, IEnumerable<int> expected)
        {
            var result = new List<int>();
            foreach (var item in source.Transform(new Func<string, int>(x => x.Length)))
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(result, expected);
        }

        [TestCase(new string[] { "ad", "aaa", "ddd", "ccccc" }, true)]
        [TestCase(new string[] { "", "efw", "1", "d3ac" }, false)]
        public void ForAll_Tests(IEnumerable<string> source, bool expected)
        {
            Assert.AreEqual(source.ForAll(new Func<string, bool>(x => x.Length != 0)), expected);
        }

        [TestCase(new string[] { "4444", "22", "666666", "1", "", "333" }, 
            new string[] { "", "1", "22", "333", "4444", "666666"})]
        [TestCase(new string[] { "a", "aaa", "a", "aa", "aaaaaaaaaaa", "aaaaaa" },
            new string[] { "a", "a", "aa", "aaa", "aaaaaa", "aaaaaaaaaaa" })]
        [TestCase(new string[] { },
            new string[] { })]
        [TestCase(new string[] { "12345", "54321" },
            new string[] { "12345", "54321" })]
        public void SortBy_KeyIsIntegerFedaultComparer_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var actual = source.SortBy(new Func<string, int>(x => x.Length));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new string[] { "4444", "22", "666666", "1", "", "333" },
            new string[] { "666666", "4444", "333", "22", "1", "" })]
        [TestCase(new string[] { "abc", "a", "abcde", "abcdefg" },
            new string[] { "abcdefg", "abcde", "abc", "a" })]
        [TestCase(new string[] { },
            new string[] { })]
        [TestCase(new string[] { "one element" },
            new string[] { "one element" })]
        [TestCase(new string[] { "12345", "1234", "123", "12", "1", "" },
            new string[] { "12345", "1234", "123", "12", "1", "" })]
        public void SortBy_KeyIsIntegerCustomComparer_Tests(IEnumerable<string> source, IEnumerable<string> expected)
        {
            var actual = source.SortBy(new Func<string, int>(x => x.Length), new DescendingOrderComparer());
            CollectionAssert.AreEqual(expected, actual);
        }


    }
}