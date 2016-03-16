using System;
using System.Diagnostics;
using System.Linq;

namespace ProjectEuler
{
    public class Program
    {
        private static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine(Solutions.Problem504());
            sw.Stop();
            Console.WriteLine("Elapsed time: {0} ms", sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
