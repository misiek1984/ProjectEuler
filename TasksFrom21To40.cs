using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public partial class Solutions
    {
        #region Problem 21

        public static void Problem21()
        {
            var dict = new Dictionary<long, long>();

            for (var i = 1; i < 10000; ++i)
                dict[i] = i.FindAllDivisors().Sum();

            long sum = 0;
            foreach (var kvp in dict)
                if (kvp.Value != kvp.Key && dict.ContainsKey(kvp.Value) && dict[kvp.Value] == kvp.Key)
                {
                    Console.WriteLine(kvp.Key);
                    sum += kvp.Key;
                }

            Console.WriteLine(sum);
        }

        #endregion
        #region Problem 22

        public static void Problem22()
        {
            var names = File.ReadAllLines(@"Input\names.txt")[0].Split(new char[] { ',' });
            var list = new List<string>();
            foreach (var name in names)
                list.Add(name.Substring(1, name.Length - 2));

            list.Sort();

            decimal sum = 0;
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i].CharactersValue();

                sum += (i + 1) * temp;
            }

            Console.WriteLine(sum);
        }

        #endregion
        #region Problem 23

        public static void Problem23()
        {
            var set = new HashSet<int>();
            for (var i = 1; i <= 28123; ++i)
                if (i.FindAllDivisors().Sum() > i)
                    set.Add(i);

            var res = new List<int>();
            for (var i = 1; i <= 28123; ++i)
            {
                var flag = false;
                foreach (var abundant in set)
                {
                    if (set.Contains(i - abundant))
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    res.Add(i);
            }


            Console.WriteLine(res.Sum());
        }

        #endregion
        #region Problem 24

        /*
        A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:

        012   021   102   120   201   210

        What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?

        0 1 2 3 4 5 6 7 8 9
        2 * 9! = 362880 + 362880 = 725760
        2
         
        0 1 3 4 5 6 7 8 9
        725760 + 6 * 8! = 725760 + 6 * 40320 = 967680
        27
  
        0 1 3 4 5 6 8 9 
        967680 + 6 * 7! = 967680 + 6 * 5040 = 997920
        278

        0 1 3 4 5 6 9 
        997920 + 2 * 6! = 997920 + 2 * 720 =  999360
        2783
         
        0 1 4 5 6 9 
        999360 + 5 * 5! =  999360 + 5 * 120 = 999960
        27839
        
        0 1 4 5 6 
        999960 + 4! = 999960 + 24 = 999984
        278391
         
        0 4 5 6 
        999984 + 2 * 3! = 999984 + 2 *6 = 999996
        2783915
         
        0 4 6
        046   064   406   460   604   640
        2783915460
    
        */

        #endregion
        #region Problem 25

        //http://www.wolframalpha.com/input/?i=fibonacci+4782

        #endregion
        #region Problem 26

        public static void Problem26()
        {
            var max = 0;
            var maxDenominator = 0;
            for (var i = 1; i < 1000; ++i)
            {
                var sb = new StringBuilder();
                var m = Divide(1, i, new List<int>(), sb);
                if (m > max)
                {
                    max = m;
                    maxDenominator = i;
                }

                Console.WriteLine("1/" + i + " = " + sb[0] + "." + sb.ToString().Substring(1, sb.Length - 1));
            }

            Console.WriteLine(maxDenominator);
        }

        private static int Divide(int i, int denominator, ICollection<int> visited, StringBuilder sb)
        {
            if (visited.Contains(i))
            {
                var res = i / denominator;
                var s = sb.ToString();
                var start = s.IndexOf(res.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal);
                var cycle = s.Substring(start, s.Length - start);
                sb.Append(String.Format("({0})", cycle));
                return cycle.Length;
            }

            if (i == 0)
                return 0;

            visited.Add(i);

            if (i < denominator)
            {
                sb.Append("0");
                return Divide(i * 10, denominator, visited, sb);
            }

            sb.Append(i / denominator);
            return Divide(i % denominator * 10, denominator, visited, sb);
        }

        #endregion
        #region Problem 27 - Quadratic primes
        /*
Euler published the remarkable quadratic formula:

n² + n + 41

It turns out that the formula will produce 40 primes for the consecutive values n = 0 to 39. However, when n = 40, 402 + 40 + 41 = 40(40 + 1) + 41 is divisible by 41, and certainly when n = 41, 41² + 41 + 41 is clearly divisible by 41.

Using computers, the incredible formula  n²  79n + 1601 was discovered, which produces 80 primes for the consecutive values n = 0 to 79. The product of the coefficients, 79 and 1601, is 126479.

Considering quadratics of the form:

n² + an + b, where |a|  1000 and |b|  1000

where |n| is the modulus/absolute value of n
e.g. |11| = 11 and |4| = 4
Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n = 0.
*/
        public static void Problem27()
        {
            var maxA = 0;
            var maxB = 0;
            var maxLength = 0;

            for (var a = -1000; a <= 1000; ++a)
            {
                for (var b = -1000; b <= 1000; ++b)
                {
                    if (b.IsPrime())
                    {
                        var length = 0;
                        for (var i = 0; i < Int32.MaxValue; ++i)
                        {
                            if (!(i * i + a * i + b).IsPrime())
                                break;

                            length++;
                        }

                        if (length > maxLength)
                        {
                            maxLength = length;
                            maxA = a;
                            maxB = b;
                        }
                    }
                }
            }

            Console.WriteLine("maxLength=" + maxLength + " a=" + maxA + " b=" + maxB + " a*b=" + maxA * maxB);
        }

        #endregion
        #region Problem 28 - Number spiral diagonals

        /*
Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:

21 22 23 24 25
20  7  8  9 10
19  6  1  2 11
18  5  4  3 12
17 16 15 14 13

It can be verified that the sum of the numbers on the diagonals is 101.

What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
*/
        public static void Problem28()
        {
            var diagonal1 = 0;
            var diagonal2 = 1;
            var last = 1;
            var l = 1001 * 1001;
            var index = 1;
            while (last != l)
            {
                var height = Height(index);
                var a = last + height - 1;
                var b = a + height - 1;
                var c = b + height - 1;
                var d = c + height - 1;
                last = d;

                diagonal1 += a + c;
                diagonal2 += b + d;

                index++;
            }

            Console.WriteLine(diagonal1);
            Console.WriteLine(diagonal2);
            Console.WriteLine(diagonal1 + diagonal2);
        }

        public static int Height(int index)
        {
            if (index == 0)
                return 1;
            var height = 1;

            for (var i = 0; i < index; ++i)
                height += 2;

            return height;
        }

        public static int NumberOfElements(int index, int prevHeight)
        {
            return 2 * Height(index) + 2 * prevHeight;
        }

        #endregion
        #region Problem 29 - Distinct powers

        /*
Consider all integer combinations of ab for 2  a  5 and 2  b  5:

22=4, 23=8, 24=16, 25=32
32=9, 33=27, 34=81, 35=243
42=16, 43=64, 44=256, 45=1024
52=25, 53=125, 54=625, 55=3125
If they are then placed in numerical order, with any repeats removed, we get the following sequence of 15 distinct terms:

4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125

How many distinct terms are in the sequence generated by ab for 2  a  100 and 2  b  100?
*/
        public static void Problem29()
        {
            var hashset = new HashSet<double>();
            for (var a = 2; a <= 100; ++a)
            {
                for (var b = 2; b <= 100; ++b)
                {
                    var res = Math.Pow(a, b);
                    if (!hashset.Contains(res))
                        hashset.Add(res);
                }
            }

            Console.WriteLine(hashset.Count);
        }

        #endregion
        #region Problem 30 - Digit fifth powers

        /*
Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

1634 = 14 + 64 + 34 + 44
8208 = 84 + 24 + 04 + 84
9474 = 94 + 44 + 74 + 44
As 1 = 14 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
*/

        public static void Problem30()
        {
            var res =
                Enumerable.Range(2, 1000000)
                          .Where(
                              i => i == i.ToString().Sum(d => Math.Pow(Int32.Parse(d.ToString()), 5))).ToList();

            res.ForEach(Console.WriteLine);
            Console.WriteLine("Sum=" + res.Sum());
        }

        #endregion
        #region Problem 31 - Coin sums

        /*
In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:

1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
It is possible to make £2 in the following way:

1£1 + 150p + 220p + 15p + 12p + 31p
How many different ways can £2 be made using any number of coins?
*/

        public static void Problem31()
        {
            var count = 0;
            var sum = 0;
            for (var i = 0; i <= 200; ++i)
            {
                for (var j = 0; j <= 100; ++j)
                {
                    sum = i + 2 * j;
                    if (sum > 200)
                        continue;

                    for (var k = 0; k <= 40; ++k)
                    {
                        sum = i + 2 * j + 5 * k;
                        if (sum > 200)
                            continue;

                        for (var l = 0; l <= 20; ++l)
                        {
                            sum = i + 2 * j + 5 * k + 10 * l;
                            if (sum > 200)
                                continue;

                            for (var m = 0; m <= 10; ++m)
                            {
                                sum = i + 2 * j + 5 * k + 10 * l + 20 * m;
                                if (sum > 200)
                                    continue;

                                for (var n = 0; n <= 4; ++n)
                                {
                                    sum = i + 2 * j + 5 * k + 10 * l + 20 * m + 50 * n;
                                    if (sum > 200)
                                        continue;

                                    for (var x = 0; x <= 2; ++x)
                                    {
                                        sum = i + 2 * j + 5 * k + 10 * l + 20 * m + 50 * n + 100 * x;

                                        if (sum == 200)
                                            count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(count + 1);
        }

        #endregion
        #region Problem 32 - Pandigital products

        /*
 We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.

The product 7254 is unusual, as the identity, 39  186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.

Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.

HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.
*/

        public static void Problem32()
        {
            var perm = "123456789".Permutations();
            var hashet = new HashSet<int>();
            var sum = 0;
            foreach (var p in perm)
            {
                for (var i = 1; i < 8; ++i)
                {
                    var multiplicant = Int32.Parse(p.Substring(0, i));
                    for (var j = 1; j < p.Length - i; ++j)
                    {
                        var multiplier = Int32.Parse(p.Substring(i, j));
                        var product = Int32.Parse(p.Substring(i + j, p.Length - i - j));

                        if (!hashet.Contains(product) && multiplicant * multiplier == product)
                        {
                            hashet.Add(product);
                            sum += product;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
        }



        #endregion
        #region Problem 33 - Digit canceling fractions

        /*
The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

There are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.

If the product of these four fractions is given in its lowest common terms, find the value of the denominator
 */

        public static void Problem33()
        {
            var nom = 1;
            var denom = 1;
            for (var i = 10; i < 100; ++i)
                for (var j = i; j < 100; ++j)
                {
                    /*
                     i   x1x2
                     - = ---- 
                     j   y1y2
                     */
                    var x1 = (float)(i / 10);
                    var x2 = (float)(i % 10);
                    var y1 = (float)(j / 10);
                    var y2 = (float)(j % 10);
                    var f1 = i / (float)j;
                    float? f2 = null;

                    if (i != j)
                    {
                        if (x1 == y1)
                            f2 = x2 / y2;
                        else if (x1 == y2)
                            f2 = x2 / y1;
                        else if (x2 == y1)
                            f2 = x1 / y2;
                        else if (x2 == y1)
                            f2 = x1 / y1;

                        if (f1 == f2)
                        {
                            nom *= i;
                            denom *= j;
                            Console.WriteLine("{0}/{1}", i, j);
                        }
                    }
                }

            //Console.WriteLine("{0}/{1}", nom, denom);
            Console.WriteLine("{0}/{1}", nom / nom.GreatestCommonDivisor(denom), denom / nom.GreatestCommonDivisor(denom));
        }

        #endregion
        #region Problem 34 - Digit factorials

        /*
145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

Find the sum of all numbers which are equal to the sum of the factorial of their digits.

Note: as 1! = 1 and 2! = 2 are not sums they are not included.
*/

        public static void Problem34()
        {
            var source = Enumerable.Range(10, 3265920).AsParallel();
            var sum = source.Where(i => i.ToDigits().Aggregate(0, (a, b) => a + b.Factorial()) == i).Sum();

            Console.WriteLine(sum);
        }

        #endregion
        #region Problem 35 - Circular primes

        /*
The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.

There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.

How many circular primes are there below one million?
*/

        public static void Problem35()
        {
            var source = Enumerable.Range(2, 1000000).AsParallel();
            var res = source.Where(i => i.Rotations().All(a => a.IsPrime()));
            //res.ForAll(Console.WriteLine);
            Console.WriteLine(res.Count());
        }

        #endregion
        #region Problem 36 - Double-base palindromes

        /*
The decimal number, 585 = 10010010012 (binary), is palindromic in both bases.

Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

(Please note that the palindromic number, in either base, may not include leading zeros.?
*/

        public static void Problem36()
        {
            var source = Enumerable.Range(1, 1000000).AsParallel();
            var res = source.Where(i => i.ToString(CultureInfo.InvariantCulture).IsPolindrone() && i.ToBinary().IsPolindrone());
            Console.WriteLine(res.Sum());
        }

        #endregion
        #region Problem 37 - Truncatable primes

        /*
The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.

Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
*/

        public static void Problem37()
        {
            var source = Enumerable.Range(11, 1000000).AsParallel();
            var res = source.Where(i => i.IsPrime()).Where(j => RemoveLeftRight(j).All(k => k.IsPrime())).Select(l => l);
            res.ForAll(Console.WriteLine);
            Console.WriteLine("Sum=" + res.Sum());
        }

        public static IEnumerable<int> RemoveLeftRight(int number)
        {
            var list = new List<int>();
            var s = number.ToString(CultureInfo.InvariantCulture);
            for (var i = 1; i < s.Length; ++i)
            {
                list.Add(Int32.Parse(s.Substring(0, i)));
                list.Add(Int32.Parse(s.Substring(i, s.Length - i)));
            }
            return list;
        }


        #endregion
        #region Problem 38 - Pandigital multiples

        /*
Take the number 192 and multiply it by each of 1, 2, and 3:

192  1 = 192
192  2 = 384
192  3 = 576
By concatenating each product we get the 1 to 9 pandigital, 192384576. We will call 192384576 the concatenated product of 192 and (1,2,3)

The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, which is the concatenated product of 9 and (1,2,3,4,5).

What is the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of an integer with (1,2, ... , n) where n > 1?
*/

        public static void Problem38()
        {
            var max = 0;
            for (var i = 1; i < 10000; ++i)
            {
                max = Math.Max(max, HasPandigitalMultiples(i));
            }

            Console.WriteLine("Max=" + max);
        }

        private static int HasPandigitalMultiples(int number)
        {
            var s = number.ToString(CultureInfo.InvariantCulture);

            for (var i = 2; i < 10; ++i)
            {
                s += (i * number).ToString(CultureInfo.InvariantCulture);
                if (s.Contains("0") || s.Distinct().Count() != s.Length)
                    return -1;

                if (s.Length == 9)
                    return Int32.Parse(s);
            }

            return -1;
        }

        #endregion
        #region Problem 39 - Integer right triangles

        /*
If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

{20,48,52}, {24,45,51}, {30,40,50}

For which value of p  1000, is the number of solutions maximised?
*/
        public static void Problem39()
        {
            var max = 0;
            var maxValue = 0;
            for (var i = 3; i <= 1000; ++i)
            {
                var temp = NumberOfTriangles(i);
                if (temp > max)
                {
                    max = temp;
                    maxValue = i;
                }
            }

            Console.WriteLine("Max={0}, MaxValue={1}", max, maxValue);
        }

        private static int NumberOfTriangles(int p)
        {
            var count = 0;
            for (var a = 1; a < p - 1; ++a)
            {
                for (var b = 1; b < a; ++b)
                {
                    var c = p - a - b;
                    if (c > a && a * a + b * b == c * c)
                    {
                        //Console.WriteLine("{0}, {1}, {2}", a,b,c);
                        count++;
                    }
                }
            }

            return count;
        }

        #endregion
        #region Problem 40 - Champernowne's constant

        /*
An irrational decimal fraction is created by concatenating the positive integers:

0.123456789101112131415161718192021...

It can be seen that the 12th digit of the fractional part is 1.

If dn represents the nth digit of the fractional part, find the value of the following expression.

d1  d10  d100  d1000  d10000  d100000  d1000000
*/

        public static void Problem40()
        {
            var sb = new StringBuilder();
            var i = 1;
            while (sb.Length < 1000000)
                sb.Append(i++);

            Console.WriteLine(sb[0]);
            Console.WriteLine(sb[9]);
            Console.WriteLine(sb[99]);
            Console.WriteLine(sb[999]);
            Console.WriteLine(sb[9999]);
            Console.WriteLine(sb[99999]);
            Console.WriteLine(sb[999999]);
            Console.WriteLine(Int32.Parse(sb[0].ToString()) * Int32.Parse(sb[9].ToString()) * Int32.Parse(sb[99].ToString()) * Int32.Parse(sb[999].ToString()) * Int32.Parse(sb[9999].ToString()) * Int32.Parse(sb[99999].ToString()) * Int32.Parse(sb[999999].ToString()));
        }

        #endregion
    }
}
