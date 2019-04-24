using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        private static IEnumerable<TestCaseData> IntSortByTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 5, 1, 2, 3, 4 }, new Func<int, int>(x => x + 2), new int[] { 1, 2, 3, 4, 5 });
                yield return new TestCaseData(new int[] { 235, 1, 22, 3, 4 }, new Func<int, int>(x => x), new int[] { 1, 3, 4, 22, 235 });
            }
        }

        [TestCase(new[] { 1, 2, 3, 14, 21, 1, -12, -5 }, ExpectedResult = new[] { 2, 14, -12 })]
        [TestCase(new[] { 2341, 227, -32, 33, 144, 21, 1, 212, -5 }, ExpectedResult = new[] { -32, 144, 212 })]
        public IEnumerable<int> Filter_FilterIsEvenWithConcreteIntArray(int[] array) => array.Filter(x => x % 2 == 0);

        public void Filter_FilterIsEvenWithConcreteStringArray()
        {
            string[] array = new[] { "aabcc", "ac", "aac", "aa" };
            CollectionAssert.AreEqual(new[] { "aabcc" }, array.Filter(x => x.Length > 3));
        }

        [TestCase(new[] { 1, 2, 3, 14, 21, 1, -12, -5 }, ExpectedResult = false)]
        [TestCase(new[] { 2341, 227, -32, 33, 144, 21, 1, 212, -5 }, ExpectedResult = false)]
        [TestCase(new[] { 12, 2, 888, 14 }, ExpectedResult = true)]
        public bool ForAll_ForAllIsEvenWithConcreteArray(int[] array) => array.ForAll(x => x % 2 == 0);

        [Test]
        public void Cast_CastToIntFromInt()
        {
            IEnumerable list = new ArrayList()
           {
               1,
               2,
               3,
               4
           };
            Enumerable.CastTo<int>(list);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4 }, list);
        }

        [Test]
        public void Cast_CastToStringFromIntThrowsArgumentNullException() => Assert.Throws<ArgumentNullException>(() => Enumerable.CastTo<string>(null));

        [Test]
        public void Cast_CastToStringFromIntThrowsInvalidCastException()
        {
            ArrayList list = new ArrayList()
           {
               1,
               2,
               3,
               32d,
               4
           };

            Assert.Throws<InvalidCastException>(() => CollectionAssert.AreEqual(new List<string> { "1", "2", "3", "4" }, Enumerable.CastTo<string>(list)));
        }

        [Test]
        public void Cast_CastToStringFromString()
        {
            ArrayList list = new ArrayList()
           {
               "1",
               "2",
               "3",
               "4"
           };

            CollectionAssert.AreEqual(new List<string> { "1", "2", "3", "4" }, Enumerable.CastTo<string>(list));
        }

        [TestCase(new double[] { 523, 0.234, double.PositiveInfinity },
            ExpectedResult = new[]
            {
            "пять два три",
            "нуль точка два три четыре",
            "положительная бесконечность"
            })]
        public IEnumerable<string> Transform_TransformToRussinWordsWith(double[] array) => array.Transform(x => new RusssianTransformer().Transform(x));

        [TestCase( new int[] { 1, 2, 3, 14, 21, 1 }, ExpectedResult = new int[] { 43, 44, 45, 56, 63, 43 })]
        [TestCase(new int[] { 33, 0 }, ExpectedResult = new int[] { 75, 42 })]
        public IEnumerable<int> Transform_TransformByAdding42(int[] array) => array.Transform(x => x + 42);               

        [TestCaseSource(nameof(IntSortByTestCases))]
        public static void SortBy_SortByWithIntArray(int[] source, Func<int, int> key, int[] expected) =>
            CollectionAssert.AreEqual(expected, source.SortBy(key));

        [Test]
        public void SortBy_SortByWithBookListSortByPrice()
        {
            Book firstBook = new Book("2-266-11156-6", "Jack London", "Martin Eden", "Macmillan", 1909, 505, 40.23);
            Book secondBook = new Book("5-02-013850-9", "Fannie Flagg", "Fried Green Tomatoes at the Whistle Stop Cafe", "Random House", 1987, 403, 45.10);
            Book thirdBook = new Book("2-222-11143-8", "George Orwell", "Nineteen Eighty-Four", "Macmillan", 1949, 328, 19.84);
            Book fourthBook = new Book("3-222-12143-8", "Charles Dickens", "A Tale of Two Cities", "Chapman & Hall", 1859, 341, 20.23);
            var books = new List<Book>()
            {
                firstBook,
                secondBook,
                thirdBook,
                fourthBook
            };

            var expected = new List<Book>()
            {
                thirdBook,
                fourthBook,
                firstBook,
                secondBook
            };

            CollectionAssert.AreEqual(expected, books.SortBy(x => x.Price));
        }

        [Test]
        public void SortBy_SortByWithBookListSortByYear()
        {
            Book firstBook = new Book("2-266-11156-6", "Jack London", "Martin Eden", "Macmillan", 1909, 505, 40.23);
            Book secondBook = new Book("5-02-013850-9", "Fannie Flagg", "Fried Green Tomatoes at the Whistle Stop Cafe", "Random House", 1987, 403, 45.10);
            Book thirdBook = new Book("2-222-11143-8", "George Orwell", "Nineteen Eighty-Four", "Macmillan", 1949, 328, 19.84);
            Book fourthBook = new Book("3-222-12143-8", "Charles Dickens", "A Tale of Two Cities", "Chapman & Hall", 1859, 341, 20.23);
            var books = new List<Book>()
            {
                firstBook,
                secondBook,
                thirdBook,
                fourthBook
            };

            var expected = new List<Book>()
            {
                fourthBook,
                firstBook,
                thirdBook,
                secondBook
            };

            CollectionAssert.AreEqual(expected, books.SortBy(x => x.Year));
        }

        [Test]
        public void SortByDescending_SortByDescendingWithBookListSortByYear()
        {
            Book firstBook = new Book("2-266-11156-6", "Jack London", "Martin Eden", "Macmillan", 1909, 505, 40.23);
            Book secondBook = new Book("5-02-013850-9", "Fannie Flagg", "Fried Green Tomatoes at the Whistle Stop Cafe", "Random House", 1987, 403, 45.10);
            Book thirdBook = new Book("2-222-11143-8", "George Orwell", "Nineteen Eighty-Four", "Macmillan", 1949, 328, 19.84);
            Book fourthBook = new Book("3-222-12143-8", "Charles Dickens", "A Tale of Two Cities", "Chapman & Hall", 1859, 341, 20.23);
            var books = new List<Book>()
            {
                firstBook,
                secondBook,
                thirdBook,
                fourthBook
            };

            var expected = new List<Book>()
            {
                fourthBook,
                firstBook,
                thirdBook,
                secondBook
            };

            expected.Reverse();
            CollectionAssert.AreEqual(expected, books.SortByDescending(x => x.Year));
        }

        [Test]
        public void SortBy_SortByWithBookListSortByNumberOfPages()
        {
            Book firstBook = new Book("2-266-11156-6", "Jack London", "Martin Eden", "Macmillan", 1909, 505, 40.23);
            Book secondBook = new Book("5-02-013850-9", "Fannie Flagg", "Fried Green Tomatoes at the Whistle Stop Cafe", "Random House", 1987, 403, 45.10);
            Book thirdBook = new Book("2-222-11143-8", "George Orwell", "Nineteen Eighty-Four", "Macmillan", 1949, 328, 19.84);
            Book fourthBook = new Book("3-222-12143-8", "Charles Dickens", "A Tale of Two Cities", "Chapman & Hall", 1859, 341, 20.23);
            var books = new List<Book>()
            {
                firstBook,
                secondBook,
                thirdBook,
                fourthBook
            };

            var expected = new List<Book>()
            {
                thirdBook,
                fourthBook,
                secondBook,
                firstBook,
            };

            CollectionAssert.AreEqual(expected, books.SortBy(x => x.NumberOfPages));
        }

        [Test]
        public void SortByDescending_SortByDescendingWithBookListSortByNumberOfPages()
        {
            Book firstBook = new Book("2-266-11156-6", "Jack London", "Martin Eden", "Macmillan", 1909, 505, 40.23);
            Book secondBook = new Book("5-02-013850-9", "Fannie Flagg", "Fried Green Tomatoes at the Whistle Stop Cafe", "Random House", 1987, 403, 45.10);
            Book thirdBook = new Book("2-222-11143-8", "George Orwell", "Nineteen Eighty-Four", "Macmillan", 1949, 328, 19.84);
            Book fourthBook = new Book("3-222-12143-8", "Charles Dickens", "A Tale of Two Cities", "Chapman & Hall", 1859, 341, 20.23);
            var books = new List<Book>()
            {
                firstBook,
                secondBook,
                thirdBook,
                fourthBook
            };

            var expected = new List<Book>()
            {
                thirdBook,
                fourthBook,
                secondBook,
                firstBook,
            };

            expected.Reverse();
            CollectionAssert.AreEqual(expected, books.SortByDescending(x => x.NumberOfPages));
        }

        [Test]
        public void SortBy_SortByWithPointListSortByX()
        {
            var points = new List<Point>()
            {
                new Point(10, 10),
                new Point(25, 7),
                new Point(50, 15),
                new Point(70, 88),
                new Point(6, 4),
                new Point(4, 4),
                new Point(140, 68),
                new Point(62, 15)
            };

            var expected = new List<Point>
            {
                new Point(4, 4),
                new Point(6, 4),
                new Point(10, 10),
                new Point(25, 7),
                new Point(50, 15),
                new Point(62, 15),
                new Point(70, 88),
                new Point(140, 68)
            };

            CollectionAssert.AreEqual(expected, points.SortBy(x => x.X));
        }

        ////[Test]
        ////public void SortBy_SortByWithPointListWithoutCompareThrowsArgumentException()
        ////{
        ////    var points = new List<Point>()
        ////    {
        ////        new Point(10, 10),
        ////        new Point(25, 7),
        ////        new Point(50, 15),
        ////        new Point(70, 88),
        ////        new Point(6, 4),
        ////        new Point(4, 4),
        ////        new Point(140, 68),
        ////        new Point(62, 15)
        ////    };

        ////    var expected = new List<Point>
        ////    {
        ////        new Point(4, 4),
        ////        new Point(6, 4),
        ////        new Point(10, 10),
        ////        new Point(25, 7),
        ////        new Point(50, 15),
        ////        new Point(62, 15),
        ////        new Point(70, 88),
        ////        new Point(140, 68)
        ////    };

        ////    Assert.Throws<ArgumentException>(() => CollectionAssert.AreEqual(expected, points.SortBy(x => x)));
        ////}

        [Test]
        public void SortBy_SortByWithPointListWithComparer()
        {
            var points = new List<Point>()
            {
                new Point(10, 10),
                new Point(25, 7),
                new Point(50, 15),
                new Point(70, 88),
                new Point(6, 4),
                new Point(4, 4),
                new Point(140, 68),
                new Point(62, 15)
            };

            var expected = new List<Point>
            {
                new Point(4, 4),
                new Point(6, 4),
                new Point(10, 10),
                new Point(25, 7),
                new Point(50, 15),
                new Point(62, 15),
                new Point(70, 88),
                new Point(140, 68)
            };

            CollectionAssert.AreEqual(expected, points.SortBy(x => x, new ComparePointByX()));
        }

        [TestCase(1, 4, ExpectedResult = new int[] { 1, 2, 3, 4 })]
        [TestCase(-7, 3, ExpectedResult = new int[] { -7, -6, -5 })]
        public IEnumerable<int> GenerateIntegralNumbersTest(int start, int count) => PseudoEnumerable.Enumerable.GenerateIntegralNumbers(start, count);
    }

    public class ComparePointByX : IComparer<Point>
    {
        public int Compare(Point x, Point y)
        {
            if (x.X > y.X)
            {
                return 1;
            }
            else if (x.X == y.X)
            {
                return 0;
            }
            else return -1;
        }
    }
}