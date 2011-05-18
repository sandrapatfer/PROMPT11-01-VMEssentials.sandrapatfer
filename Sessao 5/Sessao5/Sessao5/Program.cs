using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RationalNumber> rnumbers = new List<RationalNumber> {
                new RationalNumber(1,1),
                new RationalNumber(2,1)
            };
            var n = rnumbers.Where(r => r.Equals(new RationalNumber(1, 1)));
            foreach (var r in n)
                Console.WriteLine(r);
            rnumbers.Sort();
            //rnumbers.Sort((r1, r2) => r1.CompareTo(r2));
            foreach (var r in rnumbers)
                Console.WriteLine(r);
        }
    }
}
