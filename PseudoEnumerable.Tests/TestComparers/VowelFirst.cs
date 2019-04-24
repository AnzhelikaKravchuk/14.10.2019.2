using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests.TestComparers
{
    public class VowelFirst : IComparer<char>
    {
        private string vowels = "aeiou";

        public int Compare(char x, char y)
        {
            if (vowels.Contains(x) && !vowels.Contains(y))
            {
                return -1;
            }
            else if (!vowels.Contains(x) && vowels.Contains(y))
            {
                return 1;
            }

            return 0;
        }
    }
}
