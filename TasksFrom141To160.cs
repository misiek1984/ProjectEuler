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
        #region Problem 145 -  How many reversible numbers are there below one-billion

/*
Some positive integers n have the property that the sum [ n + reverse(n) ] consists entirely of odd (decimal) digits. 
For instance, 36 + 63 = 99 and 409 + 904 = 1313. We will call such numbers reversible; so 36, 63, 409, and 904 are reversible. 
Leading zeroes are not allowed in either n or reverse(n).

There are 120 reversible numbers below one-thousand.

How many reversible numbers are there below one-billion (109)?
 */

        public static int Problem145()
        {
            var max = 1000 * 1000 *1000;
            var count = 0;
            for (var i = 1; i < max; ++i)
            {
                var f = i.FirstDigit();
                if (i%10 == 0 || (i % 2 == 0 && f % 2 == 0) || (i % 2 == 1 && f % 2 == 1))
                    continue;

                var reverse = 0;
                var temp = i;
                while (temp != 0)
                {
                    reverse = 10*reverse + temp%10;
                    temp = temp/10;
                }

                temp = i + reverse;
                while (temp != 0)
                {
                    if (temp%2 == 0)
                        break;

                    temp = temp/10;
                }

                if (temp == 0)
                    count++;

            }

            return count;
        }



        #endregion
    }
}
