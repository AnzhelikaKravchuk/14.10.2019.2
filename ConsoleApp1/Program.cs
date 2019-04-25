using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PseudoEnumerable;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] some = new int[] { 2, 3, 1, 4, -2, 11, 123 };
            IEnumerable<int> numb = some.SortByDescending(x => x);

            foreach (var item in numb)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
