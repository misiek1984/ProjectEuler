using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ProjectEuler
{
    public static class Helper
    {
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> f)
        {
            foreach (var item in e)
                f(item);
        }



        public static bool IsSquare(this int n)
        {
            var sqrt = Math.Sqrt(n);
            return sqrt % 1 == 0;
        }

        public static int NewtonSymbol(this int n, int k)
        {
            checked
            {
                var res = 1;

                for (var i = 1; i <= k; ++i)
                    res = res * (n - i + 1) / i;

                return res;
            }
        }

        public static int Factorial(this int number)
        {
            checked
            {
                if (number < 0)
                    throw new Exception("number < 0");

                switch (number)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 6;
                    case 4:
                        return 24;
                    case 5:
                        return 120;
                    case 6:
                        return 720;
                    case 7:
                        return 5040;
                    case 8:
                        return 40320;
                    case 9:
                        return 362880;
                    default:
                        return number * Factorial(number - 1);
                }
            }
        }

        public static IEnumerable<int> Rotations(this int number)
        {
            var list = new List<int> { number };
            var log = Math.Floor(Math.Log10(number));
            var m = (int)Math.Pow(10, log);
            for (var i = 1; i <= log; ++i)
            {
                number = m * (number % 10) + number / 10;
                list.Add(number);
            }

            return list;
        }

        public static int CharactersValue(this string characters)
        {
            return characters.Sum(ch => ch - 'A' + 1);
        }
        public static int DigitsValue(this string digits)
        {
            return digits.Sum(ch => ch - '0');
        }

        public static IList<int> EulersTotient(this int number)
        {
            if (number == 0)
                return new List<int>();

            number = Math.Abs(number);

            if (number == 1 || number == 2)
                return new List<int> { 1 };

            var relativePrimes = new List<int> { 1, number - 1 };

            var limit = number / 2.0;
            if (number%2 == 0)
            {
                for (var i = 3; i < limit; i += 2)
                    if (number.GreatestCommonDivisor(i) == 1)
                    {
                        relativePrimes.Add(i);
                        relativePrimes.Add(number - i);
    
                    }
            }
            else
            {
                for (var i = 2; i < limit; ++i)
                    if (number.GreatestCommonDivisor(i) == 1)
                    {
                        relativePrimes.Add(i);
                        relativePrimes.Add(number - i);
                    }
            }

            return relativePrimes;
        }



        /// <summary>
        /// Example: http://www.wolframalpha.com/input/?i=Convergents%5Bsqrt%283%29%2C10%5D
        /// </summary>
        /// <param name="expansion"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Tuple<BigInteger, BigInteger> ContinuedFractionsValue(this IList<int> expansion, int max = 10)
        {
            return expansion.ContinuedFractionsValue(0, max);
        }

        private static Tuple<BigInteger, BigInteger> ContinuedFractionsValue(this IList<int> expansion, int index, int max)
        {
            if (expansion.Count == 1)
                return new Tuple<BigInteger, BigInteger>(expansion[0], 1);

            var i = index;
            //Element a0 should be taken into account only once.
            if (index >= expansion.Count)
                i = (index - 1) % (expansion.Count - 1) + 1;

            if (index == max)
            {
                if (max == 0)
                    return new Tuple<BigInteger, BigInteger>(expansion[0], 1);

                return new Tuple<BigInteger, BigInteger>(1, expansion[i]);
            }

            var res = ContinuedFractionsValue(expansion, index + 1, max);

            if (index == 0)
                return new Tuple<BigInteger, BigInteger>(res.Item2*expansion[i] + res.Item1, res.Item2);

            return new Tuple<BigInteger, BigInteger>(res.Item2, res.Item2*expansion[i] + res.Item1);
        }

        /// <summary>
        /// http://en.wikipedia.org/wiki/Methods_of_computing_square_roots
        /// See "Continued fraction expansion"
        /// Example: http://www.wolframalpha.com/input/?i=continued+fraction+sqrt%283%29
        /// </summary>
        public static IList<int> ContinuedFractionsExpansionOfSquareRoot(this int number)
        {
            var res = new List<int>();

            var sqrt = Math.Sqrt(number);
            var floor = (int) Math.Floor(sqrt);

            var m = 0;
            var d = 1;
            var a = floor;

            res.Add(a);

            if (sqrt != floor)
                while (true)
                {
                    m = d*a - m;
                    d = (number - m*m)/d;
                    a = (int) Math.Floor((sqrt + m)/d);

                    res.Add(a);

                    if (a == 2*floor)
                        break;
                }

            return res;
        }



        public static bool IsPrime(this int number)
        {
            if (number <= 1)
                return false;

            var l = Math.Sqrt(number);
            for (var i = 2; i <= l; ++i)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static IEnumerable<int> Factorization(this int number)
        {
            var primes = ((int)Math.Sqrt(number)).GetPrimes().ToList();
            while (number != 1)
            {
                int prime;
                if (number.IsPrime())
                    prime = number;
                else
                    prime = primes.Where(p => p <= number).Where(p => number % p == 0).Min();
                number = number / prime;
                yield return prime;
            }
        }

        public static IEnumerable<int> GetPrimes(this int max)
        {
            for (var i = 2; i <= max; i++)
                if (i.IsPrime())
                    yield return i;
        }

        public static int GreatestCommonDivisor(this int a, int b)
        {
            while (b != 0)
            {
                var t = b;
                b = a%b;
                a = t;
            }

            return a;
        }

        public static IEnumerable<int> FindAllDivisors(this int number)
        {
            for (var i = 1; i < number; i++)
                if (number%i == 0)
                    yield return i;
        }



        public static string ToBinary(this int number)
        {
            return Convert.ToString(number, 2);
        }

        public static IList<int> ToDigits(this int number)
        {
            return
                number.ToString(CultureInfo.InvariantCulture)
                      .Select(d => Int32.Parse(d.ToString(CultureInfo.InvariantCulture)))
                      .ToList();
        }



        public static bool IsPolindrone(this string s)
        {
            if (s.Length == 0)
                return true;

            if (s.Length == 1)
                return true;

            for (var i = 0; i < s.Length/2; ++i)
                if (s[i] != s[s.Length - 1 - i])
                    return false;

            return true;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/2484892/fastest-algorithm-to-check-if-a-number-is-pandigital
        /// </summary>
        public static bool IsPandigital(this long n)
        {
            var digits = 0;
            var count = 0;
            var tmp = 0;

            while (n > 0)
            {
                tmp = digits;
                digits = digits | 1 << (int) ((n%10) - 1);
                if (tmp == digits)
                {
                    return false;
                }

                count++;
                n /= 10;
            }
            return digits == (1 << count) - 1;
        }

        public static bool IsPandigital(this int number, int n, bool includeZero = false)
        {
            var s = number.ToString(CultureInfo.InvariantCulture);
            return s.IsPandigital(n, includeZero);
        }

        public static bool IsPandigital(this string number, int n, bool includeZero = false)
        {
            var s = number.ToString(CultureInfo.InvariantCulture);

            if (n > 9 || n < 0)
                return false;

            if (!includeZero && s.Length != n || includeZero && s.Length != n + 1)
                return false;

            if (includeZero)
            {
                for (var i = 0; i <= n; ++i)
                    if (!s.Contains(i.ToString(CultureInfo.InvariantCulture)))
                        return false;
            }
            else
            {
                for (var i = 1; i < n; ++i)
                    if (!s.Contains(i.ToString(CultureInfo.InvariantCulture)))
                        return false;
            }

            return true;
        }



        public static bool IsPermutation(this string s, string s1)
        {
            if (s.Length != s1.Length)
                return false;

            var sort1 = String.Concat(s.OrderBy(ch => ch));
            var sort2 = String.Concat(s1.OrderBy(ch => ch));

            for (var i = 0; i < sort1.Count(); ++i)
                if (sort1[i] != sort2[i])
                    return false;
            //var chars =
            //    s.GroupBy(ch => ch).Select(g => new { Char = g.Key, Count = g.Count() }).OrderBy(g => g.Char).ToList();
            //var chars2 =
            //    s1.GroupBy(ch => ch).Select(g => new { Char = g.Key, Count = g.Count() }).OrderBy(g => g.Char).ToList();

            //return
            //    chars.Join(chars2, ch => ch.Char + "-" + ch.Count, ch => ch.Char + "-" + ch.Count, (o, i) => o).Count() ==
            //    chars.Count();

            return true;
        }

        public static bool IsPermutation(this int number, int number2)
        {
            return
                number.ToString(CultureInfo.InvariantCulture)
                      .IsPermutation(number2.ToString(CultureInfo.InvariantCulture));
        }

        public static bool IsPermutation(this long number, long number2)
        {
            return
                number.ToString(CultureInfo.InvariantCulture)
                      .IsPermutation(number2.ToString(CultureInfo.InvariantCulture));
        }

        public static IEnumerable<string> Permutations(this string s)
        {
            var res = new List<string>();

            if (s.Length == 1)
            {
                res.Add(s);
                return res;
            }

            for (var i = 0; i < s.Length; ++i)
                foreach (var p in Permutations(s.Substring(0, i) + s.Substring(i + 1, s.Length - i - 1)))
                    res.Add(s[i] + p);

            return res;
        }

        public static IEnumerable<int> Permutations(this int number)
        {
            var res = number.ToString(CultureInfo.InvariantCulture).Permutations();
            return res.Select(Int32.Parse);
        }

        public static IEnumerable<IList<int>> Permutations(this IEnumerable<int> set)
        {
            var perm = new Permutations(set);
            while (perm.GetNextPerm())
                yield return perm.Set;
        }


        public static int FirstDigit(this int i)
        {
            i = Math.Abs(i);

            if (i < 10)
                return i;
            if (i < 100)
                return i / 10;
            if (i < 1000)
                return i / 100;
            if (i < 10000)
                return i / 1000;
            if (i < 100000)
                return i / 10000;
            if (i < 1000000)
                return i / 100000;
            if (i < 10000000)
                return i / 1000000;
            if (i < 100000000)
                return i / 10000000;
            if (i < 1000000000)
                return i / 100000000;

            return i;
        }
    }

    /// <summary>
    /// http://www.mathblog.dk/project-euler-24-millionth-lexicographic-permutation/
    /// </summary>
    public class Permutations
    {
        public IList<int> Set { get; private set; }

        public Permutations(IEnumerable<int> set)
        {
            Set = new List<int>(set);
        }

        public bool GetNextPerm()
        {
            var N = Set.Count();
            var i = N - 1;

            while (Set[i - 1] >= Set[i])
            {
                i--;
                if (i < 1) return false;
            }

            var j = N;
            while (Set[j - 1] <= Set[i - 1])
            {
                j = j - 1;
            }

            // Swap values at position i-1 and j-1
            Swap(i - 1, j - 1);

            i++;
            j = N;

            while (i < j)
            {
                Swap(i - 1, j - 1);
                i++;
                j--;
            }

            return true;
        }

        private void Swap(int i, int j)
        {
            var k = Set[i];
            Set[i] = Set[j];
            Set[j] = k;
        }
    }
}
