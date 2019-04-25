using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter Tests

        [Test]
        public void Filter_SourceIsNull_ThrowsArgumentNullException()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter(source, x => x % 2 == 0));
        }

        [Test]
        public void Filter_PredicateIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter(new int[] { 1, 2 }, null));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6 })]
        public IEnumerable<int> Filter_SourceTypeIsInt_Filters(IEnumerable<int> source)
            => Enumerable.Filter(source, x => x % 2 == 0);
        
        [TestCase(new string[] { "some", "string" }, 5, ExpectedResult = new string[] { "some" })]
        public IEnumerable<string> Filter_SourceTypeIsString_Filters(IEnumerable<string> source, int length)
            => Enumerable.Filter(source, x => x.Length < length);

        #endregion

        #region Transform Tests

        [Test]
        public void Transform_SourceIsNull_ThrowsArgumentNullException()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform(source, x => x * 2));
        }

        [Test]
        public void Transform_TransformerIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform<int, int>(new int[] { 1, 2 }, null));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6, 8, 10, 12 })]
        public IEnumerable<int> Transform_SourceTypeIsInt_Transforms(IEnumerable<int> source)
            => Enumerable.Transform<int, int>(source, x => x * 2);

        [TestCase(arg: new string[] { "some", "string" }, ExpectedResult = new int[] { 4, 6 })]
        public IEnumerable<int> Transform_SourceTypeIsString_Transforms(IEnumerable<string> source)
            => Enumerable.Transform<string, int>(source, x => x.Length);

        #endregion
        
        #region CastTo Tests

        [Test]
        public void CastTo_SourceIsNull_ThrowsArgumentNullException()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => Enumerable.CastTo<int>(source));
        }

        [Test]
        public void CastTo_ThrowsInvalidCastException()
        {
            string[] source = new string[] { "4", "5" };
            using (var iterator = Enumerable.CastTo<int>(source).GetEnumerator())
            {
                Assert.Throws<InvalidCastException>(delegate
                {
                    while (iterator.MoveNext())
                    {
                    }
                });
            }
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = new int[] { 1, 2, 3, 4, 5 })]
        public IEnumerable<int> CastTo_Casts(IEnumerable source)
            => Enumerable.CastTo<int>(source);

        #endregion

        #region ForAll Tests

        [TestCase(new int[] { 2, 4, 6, 8 }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 2, 3, 4 }, ExpectedResult = false)]
        public bool ForAll_ChecksNumbers(IEnumerable<int> source)
            => Enumerable.ForAll(source, x => x % 2 == 0);

        [TestCase(new string[] { "some", "home" }, 5, ExpectedResult = true)]
        [TestCase(new string[] { "some", "string" }, 5, ExpectedResult = false)]
        public bool ForAll_ChecksStrings(IEnumerable<string> source, int length)
            => Enumerable.ForAll(source, x => x.Length < length);

        #endregion
    }
}