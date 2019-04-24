using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    /// <summary>
    /// Inverse comparer for type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements to compare.</typeparam>
    internal class InverseComparer<T> : IComparer<T>
    {
        private IComparer<T> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="{}"/> class.
        /// </summary>
        /// <param name="comparer"> Comparer to compare two elements.</param>
        public InverseComparer(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        /// <inheritdoc/>
        public int Compare(T x, T y)
        {
            int inverse = -this.comparer.Compare(x, y);

            if (this.comparer.Compare(x, y) > 0)
            {
                return inverse;
            }

            if (this.comparer.Compare(x, y) < 0)
            {
                return inverse;
            }

            return 0;
        }
    }
}
