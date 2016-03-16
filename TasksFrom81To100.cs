using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MK.StateSpaceSearch;
using MK.StateSpaceSearch.DataStructures;
using MK.StateSpaceSearch.Fringe;

namespace ProjectEuler
{
    public partial class Solutions
    {
        #region Problem 81 - Path sum: two ways
/*
In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by only moving to the right and down, is indicated in bold red and is equal to 2427.

131	673	234	103	18
201	96	342	965	150
630	803	746	422	111
537	699	497	121	956
805	732	524	37	331

Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing a 80 by 80 matrix, 
from the top left to the bottom right by only moving right and down
*/
        public static int Problem81()
        {
            const int size = 80;
            var matrix = new int[size][];
            var d = new int[size][];
            for (var i = 0; i < matrix.Length; ++i)
            {
                matrix[i] = new int[size];
                d[i] = new int[size];
            }

            var y = 0;
            foreach (var s in File.ReadAllLines(@"Input\matrix.txt"))
            {
                var x = 0;
                foreach (var i in s.Split(','))
                {
                    matrix[y][x] = Int32.Parse(i);
                    d[y][x] = Int32.MaxValue;
                    x++;
                }
                y++;
            }

            d[0][0] = matrix[0][0];

            var sd = new List<Tuple<int, int>>();
            sd.Add(new Tuple<int, int>(0, 0));

            while (sd.Count > 0)
            {
                var c = sd[0];
                sd.RemoveAt(0);

                var x1 = c.Item1;
                var y1 = c.Item2;

                //Move right
                if (x1 < size - 1)
                {
                    var w = d[y1][x1] + matrix[y1][x1 + 1];
                    if (d[y1][x1 + 1] > w)
                    {
                        d[y1][x1 + 1] = w;
                        sd.Add(new Tuple<int, int>(x1 + 1, y1));
                    }
                }

                //Move down
                if (y1 < size - 1)
                {
                   
                    var w = d[y1][x1]  + matrix[y1 + 1][x1];
                    if (d[y1 + 1][x1] > w)
                    {
                        d[y1 + 1][x1] = w;
                        sd.Add(new Tuple<int, int>(x1, y1 + 1));
                    }
                }

                sd.Sort((t1, t2) => d[t1.Item2][t1.Item1].CompareTo(d[t2.Item2][t2.Item1]));
            }

            return d[size - 1][size - 1];
        }

        #endregion

        #region Problem 82 - Path sum: three ways

/*
The minimal path sum in the 5 by 5 matrix below, by starting in any cell in the left column and finishing in any cell in the right column, and only moving up, down, and right, is indicated in red and bold; the sum is equal to 994.

131	673	234	103	18
201	96	342	965	150
630	803	746	422	111
537	699	497	121	956
805	732	524	37	331

Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing a 80 by 80 matrix, from the left column to the right column.
*/
        public static int Problem82()
        {
            const int size =80;
            var matrix = new int[size][];
            var d = new int[size][];
            for (var i = 0; i < matrix.Length; ++i)
            {
                matrix[i] = new int[size];
                d[i] = new int[size];
            }

            var y = 0;
            foreach (var s in File.ReadAllLines(@"Input\matrix.txt"))
            {
                var x = 0;
                foreach (var i in s.Split(','))
                {
                    matrix[y][x] = Int32.Parse(i);
                    d[y][x] = Int32.MaxValue;
                    x++;
                }
                y++;
            }

            var min = Int32.MaxValue;
            for (var iteration = 0; iteration < size; ++iteration)
            {
                for (var i = 0; i < size; ++i)
                    for (var j = 0; j < size; ++j)
                        d[i][j] = Int32.MaxValue;

                d[iteration][0] = matrix[iteration][0];

                var sd = new List<Tuple<int, int>>();
                sd.Add(new Tuple<int, int>(0, iteration));

                while (sd.Count > 0)
                {
                    var c = sd[0];
                    sd.RemoveAt(0);

                    var x1 = c.Item1;
                    var y1 = c.Item2;

                    //Move right
                    if (x1 < size - 1)
                    {
                        var w = d[y1][x1] + matrix[y1][x1 + 1];
                        if (d[y1][x1 + 1] > w && w < min)
                        {
                            d[y1][x1 + 1] = w;
                            sd.Add(new Tuple<int, int>(x1 + 1, y1));
                        }
                    }

                    //Move down
                    if (y1 < size - 1)
                    {
                        var w = d[y1][x1] + matrix[y1 + 1][x1];
                        if (d[y1 + 1][x1] > w && w < min)
                        {
                            d[y1 + 1][x1] = w;
                            sd.Add(new Tuple<int, int>(x1, y1 + 1));
                        }
                    }

                    //Move up
                    if (y1 > 0)
                    {

                        var w = d[y1][x1] + matrix[y1 - 1][x1];
                        if (d[y1 - 1][x1] > w && w < min)
                        {
                            d[y1 - 1][x1] = w;
                            sd.Add(new Tuple<int, int>(x1, y1 - 1));
                        }
                    }

                    sd.Sort((t1, t2) => d[t1.Item2][t1.Item1].CompareTo(d[t2.Item2][t2.Item1]));
                }

                for (var i = 0; i < size; ++i)
                    min = Math.Min(min, d[i][size - 1]);
            }

            return min;
        }

