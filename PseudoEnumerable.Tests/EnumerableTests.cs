using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using PseudoEnumerable;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        public void Test()
        {
            int[] array = new[] {1, 2, 3, 4};
            PseudoEnumerable.Enumerable.Where(array,x => x > 0);
        }
    }
}