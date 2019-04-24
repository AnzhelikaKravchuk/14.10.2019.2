using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    public static class TestClasses
    {
        public static bool IsIntPalindrome(int number)
        {
            return IsPalindrome(number);
        }

        private static bool IsPalindrome(int number)
        {
            uint numberCast = (uint)((number < 0) ? -number + 0.5 : number + 0.5);
            uint digitsAmount = (uint)Math.Ceiling(Math.Log10(numberCast));
            uint dozens = (uint)Math.Pow(10, digitsAmount - 1);

            while (numberCast > 9)
            {
                if (numberCast % 10 != numberCast / dozens)
                {
                    return false;
                }

                numberCast = (numberCast % dozens) / 10;
                dozens = dozens / 100;
            }

            return true;
        }
    }
}