        #endregion

        #region Problem 83 - Path sum: four ways

/*
NOTE: This problem is a significantly more challenging version of Problem 81.

In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by moving left, right, up, and down, is indicated in bold red and is equal to 2297.


131	673	234	103	18
201	96	342	965	150
630	803	746	422	111
537	699	497	121	956
805	732	524	37	331

Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing a 80 by 80 matrix, from the top left to the bottom right by moving left, right, up, and down.
*/
        public static int Problem83()
        {
            const int size = 80;
            var matrix = new int[size][];
            var d = new int[size][];
            for (var i = 0; i < matrix.Length; ++i)
            {
                matrix[i] = new int[size];
                d[i] = new int[size];
            }

            var y = 0;
            foreach (var s in File.ReadAllLines(@"Input\matrix.txt"))
            {
                var x = 0;
                foreach (var i in s.Split(','))
                {
                    matrix[y][x] = Int32.Parse(i);
                    d[y][x] = Int32.MaxValue;
                    x++;
                }
                y++;
            }

            d[0][0] = matrix[0][0];

            var sd = new List<Tuple<int, int>>();
            sd.Add(new Tuple<int, int>(0, 0));

            while (sd.Count > 0)
            {
                var c = sd[0];
                sd.RemoveAt(0);

                var x1 = c.Item1;
                var y1 = c.Item2;

                //Move left
                if (x1 > 0)
                {
                    var w = d[y1][x1] + matrix[y1][x1 - 1];
                    if (d[y1][x1 - 1] > w)
                    {
                        d[y1][x1 - 1] = w;
                        sd.Add(new Tuple<int, int>(x1 - 1, y1));
                    }
                }

                //Move right
                if (x1 < size - 1)
                {
                    var w = d[y1][x1] + matrix[y1][x1 + 1];
                    if (d[y1][x1 + 1] > w)
                    {
                        d[y1][x1 + 1] = w;
                        sd.Add(new Tuple<int, int>(x1 + 1, y1));
                    }
                }

                //Move down
                if (y1 < size - 1)
                {

                    var w = d[y1][x1] + matrix[y1 + 1][x1];
                    if (d[y1 + 1][x1] > w)
                    {
                        d[y1 + 1][x1] = w;
                        sd.Add(new Tuple<int, int>(x1, y1 + 1));
                    }
                }

                //Move up
                if (y1 > 0)
                {

                    var w = d[y1][x1] + matrix[y1 - 1][x1];
                    if (d[y1 - 1][x1] > w)
                    {
                        d[y1 - 1][x1] = w;
                        sd.Add(new Tuple<int, int>(x1, y1 - 1));
                    }
                }

                sd.Sort((t1, t2) => d[t1.Item2][t1.Item1].CompareTo(d[t2.Item2][t2.Item1]));
            }

            return d[size - 1][size - 1];
        }

        #endregion

        #region Problem 85 - Couting rectangles

