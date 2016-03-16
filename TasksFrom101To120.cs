using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public partial class Solutions
    {
        #region Problem 102 -  Triangle containment

/*
Three distinct points are plotted at random on a Cartesian plane, for which -1000 ≤ x, y ≤ 1000, such that a triangle is formed.

Consider the following two triangles:

A(-340,495), B(-153,-910), C(835,-947)

X(-175,41), Y(-421,-714), Z(574,-645)

It can be verified that triangle ABC contains the origin, whereas triangle XYZ does not.

Using triangles.txt (right click and 'Save Link/Target As...'), a 27K text file containing the co-ordinates of one thousand "random" triangles, find the number of triangles for which the interior contains the origin.

NOTE: The first two examples in the file represent the triangles in the example given above.
 */

        public static int Problem102()
        {
            var count = 0;
            var triangles = File.ReadAllLines("Input\\triangles.txt");

            foreach (var triangle in triangles)
            {
                var coord = triangle.Split(',').Select(Int32.Parse).ToArray();
                var p1 = new Point(coord[0], coord[1]);
                var p2 = new Point(coord[2], coord[3]);
                var p3 = new Point(coord[4], coord[5]);

                var intersections = 0;

                if (Intersect(p1, p2))
                    intersections++;

                if (Intersect(p2, p3))
                    intersections++;

                if (Intersect(p3, p1))
                    intersections++;

                if (intersections % 2 == 1)
                    count++;
            }

            return count;
        }

        private static bool Intersect(Point p1, Point p2)
        {
            //Sprawdzam czy punkty leżą po przeciwnej stronie prostej y = x
            if (DiffSign(p1.Y - p1.X, p2.Y - p2.X))
            {
                //Wyznaczam równanie prostej przechodzącej przez te dwa punkty
                if (p2.X != p1.X)
                {
                    var a = (p2.Y - p1.Y)/(decimal) (p2.X - p1.X);
                    var b = p1.Y - a*p1.X;

                    //Wyznaczamy punkt przecięcia z y = x
                    // y = ax + b
                    // x = ax + b
                    // x = (1 - a) / b

                    var x = (1 - a)/b;

                    //Jeśli x > 0 to przecina śię z y = x w I ćwiartce
                    if (x > 0)
                        return true;
                }
                else
                {
                    if (p1.X > 0)
                        return true;
                }

            }

            return false;
        }
        private static bool DiffSign(int x, int y)
        {
            if (x > 0 && y < 0 || x < 0 && y > 0)
                return true;

            return false;
        }

        #endregion

        #region Problem 104 -  Pandigital Fibonacci ends

/*
The Fibonacci sequence is defined by the recurrence relation:

Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
It turns out that F541, which contains 113 digits, is the first Fibonacci number for which the last nine digits are 1-9 pandigital 
(contain all the digits 1 to 9, but not necessarily in order). And F2749, which contains 575 digits, 
is the first Fibonacci number for which the first nine digits are 1-9 pandigital.

Given that Fk is the first Fibonacci number for which the first nine digits AND the last nine digits are 1-9 pandigital, find k.
 */

        public static int Problem104()
        {
            var i = 1;
            var prevTrailingDigits = 0L;
            var curTrailingDigits = 1L;

            const int Modulo = 1000000000;

            //N apodstawie uproszczonego wzoru Bineta
            const double A = 0.44721359549995793928183473374626;
            const double B = 1.6180339887498948482045868343656;

            double logA = Math.Log10(A);
            double logB = Math.Log10(B);

            //160500643816367088
            while (curTrailingDigits.ToString(CultureInfo.InvariantCulture).Length < 18)
            {
                var temp = curTrailingDigits;
                curTrailingDigits = curTrailingDigits + prevTrailingDigits;
                prevTrailingDigits = temp;
                i++;
            }

            curTrailingDigits = curTrailingDigits % Modulo;
            prevTrailingDigits = prevTrailingDigits % Modulo;

            while (true)
            {
                if (curTrailingDigits.IsPandigital())
                {
                    var x = logA + i*logB;
                    //10^11.1 = 1 25892541179,41672104239541063958
                    //10^0.1  = 1,2589254117941672104239541063958
                    var fn = Math.Pow(10, x - (long)x);
                    var leading = (long)(fn * Modulo / 10);

                    if (leading.IsPandigital())
                        return i;
                }


                var temp = curTrailingDigits;
                curTrailingDigits = curTrailingDigits + prevTrailingDigits;
                prevTrailingDigits = temp;

                curTrailingDigits = curTrailingDigits % Modulo;
                prevTrailingDigits = prevTrailingDigits % Modulo;

                i++;
            }
        }

        public static int Problem104Old()
        {
            var i =1;
            BigInteger prev = 0;
            BigInteger cur = 1;
            while (cur.ToString().Length < 18)
            {
                var temp = cur;
                cur = cur + prev;
                prev = temp;
                i++;
            }

            
            while (true)
            {
                var tail = (long)(cur % 1000000000);
                if (tail.IsPandigital())
                {
                    var l = (int) (BigInteger.Log10(cur) + 1);
                    var head = (long) (cur/BigInteger.Pow(10, l - 9));

                    if (head.IsPandigital())
                        return i;
                }
                

                var temp = cur;
                cur = cur + prev;
                prev = temp;
                i++;
            }
        }



        #endregion

        #region Problem 112 - Bouncy numbers
        /*
Working from left-to-right if no digit is exceeded by the digit to its left it is called an increasing number; for example, 134468.

Similarly if no digit is exceeded by the digit to its right it is called a decreasing number; for example, 66420.

We shall call a positive integer that is neither increasing nor decreasing a "bouncy" number; for example, 155349.

Clearly there cannot be any bouncy numbers below one-hundred, but just over half of the numbers below one-thousand (525) are bouncy. In fact, the least number for which the proportion of bouncy numbers first reaches 50% is 538.

Surprisingly, bouncy numbers become more and more common and by the time we reach 21780 the proportion of bouncy numbers is equal to 90%.

Find the least number for which the proportion of bouncy numbers is exactly 99
*/

        public static int Problem112()
        {
            var i = 1;
            var c = 0m;
            while (true)
            {
                if (IsBouncy(i))
                    c++;

                if (c/i == 0.99m)
                    return i;

                ++i;
            }
        }

        private static bool IsBouncy(int i)
        {
            if (i < 100)
                return false;

            var current = i%10;
            i = i/10;

            var incr = 0;
            var dec = 0;
            while(i != 0)
            {
                var next = i%10;
                if (next > current)
                    dec++;
                else if (next < current)
                    incr++;

                if (dec > 0 && incr > 0)
                    return true;

                current = next;
                i = i / 10;
            }

            return false;
        }

        #endregion
    }
}
