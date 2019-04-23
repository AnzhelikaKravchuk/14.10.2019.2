﻿using System;
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
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6 })]
        public IEnumerable<int> Filter_FiltersNumbers(IEnumerable<int> source)
            => Enumerable.Filter(source, x => x % 2 == 0);

        [TestCase(new string[] { "some", "string" }, 5, ExpectedResult = new string[] { "some" })]
        public IEnumerable<string> Filter_FiltersSrings(IEnumerable<string> source, int length)
            => Enumerable.Filter(source, x => x.Length < length);
    }
}