        public static int Problem85()
        {
            var max = 2000000;
            var theBestRect = new Size(0, 0);
            var globalError = long.MaxValue;

            for (var width = 1; width < 1500; ++width)
                for (var height = 1; height <= width; ++height)
                {
                    var curRect = new Size(width, height);
                    var c = 0L;

                    for (var w = 1; w <= width; ++w)
                        for (var h = 1; h <= height; ++h)
                        {
                            c += Count(curRect, w, h);
                        }

                    if (c > max)
                        break;

                    var error = Math.Abs(max - c);
                    if (globalError > error)
                    {
                        theBestRect = curRect;
                        globalError = error;
                    }
                }

            return theBestRect.Width * theBestRect.Height;
        }

        private static long Count(Size bigRect, int w, int h)
        {
            return (bigRect.Height - h + 1) * (bigRect.Width - w + 1);
        }

        #endregion

        #region Problem 84

        public static string Problem84()
        {
            var board = new List<string>
                {
                    "GO", "A1", "CC1", "A2", "T1", "R1", "B1", "CH1", "B2", "B3", 
                    "JAIL", "C1", "U1", "C2", "C3", "R2", "D1", "CC2", "D2", "D3", 
                    "FP", "E1", "CH2", "E2", "E3", "R3", "F1", "F2", "U2", "F3", 
                    "G2J", "G1", "G2", "CC3", "G3", " R4", "CH3", "H1", "T2", "H2"
                };

            var currentCC = 0;
            var ccCards = new[]
                {
                    "GO", "G2J",
                    String.Empty, String.Empty, String.Empty, String.Empty,
                    String.Empty, String.Empty, String.Empty, String.Empty,
                    String.Empty, String.Empty, String.Empty, String.Empty,
                    String.Empty, String.Empty
                };

            var currentCH = 0;
            var chCards = new[]
                {
                    "GO", "G2J", "C1", "E3",
                    "H2", "R1", "NextR", "NextR",
                    "NextU", "GoBack3",
                    String.Empty, String.Empty,
                    String.Empty, String.Empty,
                    String.Empty, String.Empty
                };

            var counts = new Dictionary<string, int>();
            foreach (var cell in board)
                counts[cell] = 0;

            var rand1 = new Random((int)DateTime.Now.Ticks);
            var rand2 = new Random((int)DateTime.Now.Ticks);
            var index = 0;
            var doubles = 0;
            var sides = 4;
            for (var it = 0; it < 100000; ++it)
            {
                var firstDice = rand1.Next(sides) + 1;
                var secondDice = rand2.Next(sides) + 1;

                index = (index + firstDice + secondDice) % board.Count;
                var square = board[index];

                doubles = firstDice == secondDice ? ++doubles : 0;

                if (doubles == 3)
                    square = "G2J";

                if (square.Contains("CH"))
                {
                    var ch = chCards[currentCH];
                    currentCH = (currentCH + 1)%16;

                    if (!String.IsNullOrEmpty(ch))
                    {
                        if (ch == "NextR")
                        {
                            if (square == "CH1")
                                square = "R2";
                            else if (square == "CH2")
                                square = "R3";
                            else if (square == "CH3")
                                square = "R1";
                        }
                        else if (ch == "NextU")
                        {
                            if (square == "CH1")
                                square = "U1";
                            else if (square == "CH2")
                                square = "U2";
                            else if (square == "CH3")
                                square = "U1";
                        }
                        else if (ch == "GoBack3")
                            square = board[index - 3];
                        else
                            square = ch;
                    }
                }
                
                if (square.Contains("CC"))
                {
                    var cc = ccCards[currentCC];
                    currentCC = (currentCC + 1) % 16;

                     if (!String.IsNullOrEmpty(cc))
                         square = cc;
                }

                if (square == "G2J")
                {
                    square = "JAIL";
                    doubles = 0;
                }

                index = board.IndexOf(square);
                counts[square]++;
            }

            var ordered = counts.OrderByDescending(kvp => kvp.Value).ToList();
            var first = board.IndexOf(ordered[0].Key);
            var second = board.IndexOf(ordered[1].Key);
            var third = board.IndexOf(ordered[2].Key);
            return String.Format("{0} {1} {2}\n{3} {4} {5}\n{6} {7} {8}", 
                first, second, third, 
                ordered[0].Key, ordered[1].Key, ordered[2].Key,
                ordered[0].Value, ordered[1].Value, ordered[2].Value);
        }

