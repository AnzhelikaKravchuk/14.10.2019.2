using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Cast tests

        [TestCase(arg: new object[] { "hi", "uy", 12 })]
        [TestCase(arg: new object[] { 12, "hi", "uy" })]
        [TestCase(arg: new object[] { "hi", 12, "uy" })]
        [TestCase(arg: new object[] { 90.90 })]
        public void CastToTests_WithNotStringElement_ThrowInvalidCastException(IEnumerable source)
        {
            void Cast()
            {
                using (var iterator = Enumerable.CastTo<string>(source).GetEnumerator())
                {
                    foreach (var el in source)
                    {
                        iterator.MoveNext();
                    }
                }
            }

            Assert.Throws<InvalidCastException>(() => Cast());
        }

        [TestCase(arg: new double[] { 90.90, 9, double.MaxValue })]
        [TestCase(arg: new double[] { (double)int.MaxValue + 1, -190, 9 })]
        [TestCase(arg: new double[] { 90.90, 9, (double)int.MinValue - 1, 70000 })]
        public void CastToTests_DoubleToIntOverflow_ThrowInvalidCastException(IEnumerable source)
        {
            void Cast()
            {
                using (var iterator = Enumerable.CastTo<string>(source).GetEnumerator())
                {
                    foreach (var el in source)
                    {
                        iterator.MoveNext();
                    }
                }
            }

            Assert.Throws<InvalidCastException>(() => Cast());
        }

        [TestCase(arg: new string[] { "Never", "know", "how much I Love", "you" }, ExpectedResult = new string[] { "Never", "know", "how much I Love", "you" })]
        [TestCase(arg: new string[] { "aaa", "bbb" }, ExpectedResult = new string[] { "aaa", "bbb" })]
        [TestCase(arg: new string[] { "", "" }, ExpectedResult = new string[] { "", "" })]
        [TestCase(arg: new string[] { "" }, ExpectedResult = new string[] { "" })]
        public IEnumerable<string> CastToTestsString_ValidElements(IEnumerable source)
        {
            return Enumerable.CastTo<string>(source);
        }

        [TestCase(arg: new int[] { 90, 9 }, ExpectedResult = new int[] { 90, 9 })]
        public IEnumerable<int> CastToTestsInt_ValidElements(IEnumerable source)
        {
            return Enumerable.CastTo<int>(source);
        }

        #endregion

        #region Filter tests

        [Test]
        public void Filter_ArgumentNullException_NullFilterTest()
        {
            int[] array = new int[] { 6 };
            Assert.Throws<ArgumentNullException>(() => array.Filter(null));
        }

        [Test]
        public void Filter_ArgumentNullException_NullArrayTest()
        {
            int[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Filter(x => x % 2 == 0));
        }

        [TestCase(new int[] { -2, 0 }, ExpectedResult = new int[] { -2, 0 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 77 }, ExpectedResult = new int[] { 1, 2, 3, 4, 5, 6, 7, 77 })]
        [TestCase(new int[] { 606, 11, 7, 19 }, ExpectedResult = new int[] { 606, 11, 7 })]
        [TestCase(new int[] { 777, 77, 77, 707, 7 }, ExpectedResult = new int[] { 777, 77, 77, 707, 7 })]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1 }, ExpectedResult = new int[] { 1, 1, 1, 1, 1, 1 })]
        [TestCase(new int[] { 18, 12344321, -33233, 1234321, 1235, 2346, 0128 }, ExpectedResult = new int[] { 12344321, -33233, 1234321 })]
        public IEnumerable<int> Filter_FilterPalindrome_ValidParametersTest(int[] array)
            => array.Filter(TestClasses.IsIntPalindrome);

        [TestCase(new int[] { 0 }, ExpectedResult = new int[] { 0 })]
        [TestCase(new int[] { -2, 0 }, ExpectedResult = new int[] { -2, 0 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 77 }, ExpectedResult = new int[] { 2, 4, 6, 68, 70 })]
        [TestCase(new int[] { 606, 11, 7, 19 }, ExpectedResult = new int[] { 606 })]
        [TestCase(new int[] { 777, 77, 77, 707, 7 }, ExpectedResult = new int[] { })]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1 }, ExpectedResult = new int[] { })]
        [TestCase(new int[] { 18, 12344321, -33233, 1234321, 1235, 2346, 0128 }, ExpectedResult = new int[] { 18, 2346, 0128 })]
        public IEnumerable<int> Filter_FilterEven_ValidParametersTest(int[] array)
            => array.Filter(x => x % 2 == 0);

        [TestCase(new int[] { 309, 94, 8000, 26, -9, -119, 90, 21, 45, 6 }, ExpectedResult = new int[] { 309, 94, 8000, 26, 90, 21, 45, 6 })]
        public IEnumerable<int> Filter_FilterArrayByKey_ValidParametersDigitAsCharTest(int[] array)
        {
            return array.Filter(x => x > 0);
        }

        [TestCase(arg: new string[] { "sdfsdf", "sf", "", "yyyy" }, ExpectedResult = new[] { "sdfsdf", "yyyy" })]
        public IEnumerable<string> Filter_StringTest(string[] array)
        {
            return array.Filter(x => x.Length > 3);
        }

        #endregion

        #region Transformer tests

        [Test]
        public void Transformer_ArgumentNullException_NullFilterTest()
        {
            var array = new double[] { 1 };
            Assert.Throws<ArgumentNullException>(() => array.Transform((Func<double, int>)null));
        }

        [Test]
        public void Transformer_ArgumentNullException_NullArrayTest()
        {
            double[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Transform(x => -x));
        }

        [Test]
        public void Transformer_ArgumentException_EmptyArrayTest()
        {
            double[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Transform( x => x ));
        }

        [TestCase(new[] { -23.809, 0.001 }, ExpectedResult = new[] { "-23.809", "0.001" })]
        public IEnumerable<string> Transform_TransformToWordsENTests(double[] array) => array.Transform(x => x.ToString(CultureInfo.InvariantCulture));

        [TestCase(arg: new[] { "dd", "ddd" }, ExpectedResult = new[] { 2, 3 })]
        public IEnumerable<int> Transform_TransformToWordsRUTests(string[] array) => array.Transform(x => x.Length);

        [TestCase(new[] { 9.809, 1.00001 }, ExpectedResult = new[] { 9, 1 })]
        public IEnumerable<int> Transform_TransformBinaryTests(double[] array) => array.Transform(x => (int)x);

        #endregion

        #region

        [Test]
        public void Sort_ArgumentNullException_NullFilterTest()
        {
            var array = new int[] { 2 };
            Assert.Throws<ArgumentNullException>(() => array.SortBy((Func<int, int>)null));
        }

        [Test]
        public void Sort_ArgumentNullException_NullArrayTest()
        {
            string[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.SortBy(x => x));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesStringsAsc))]
        public IEnumerable<string> Sort_StringLengthAscTest(string[] array)
        {
            return array.SortBy(x => x.Length);
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesStringsDesc))]
        public IEnumerable<string> Sort_StringLengthDescTest(string[] array)
        {
            return array.SortByDescending(x => x.Length);
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCasesStringsAsc
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new[] { "Когда", "моя", "музыка", "вновь", "захлебнется", "от", "крика" } })
                        .Returns(new[] { "от", "моя", "Когда", "вновь", "крика", "музыка", "захлебнется" });

                    yield return new TestCaseData(
                        new[] { new[] { "veni", "vidi", "vici" } })
                        .Returns(new[] { "veni", "vidi", "vici" });

                    yield return new TestCaseData(
                        new[] { new[] { "", "kk", "" } })
                        .Returns(new[] { "", "", "kk" });
                }
            }

            public static IEnumerable TestCasesStringsDesc
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new[] { "Когда", "моя", "музыка", "вновь", "захлебнется", "от", "крика" } })
                        .Returns(new[] { "захлебнется", "музыка", "Когда", "вновь", "крика", "моя", "от" });

                    yield return new TestCaseData(
                        new[] { new[] { "veni", "vidi", "vici" } })
                        .Returns(new[] { "veni", "vidi", "vici" });

                    yield return new TestCaseData(
                        new[] { new[] { "", "kk", "" } })
                        .Returns(new[] { "kk", "", "" });

                    yield return new TestCaseData(
                        new[] { new[] { "" } })
                        .Returns(new[] { "" });
                }
            }
        }

        #endregion

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

        [TestCase(1, 2, ExpectedResult = new[] { -2, -5 })]
        [TestCase(-10, 5, ExpectedResult = new[] { -13, -16, -19, -22, -25 })]
        public IEnumerable<int> GenerateSequenceTest(int start, int count)
        {
            return Enumerable.GenerateSequence(start, count, x => x - 3);
        }
    }
}