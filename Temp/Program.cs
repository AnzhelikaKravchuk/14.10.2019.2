using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PseudoEnumerable;

namespace Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new List<string> { "a", "aa", "aaa", "aa", "ssss" };

            var result = source.Filter(s => s.Length >= 3).Transform(p => p.ToUpper());

            source.Add("dsass");

            foreach (var item in result)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();

            //var resultLinq = source.All<string>(s => s.Length > 3);
            //foreach (var item in resultLinq)
            //{
            //    Console.Write($"{item} ");
            //}
        }
    }
}