        #endregion

        #region Problem 87 - Prime poer triples

        /*
The smallest number expressible as the sum of a prime square, prime cube, and prime fourth power is 28. 
In fact, there are exactly four numbers below fifty that can be expressed in such a way:

28 = 2^2 + 2^3 + 2^4
33 = 3^2 + 2^3 + 2^4
49 = 5^2 + 2^3 + 2^4
47 = 2^2 + 3^3 + 2^4

How many numbers below fifty million can be expressed as the sum of a prime square, prime cube, and prime fourth power?
*/
        public static int Problem87()
        {
            var max = 5 * Math.Pow(10, 7);
            var set = new HashSet<int>();
            var primes = 7072.GetPrimes().ToList();
            var squares = new int[primes.Count];
            var cubes = new int[primes.Count(p => p < 368)];
            var fourths = new int[primes.Count(p => p < 84)];

            for (var i = 0; i < squares.Length; ++i)
                squares[i] = (int)Math.Pow(primes[i], 2);

            for (var i = 0; i < cubes.Length; ++i)
                cubes[i] = (int)Math.Pow(primes[i], 3);

            for (var i = 0; i < fourths.Length; ++i)
                fourths[i] = (int)Math.Pow(primes[i], 4);

            foreach (int t2 in squares)
            {
                foreach (int t1 in cubes)
                {
                    foreach (int t in fourths)
                    {
                        var n = t2 + t1 + t;
                        if (n < max)
                            set.Add(n);
                        else
                            break;
                    }
                }
            }

            return set.Count();
        }

        #endregion

        #region Problem 89 - Roman numerals

        /*
The rules for writing Roman numerals allow for many ways of writing each number (see About Roman Numerals...). However, there is always a "best" way of writing a particular number.

For example, the following represent all of the legitimate ways of writing the number sixteen:

IIIIIIIIIIIIIIII
VIIIIIIIIIII
VVIIIIII
XIIIIII
VVVI
XVI

The last example being considered the most efficient, as it uses the least number of numerals.

The 11K text file, roman.txt (right click and 'Save Link/Target As...'), contains one thousand numbers written in valid, but not necessarily minimal, Roman numerals; that is, they are arranged in descending units and obey the subtractive pair rule (see About Roman Numerals... for the definitive rules for this problem).

Find the number of characters saved by writing each of these in their minimal form.

Note: You can assume that all the Roman numerals in the file contain no more than four consecutive identical units.
*/
        public static int Problem89()
        {
            var dict = new Dictionary<char, int>
                {
                    {'I', 1},
                    {'V', 5},
                    {'X', 10},
                    {'L', 50},
                    {'C', 100},
                    {'D', 500},
                    {'M', 1000}
                };

            var count = 0;
            var romans = File.ReadAllLines("Input\\romans.txt");

            foreach (var roman in romans)
            {
                var number = ToNumber(roman, dict);
                var newRoman = ToRoman(number);

                count += roman.Length - newRoman.Length;
            }

            return count;
        }

        private static string ToRoman(int number)
        {
            //http://stackoverflow.com/questions/7040289/converting-integers-to-roman-numerals
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); 
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new Exception("Something is wrong!");
        }

        private static int ToNumber(string roman, Dictionary<char, int> dict)
        {
            var number = 0;
            var prevValue = Int32.MaxValue;
            foreach (var c in roman)
            {
                var value = dict[c];

                if (value > prevValue)
                    number += value - 2*prevValue;
                else
                    number += value;

                prevValue = value;
            }

            return number;
        }

        #endregion

        #region Problem 92 - Square digit chains

/*
A number chain is created by continuously adding the square of the digits in a number to form a new number until it has been seen before.

For example,

44 → 32 → 13 → 10 → 1 → 1
85 → 89 → 145 → 42 → 20 → 4 → 16 → 37 → 58 → 89

Therefore any chain that arrives at 1 or 89 will become stuck in an endless loop. What is most amazing is that EVERY starting number will eventually arrive at 1 or 89.

How many starting numbers below ten million will arrive at 89?
 */

