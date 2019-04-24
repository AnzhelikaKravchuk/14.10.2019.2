using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PseudoEnumerable;
using System.Numerics;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter Tests

        [Test]
        public static void Filter_NullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter<int>(null, x => x != 0));
        }

        [Test]
        public static void Filter_NullPredicate_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter<int>(new int[] { 1, 2, 3 }, null));
        }

        [Test]
        public static void Filter_EmptySource_EmptyResult()
        {
            Assert.AreEqual(Enumerable.Filter(new int[] { }, x => x != 0), new int[] { });
        }

        [Test]
        [TestCase(new int[] { 42 }, ExpectedResult = new int[] { 42 })]
        [TestCase(new int[] { 1, 3, 5, 7, 9 }, ExpectedResult = new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6}, ExpectedResult = new int[] { 2, 4, 6 })]
        [TestCase(new int[] { -2, -4, 16, 1, -3, -6 }, ExpectedResult = new int[] { -2, -4, 16, -6 })]
        [TestCase(new int[] { int.MaxValue, 0, int.MinValue }, ExpectedResult = new int[] { 0, int.MinValue })]
        public static IEnumerable<int> Filter_IntValidCases_ExpectedResult(IEnumerable<int> source)
        {
            return source.Filter(x => x % 2 == 0);
        }

        [Test]
        [TestCase(new char[] { 'a' }, ExpectedResult = new char[] { 'a' })]
        [TestCase(new char[] { 'z', 'x', 'y' }, ExpectedResult = new char[] { })]
        [TestCase(new char[] { 'a', 'z', 'x', 'y', 'b' }, ExpectedResult = new char[] { 'a', 'b' })]
        [TestCase(new char[] { 'a', 'b', 'c', 'd', 'e' }, ExpectedResult = new char[] { 'a', 'b', 'c', 'd', 'e' })]
        public static IEnumerable<char> Filter_CharValidCases_ExpectedResult(IEnumerable<char> source)
        {
            return source.Filter(x => x < 'g');
        }

        [Test]
        public static void Filter_StringValidCase_ExpectedResult()
        {
            string[] source = new string[] { "qwe", "asd", "qwerty", "dsfs", "12335454", string.Empty };
            string[] expected = new string[] { "qwerty", "dsfs", "12335454" };
            CollectionAssert.AreEqual(expected, source.Filter(x => x.Length > 3));
        }

        #endregion

        #region Transform Tests

        [Test]
        public static void Transform_NullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform<int, string>(null, x => x.ToString()));
        }

        [Test]
        public static void Transform_NullTransformer_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform<int, bool>(new int[] { 1, 2, 3 }, null));
        }

        [Test]
        public static void Transform_EmptySource_EmptyResult()
        {
            Assert.AreEqual(Enumerable.Transform(new int[] { }, x => x * 2), new int[] { });
        }

        [Test]
        [TestCase(new int[] { 48 }, ExpectedResult = new char[] { '0' })]
        [TestCase(new int[] { 49, 50, 51, 52, 53 }, ExpectedResult = new char[] { '1', '2', '3', '4', '5' })]
        [TestCase(new int[] { 80, 81, 82, 83, 84 }, ExpectedResult = new char[] { 'P', 'Q', 'R', 'S', 'T' })]
        [TestCase(new int[] { 60, 61, 62 }, ExpectedResult = new char[] { '<', '=', '>' })]
        public static IEnumerable<char> Transform_IntToCharValidCases_ExpectedResult(IEnumerable<int> source)
        {
            return source.Transform(x => (char)x);
        }

        [Test]
        [TestCase(new int[] { 1 }, ExpectedResult = "1")]
        [TestCase(new int[] { 8 }, ExpectedResult = "1000")]
        [TestCase(new int[] { 15 }, ExpectedResult = "1111")]
        [TestCase(new int[] { 255 }, ExpectedResult = "11111111")]
        public static string Transform_IntToStringValidCases_ExpectedResult(IEnumerable<int> source)
        {
            return First(source.Transform(x => Convert.ToString(x, 2)));
        }

        #endregion

        #region SortBy Tests

        [Test]
        public static void SortBy_NullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy<string, int>(null, x => x.Length));
        }

        [Test]
        public static void SortBy_NullKey_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy<int, bool>(new int[] { 1, 2, 3 }, null));
        }

        [Test]
        public static void SortBy_NullComparer_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy<int, int>(new int[] { 1, 2, 3 }, x => x, null));
        }

        [Test]
        public static void SortBy_EmptySource_EmptyResult()
        {
            Assert.AreEqual(Enumerable.SortBy(new int[] { }, x => x.ToString().Length), new int[] { });
        }

        [Test]
        [TestCase(new int[] { 1 }, ExpectedResult = new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5  }, ExpectedResult = new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 4, -8, 7, -3, -2 }, ExpectedResult = new int[] { 1, -2, -3, 4, 7, -8 })]
        [TestCase(new int[] { 2, -2, 2, -2, 2, -2 }, ExpectedResult = new int[] { 2, -2, 2, -2, 2, -2 })]
        public static IEnumerable<int> SortBy_IntValidCases_ExpectedResult(IEnumerable<int> source)
        {
            return source.SortBy(x => (int)Math.Pow(x, 2));
        }

        [Test]
        [TestCase(new char[] { 'a' }, ExpectedResult = new char[] { 'a' })]
        [TestCase(new char[] { 'b', '0', 'Z', 'a' }, ExpectedResult = new char[] { '0', 'Z', 'a', 'b' })]
        [TestCase(new char[] { '1', '3', '2', '7', '5' }, ExpectedResult = new char[] { '1', '2', '3', '5', '7' })]
        public static IEnumerable<char> SortBy_CharValidCases_ExpectedResult(IEnumerable<char> source)
        {
            return source.SortBy(x => x);
        }

        [TestCase(new string[] { "q" }, new string[] { "q" })]
        [TestCase(new string[] { "q", "w", "e", "r", "t" }, new string[] { "q", "w", "e", "r", "t" })]
        [TestCase(new string[] { "qwe", "qwer", "q", "qwert", "qw" }, new string[] { "q", "qw", "qwe", "qwer", "qwert" })]
        [TestCase(new string[] { "124314", "3", "3536", "13243", "353535353" }, new string[] { "3", "3536", "13243", "124314", "353535353" })]
        public static void SortBy_StringValidCases_ExpectedResult(IEnumerable<string> input, IEnumerable<string> expected)
        {
            Assert.AreEqual(input.SortBy(x => x.Length), expected); 
        }

        [TestCase(new string[] { "q" }, new string[] { "q" })]
        [TestCase(new string[] { "KLA", "AA", "QWE" }, new string[] { "QWE", "KLA", "AA" })]
        [TestCase(new string[] { "QWE", "WTREWT", "VNMV", "RYIYIYRR" }, new string[] { "QWE", "WTREWT", "VNMV", "RYIYIYRR" })]
        [TestCase(new string[] { "AFDDA", "QAWASAFAA", "FHGJJDGDVNV", "AAA" }, new string[] { "FHGJJDGDVNV", "AFDDA", "AAA", "QAWASAFAA" })]
        public static void SortBy_StringValidCasesWithCustomComparer_ExpectedResult(IEnumerable<string> input, IEnumerable<string> expected)
        {
            Assert.AreEqual(input.SortBy(x => x, new LetterACountComparer()), expected);
        }

        #endregion

        #region SortByDescending Tests

        [Test]
        public static void SortByDescending_NullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending<string, int>(null, x => x.Length));
        }

        [Test]
        public static void SortByDescending_NullKey_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending<int, bool>(new int[] { 1, 2, 3 }, null));
        }

        [Test]
        public static void SortByDescending_NullComparer_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending<int, int>(new int[] { 1, 2, 3 }, x => x, null));
        }

        [Test]
        public static void SortByDescending_EmptySource_EmptyResult()
        {
            Assert.AreEqual(Enumerable.SortByDescending(new int[] { }, x => x.ToString().Length), new int[] { });
        }

        [Test]
        [TestCase(new int[] { 1 }, ExpectedResult = new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = new int[] { 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 4, -8, 7, -3, -2 }, ExpectedResult = new int[] { -8, 7, 4, -3, -2, 1 })]
        [TestCase(new int[] { 2, -2, 2, -2, 2, -2 }, ExpectedResult = new int[] { -2, 2, -2, 2, -2, 2 })]
        public static IEnumerable<int> SortByDescending_IntValidCases_ExpectedResult(IEnumerable<int> source)
        {
            return source.SortByDescending(x => (int)Math.Pow(x, 2));
        }

        [Test]
        [TestCase(new char[] { 'a' }, ExpectedResult = new char[] { 'a' })]
        [TestCase(new char[] { 'b', '0', 'Z', 'a' }, ExpectedResult = new char[] { 'b', 'a', 'Z', '0' })]
        [TestCase(new char[] { '1', '3', '2', '7', '5' }, ExpectedResult = new char[] { '7', '5', '3', '2', '1' })]
        public static IEnumerable<char> SortByDescending_CharValidCases_ExpectedResult(IEnumerable<char> source)
        {
            return source.SortByDescending(x => x);
        }

        [Test]
        [TestCase(new string[] { "q" }, new string[] { "q" })]
        [TestCase(new string[] { "q", "w", "e", "r", "t" }, new string[] { "t", "r", "e", "w", "q" })]
        [TestCase(new string[] { "qwe", "qwer", "q", "qwert", "qw" }, new string[] { "qwert", "qwer", "qwe", "qw", "q" })]
        [TestCase(new string[] { "124314", "3", "3536", "13243", "353535353" }, new string[] { "353535353", "124314", "13243", "3536", "3" })]
        public static void SortByDescending_StringValidCases_ExpectedResult(IEnumerable<string> input, IEnumerable<string> expected)
        {
            Assert.AreEqual(input.SortByDescending(x => x.Length), expected);
        }

        [Test]
        [TestCase(new string[] { "q" }, new string[] { "q" })]
        [TestCase(new string[] { "KLA", "AA", "QWE" }, new string[] { "AA", "KLA", "QWE" })]
        [TestCase(new string[] { "QWE", "WTREWT", "VNMV", "RYIYIYRR" }, new string[] { "RYIYIYRR", "VNMV", "WTREWT", "QWE" })]
        [TestCase(new string[] { "AFDDA", "QAWASAFAA", "FHGJJDGDVNV", "AAA" }, new string[] { "QAWASAFAA", "AAA", "AFDDA", "FHGJJDGDVNV" })]
        public static void SortByDescending_StringValidCasesWithCustomComparer_ExpectedResult(IEnumerable<string> input, IEnumerable<string> expected)
        {
            Assert.AreEqual(input.SortByDescending(x => x, new LetterACountComparer()), expected);
        }

        #endregion

        #region CastTo Tests

        public void CastTo_Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.CastTo<string>(null));
        }

        #endregion

        #region ForAll Tests

        [Test]
        public static void ForAll_NullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending<string, int>(null, x => x.Length));
        }

        [Test]
        public static void ForAll_NullPredicate_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending<int, bool>(new int[] { 1, 2, 3 }, null));
        }

        [Test]
        [TestCase(new int[] { 42 }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 3, 5, 7, 9 }, ExpectedResult = false)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = false)]
        [TestCase(new int[] { -2, -4, 16, 10, -30, -6 }, ExpectedResult = true)]
        [TestCase(new int[] { int.MaxValue, 0, int.MinValue }, ExpectedResult = false)]
        public static bool ForAll_IntValidCases_ExpectedResult(IEnumerable<int> source)
        {
            return source.ForAll(x => x % 2 == 0);
        }

        [Test]
        [TestCase(new char[] { 'a' }, ExpectedResult = true)]
        [TestCase(new char[] { 'z', 'x', 'y' }, ExpectedResult = false)]
        [TestCase(new char[] { 'a', 'z', 'x', 'y', 'b' }, ExpectedResult = false)]
        [TestCase(new char[] { 'a', 'b', 'c', 'd', 'e' }, ExpectedResult = true)]
        public static bool ForAll_CharValidCases_ExpectedResult(IEnumerable<char> source)
        {
            return source.ForAll(x => x < 'g');
        }

        #endregion

        #region GenerateNumbers Tests

        public void GenerateNumbers_NullCount_EmptyArray(int start, uint count)
        {
            Assert.IsEmpty(Enumerable.GenerateNumbers(1, 0u, x => x));
        }

        [TestCase(0, 5u, ExpectedResult = new int[] { 0, 1, 2, 3, 4 })]
        [TestCase(3, 5u, ExpectedResult = new int[] { 3, 4, 5, 6, 7 })]
        [TestCase(-3, 5u, ExpectedResult = new int[] { -3, -2, -1, 0, 1 })]
        public static IEnumerable<int> GenerateNumbers_ValidCases_ExpectedResultAscending(int start, uint count)
        {
            return Enumerable.GenerateNumbers(start, count, x => x + 1);
        }

        [TestCase(0, 5u, ExpectedResult = new int[] { 0, 0, 0, 0, 0 })]
        [TestCase(3, 5u, ExpectedResult = new int[] { 3, -6, 12, -24, 48 })]
        [TestCase(-3, 5u, ExpectedResult = new int[] { -3, 6, -12, 24, -48 })]
        public static IEnumerable<int> GenerateNumbers_ValidCases_ExpectedResultWithComlicatedRule(int start, uint count)
        {
            return Enumerable.GenerateNumbers(start, count, x => x * -2);
        }

        #endregion

        private static T First<T>(IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                return item;
            }
            throw new ArgumentException();
        }
    }
}