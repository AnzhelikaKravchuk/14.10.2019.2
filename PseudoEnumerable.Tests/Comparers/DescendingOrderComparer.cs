using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests.Comparers
{
    /// <summary>
    /// Class that compare two integers.
    /// </summary>
    public class DescendingOrderComparer : IComparer<int>
    {
        /// <summary>
        /// Compare two integers
        /// </summary>
        /// <param name="x">First integer to compare.</param>
        /// <param name="y">Second integer to compare.</param>
        /// <returns>
        ///     if <paramref name="x"/> more than <paramref name="y"/> return negative number;
        ///     if <paramref name="x"/> less than <paramref name="y"/> return positive number;
        ///     if <paramref name="x"/> is equal to the <paramref name="y"/> return zero;
        /// </returns>
        public int Compare(int x, int y)
        {
            return y - x;
        }
    }
}