        public static int Problem92()
        {
            var max = Math.Pow(10, 7);
            var counter = 0;

            //This version works slower
            //var stopNumbers = new HashSet<int> {4, 16, 20, 37, 42, 85, 89, 145};
            //var stopNumbers2 = new HashSet<int> { 1, 13, 23, 44 };

            //foreach (var stopNumber in stopNumbers.ToArray())
            //{
            //    var i = stopNumber;
            //    while (i < max*10)
            //    {
            //        i.Permutations().ForEach(e => stopNumbers.Add(e));
            //        i *= 10;
            //    }
            //}


            //foreach (var stopNumber in stopNumbers2.ToArray())
            //{
            //    var i = stopNumber;
            //    while (i < max * 10)
            //    {
            //        i.Permutations().ForEach(e => stopNumbers2.Add(e));
            //        i *= 10;
            //    }
            //}

            for (var i = 1; i < max; ++i)
            {
                var c = i;
                while (true)
                {
                    var accu = 0;
                    while (c != 0)
                    {
                        var r = c%10;
                        accu += r*r;
                        c = c/10;
                    }
                    c = accu;

                    if (c == 89) //stopNumbers.Contains(c)
                    {
                        counter++;
                        break;
                    }

                    if (c == 1) //stopNumbers2.Contains(c)
                        break;
                }


            }

            return counter;
        }

        #endregion

        #region Problem 96 - Su Doku

/*
Su Doku (Japanese meaning number place) is the name given to a popular puzzle concept. Its origin is unclear, but credit must be attributed to Leonhard Euler who invented a similar, and much more difficult, puzzle idea called Latin Squares. The objective of Su Doku puzzles, however, is to replace the blanks (or zeros) in a 9 by 9 grid in such that each row, column, and 3 by 3 box contains each of the digits 1 to 9. Below is an example of a typical starting puzzle grid and its solution grid.

A well constructed Su Doku puzzle has a unique solution and can be solved by logic, although it may be necessary to employ "guess and test" methods in order to eliminate options (there is much contested opinion over this). The complexity of the search determines the difficulty of the puzzle; the example above is considered easy because it can be solved by straight forward direct deduction.

The 6K text file, sudoku.txt (right click and 'Save Link/Target As...'), contains fifty different Su Doku puzzles ranging in difficulty, but all with unique solutions (the first puzzle in the file is the example above).

By solving all fifty puzzles find the sum of the 3-digit numbers found in the top left corner of each solution grid; for example, 483 is the 3-digit number found in the top left corner of the solution grid above.
*/

        public static int Problem96()
        {
            var res = 0;
            var input = File.ReadAllLines(@"Input\sudoku.txt");
            var data = new SuDokuProblemData();

            for (var s = 0; s < 50; ++s)
            {
                for (var row = 0; row < 9; ++row)
                {
                    var line = input[s*10 + row + 1];
                    for (var col = 0; col < 9; ++col)
                        data[row, col] = Int32.Parse(line[col].ToString(CultureInfo.InvariantCulture));
                }

                Solve(data);
                res += data[0, 0] * 100 + data[0, 1] * 10 + data[0, 2];
            }

            return res;
        }

        public static bool Solve(SuDokuProblemData data)
        {
            if (data.NumberOfZeroes == 0)
                return true;

            for (var row = 0; row < 9; ++row)
            {
                for (var col = 0; col < 9; ++col)
                {
                    if (data._data[row][col] == 0)
                    {
                        for (var number = 9; number >= 1; --number)
                        {
                            if (!data.IsInBox(row, col, number) &&
                                !data.IsInRow(row, number) &&
                                !data.IsInColumn(col, number))
                            {
                                data[row, col] = number;
                                if (Solve(data))
                                    return true;
                                data[row, col] = 0;
                            }
                        }

                        return false;
                    }
                }
            }

            return data.NumberOfZeroes == 0;
        }

        public class SuDokuProblemData
        {
            public int[][] _data;

            public int NumberOfZeroes { get; private set; }

            public int this[int row, int col]
            {
                get { return _data[row][col]; }
                set
                {
                    var isZero = _data[row][col] == 0;

                    if (isZero && value != 0)
                        NumberOfZeroes--;
                    else if (!isZero && value == 0)
                        NumberOfZeroes++;

                    _data[row][col] = value;
                }
            }

