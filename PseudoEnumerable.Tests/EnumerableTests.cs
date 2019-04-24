using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter Tests

        [Test]
        public void Filter_Test_1()
        {
            var source = new string[] { "qwe", "qweqwe", "jfhh3" };
            var expected = new string[] { "qweqwe" };

            Func<string, bool> filterLength6 = _ => _.Length == 6;

            var actual = source.Filter(filterLength6);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Filter_Test_2()
        {
            var source = new string[] { null, "qwqq" };
            var expected = new string[] { "qwqq" };

            Func<string, bool> filterNotNull = _ => _ != null;

            var actual = source.Filter(filterNotNull);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

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
            var source = new object[] {1, 2, new StringBuilder() };
            Assert.Throws<InvalidCastException>(() => Enumerable.CastTo<int>(source).ToArray());
        }


        [Test]
        public void CastTo_Test_3()
        {
            var source = new object[] { 1, 2, new StringBuilder() };
            Assert.Throws<InvalidCastException>(() => Enumerable.CastTo<int>(source).ToArray());
        }

        [Test]
        public void CastTo_Test_4()
        {
            var source = new List<int[]> { new int[] { 1 } };
            var expected = new object[] { new object[] { 1 } };

            var actual = Enumerable.CastTo<object>(source);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region ForAll Tests

        [TestCase(new int[] { 1, 2, 3, 4, 5}, ExpectedResult = true)]
        [TestCase(new int[] { -1, 2, 3, 4, 5}, ExpectedResult = false)]
        [TestCase(new int[] { 0 }, ExpectedResult = false)]
        public bool ForAll_Test_1(IEnumerable<int> source)
        {
            Func<int, bool> positiveIntegers = i => i > 0;

            return source.ForAll(positiveIntegers);
        }

        [Test]
        public void ForAll_Test_2()
        {
            var source = new string[] {"qwe", "qqq"};
            Func<string, bool> lengthEquals3 = i => i.Length == 3;

            Assert.IsTrue(source.ForAll<string>(lengthEquals3));
        }

        #endregion

        #region Transform Tests

        [Test]
        public void Transform_Test_1()
        {
            var source = new string[] { "qwe", "qqq" };

            var expected = new string[] {"QWE", "QQQ" };

            Func<string, string> toUpper = _ => _.ToUpper();

            CollectionAssert.AreEqual(expected, source.Transform(toUpper));
        }

        [Test]
        public void Transform_Test_2()
        {
            var source = new string[] { "123", "22" };

            var expected = new int[] { 123, 22 };

            Func<string, int> stringToInt = _ => int.Parse(_);

            CollectionAssert.AreEqual(expected, source.Transform(stringToInt));
        }

        [Test]
        public void Transform_Test_3()
        {
            var source = new string[] { "123", "22" };

            var expected = new List<char[]> { new char[] { '1', '2', '3' }, new char[] { '2', '2' } };

            Func<string, char[]> toCharArray = _ => _.ToCharArray();

            CollectionAssert.AreEqual(expected, source.Transform(toCharArray));
        }

        #endregion

        #region SortBy Tests

        [Test]
        public void SortBy_Test_1()
        {
            var source = new string[] { "q", "q", "qqq", "www" };
            var expected = new string[] {"www", "q", "q", "qqq" };

            char symbol = 'q';

            Func<string, int> keyCountOfSymbol = delegate(string item)
            {
                var charArray = item.ToCharArray();
                int count = 0;
                foreach (var ch in charArray)
                {
                    if (ch == symbol)
                    {
                        count++;
                    }
                }

                return count;
            };

            CollectionAssert.AreEqual(expected, source.SortBy(keyCountOfSymbol));
        }

        [Test]
        public void SortBy_Test_2()
        {
            var source = new int[] { -1, 123, -123 };
            var expected = new int[] { -1, -123, 123 };

            Func<int, bool> keyNegativeFirst = _ => _ > 0;

            CollectionAssert.AreEqual(expected, source.SortBy(keyNegativeFirst));
        }

        [Test]
        public void SortBy_Test_3()
        {
            var source = new int[] { 2, 1, 3, 5, 4 };
            var expected = new int[] { 1, 2, 3, 4, 5 };

            Func<int, int> keyOddFirstThenEven = _ => _;

            CollectionAssert.AreEqual(expected, source.SortBy(keyOddFirstThenEven, Comparer<int>.Default));
        }

        #endregion

        #region SortByDescending Tests

        [Test]
        public void SortByDescending_Test_1()
        {
            var source = new string[] { "q", "q", "qqq", "www" };
            var expected = new string[] { "qqq", "q", "q", "www" };

            char symbol = 'q';

            Func<string, int> keyCountOfSymbol = delegate (string item)
            {
                var charArray = item.ToCharArray();
                int count = 0;
                foreach (var ch in charArray)
                {
                    if (ch == symbol)
                    {
                        count++;
                    }
                }

                return count;
            };


CollectionAssert.AreEqual(expected, source.SortByDescending(keyCountOfSymbol));
        }

        [Test]
        public void SortByDescending_Test_2()
        {
            var source = new int[] { -1, 123, -123 };
            var expected = new int[] { 123, - 1, -123 };

            Func<int, bool> keyNegativeFirst = _ => _ > 0;

            CollectionAssert.AreEqual(expected, source.SortByDescending(keyNegativeFirst));
        }

        [Test]
        public void SortByDescending_Test_3()
        {
            var source = new int[] { 2, 1, 3, 5, 4 };
            var expected = new int[] { 5, 4, 3, 2, 1 };

            Func<int, int> keyOddFirstThenEven = _ => _;

            CollectionAssert.AreEqual(expected, source.SortByDescending(keyOddFirstThenEven, Comparer<int>.Default));
        }

        #endregion

        #region GenerateSequence Tests

        [Test]
        public void GenerateSequence_Test_1()
        {
            BigInteger start = 0;
            int begin = 0;
            int count = 123;
            var expected = new List<BigInteger>(count);
            for (int i = 0; i < count; i++)
            {
                expected.Add(start++);
            }

            CollectionAssert.AreEqual(expected, Enumerable.GenerateSequence(begin, count));
        }

        [Test]
        public void GenerateSequence_Test_2()
        {
            BigInteger start = int.MaxValue;
            int begin = int.MaxValue;
            int count = 100000;
            var expected = new List<BigInteger>(count);
            for (int i = 0; i < count; i++)
            {
                expected.Add(start++);
            }

            CollectionAssert.AreEqual(expected, Enumerable.GenerateSequence(begin, count));
        }

        #endregion
    }
}