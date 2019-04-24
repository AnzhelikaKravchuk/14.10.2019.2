using System;
using System.Collections.Generic;

namespace PseudoEnumerable.Tests
{
    class LetterACountComparer : IComparer<string>
    {
        public int Compare(string first, string second)
        {
            return LetterACounter(first) - LetterACounter(second);
        }

        private int LetterACounter(string input)
        {
            int counter = 0;

            foreach (var symbol in input)
            {
                if (symbol == 'A')
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