            public SuDokuProblemData()
            {
                _data = new int[9][];
                for (var row = 0; row < 9; ++row)
                    _data[row] = new int[9];

                NumberOfZeroes = 81;
            }

            public bool IsInBox(int row, int col, int number)
            {
                var boxOffset = 3*(row/3);
                var colOffset = 3*(col/3);

                for (var boxRow = 0; boxRow < 3; ++boxRow)
                    for (var boxCol = 0; boxCol < 3; ++boxCol)
                        if (_data[boxOffset + boxRow][colOffset + boxCol] == number)
                            return true;

                return false;
            }

            public bool IsInRow(int row, int number)
            {
                for (var col = 0; col < 9; ++col)
                    if (_data[row][col] == number)
                        return true;

                return false;
            }

            public bool IsInColumn(int col, int number)
            {
                for (var row = 0; row < 9; ++row)
                    if (_data[row][col] == number)
                        return true;

                return false;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var row = 0; row < 9; ++row)
                {
                    sb.AppendLine();
                    for (var col = 0; col < 9; ++col)
                    {
                        sb.Append(_data[row][col]);
                    }
                }
                return sb.ToString();
            }
        }

        #endregion

        #region Problem 97

/*
The first known prime found to exceed one million digits was discovered in 1999, and is a Mersenne prime of the form 2^6972593−1; it contains exactly 2,098,960 digits. Subsequently other Mersenne primes, of the form 2p−1, have been found which contain more digits.

However, in 2004 there was found a massive non-Mersenne prime which contains 2,357,207 digits: 28433×2^7830457+1.

Find the last ten digits of this prime number.
 */

        public static string Problem97()
        {
            var n = new BigInteger(28433);
            n = n << 7830457;
            n += 1;
            n %= 10000000000;
            var s = n.ToString();
            return s;
        }

        #endregion

        #region Problem 99
/*
Comparing two numbers written in index form like 2^11 and 3^7 is not difficult, as any calculator would confirm that 211 = 2048 < 37 = 2187.

However, confirming that 632382^518061 > 519432^525806 would be much more difficult, as both numbers contain over three million digits.

Using base_exp.txt (right click and 'Save Link/Target As...'), a 22K text file containing one thousand lines with a base/exponent pair on each line, 
determine which line number has the greatest numerical value.

NOTE: The first two lines in the file represent the numbers in the example given above.
*/

        public static int Problem99()
        {
            var index = 1;
            var maxIndex = 0;
            var max = 0.0;
            foreach (var s in File.ReadAllLines(@"Input\base_exp.txt"))
            {
                var n = s.Split(',');
                var b = Int32.Parse(n[0]);
                var e = Int32.Parse(n[1]);
                var res = e*Math.Log(b);
                if (res > max)
                {
                    max = res;
                    maxIndex = index;
                }

                index++;
            }

            return maxIndex;
        }

        #endregion

        #region Problem 100 - Arranged probability
/*
If a box contains twenty-one coloured discs, composed of fifteen blue discs and six red discs, and two discs were taken at random, it can be seen that the probability of taking two blue discs, P(BB) = (15/21)×(14/20) = 1/2.

The next such arrangement, for which there is exactly 50% chance of taking two blue discs at random, is a box containing eighty-five blue discs and thirty-five red discs.

By finding the first arrangement to contain over 1012 = 1,000,000,000,000 discs in total, determine the number of blue discs that the box would contain.
 */

        /// <summary>
        /// http://www.wolframalpha.com/input/?i=2x%28x-1%29+%3D+n%28n-1%29
        /// </summary>
        /// <returns></returns>
        public static long Problem100()
        {
            var max = 1000000000000;

            var a = 3 - 2*Math.Sqrt(2);
            var b = 3 + 2*Math.Sqrt(2);
            var c = Math.Sqrt(2);

            var m = 0;

            while (true)
            {
                var m1 = Math.Pow(a, m);
                var m2 = Math.Pow(b, m);
                var n = 0.25 * (-m1 - c*m1 - m2 + c*m2 + 2);
                var x = 0.125 * (2 * m1 + c * m1 + 2 * m2 - c * m2 + 4);

                if (n > max)
                    return (long)Math.Ceiling(x);

                m++;
            }
        }

        #endregion
    }
}
