using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectEuler
{
    public partial class Solutions
    {
        #region Problem 205 - Dice Game

/*
Peter has nine four-sided (pyramidal) dice, each with faces numbered 1, 2, 3, 4.
Colin has six six-sided (cubic) dice, each with faces numbered 1, 2, 3, 4, 5, 6.

Peter and Colin roll their dice and compare totals: the highest total wins. The result is a draw if the totals are equal.

What is the probability that Pyramidal Pete beats Cubic Colin? Give your answer rounded to seven decimal places in the form 0.abcdefg
*/

        public static decimal Problem205()
        {
            var fourSidedResults = new SortedDictionary<int, decimal>();
            var sixSidedResults = new SortedDictionary<int, decimal>();

            for (var i = 9; i <= 36; ++i)
                fourSidedResults[i] = 0;

            for (var i = 6; i <= 36; ++i)
                sixSidedResults[i] = 0;

            Gen(fourSidedResults, 0, 4, 9);
            Gen(sixSidedResults, 0, 6, 6);

            var fourSidedProbabilities = new SortedDictionary<int, decimal>();
            var sixSidedProbabilities = new SortedDictionary<int, decimal>();

            var sum1 = fourSidedResults.Sum(kvp => kvp.Value);
            foreach(var kvp in fourSidedResults)
                fourSidedProbabilities[kvp.Key] = kvp.Value / sum1;

            var sum2 = sixSidedResults.Sum(kvp => kvp.Value);
            foreach (var kvp in sixSidedResults)
                sixSidedProbabilities[kvp.Key] = kvp.Value / sum2;

            var result = 0m;
            for (var i = 9; i <= 36; ++i)
            {
                for (var j = 6; j < i; ++j)
                {
                    result += fourSidedProbabilities[i]*sixSidedProbabilities[j];
                }
            }

            return Math.Round(result, 7);
        }

        private static void Gen(IDictionary<int, decimal> results, int current, int noOfSides, int numberOfDices)
        {
            if (numberOfDices == 0)
            {
                results[current]++;
                return;
            }

            for (var i = 1; i <= noOfSides; ++i)
                Gen(results, current + i, noOfSides, numberOfDices - 1);
        }

        #endregion

        #region Problem 206 - Concealed Square

/*
Find the unique positive integer whose square has the form 1_2_3_4_5_6_7_8_9_0,
where each “_” is a single digit.
*/

        public static int Problem206()
        {
            var min = 1010101030; //(int)Math.Sqrt(1020304050607080900);
            var max = 1389026630; // (int)Math.Sqrt(1929394959697989990);

            //for (var i = max; i > min; i -= 20)
            for (var i = min; i < max; i += 20)
            {
                if(i % 100 != 30 && i % 100 != 70)
                    continue;

                var big = BigInteger.Pow(i, 2);

                var res = big.ToString(CultureInfo.InvariantCulture);
                if (res[0] == '1' && res[2] == '2' &&
                    res[4] == '3' && res[6] == '4' &&
                    res[8] == '5' && res[10] == '6' &&
                    res[12] == '7' && res[14] == '8' &&
                    res[16] == '9' && res[18] == '0')
                    return i;
            }
            return -1;
        }

        #endregion
    }
}
