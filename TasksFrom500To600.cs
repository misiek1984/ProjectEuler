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
        #region Problem 504 - Square on the inside

/*
Let ABCD be a quadrilateral whose vertices are lattice points lying on the coordinate axes as follows:

A(a, 0), B(0, b), C(−c, 0), D(0, −d), where 1 ≤ a, b, c, d ≤ m and a, b, c, d, m are integers.

It can be shown that for m = 4 there are exactly 256 valid ways to construct ABCD. Of these 256 quadrilaterals, 42 of them strictly contain a square number of lattice points.

How many quadrilaterals ABCD strictly contain a square number of lattice points for m = 100?
*/

        /// <summary>
        /// 694687
        /// </summary>
        public static int Problem504()
        {
            //m=40 -> count = 45655
            //m=30 -> count = 19281
            //m=20 -> count = 5582
            //m=10 -> count = 862
            //m=4 -> count = 42

            var triangles = new Dictionary<int, Dictionary<int, List<int>>> (); 
            var m = 100;
            
            //a is a positive x coordinate
            for (var a = 1; a <= m; ++a)
            {
                var dict = triangles[a] = new Dictionary<int, List<int>>();
                //a is a negative x coordinate
                //There is no sense to check all pairs of a and c because of symetry
                for (var c = 1; c <= a; ++c) 
                {
                    var list = dict[c] = new List<int>();

                    //b is a negative positive x coordinate
                    for (var b = 1; b <= m; ++b)
                    {
                        list.Add(CountPointsInTriangleExcludeXAxis(a, b, c));
                    }
                }
            }

            var count = 0;
            foreach (var a in triangles)
            {
                foreach (var c in a.Value)
                {
                    var list = c.Value;
                    foreach (var numberOfPointsInUpperTriangle in list)
                        foreach (var numberOfPointsInLowerTriangle in list)
                        {
                            var numberOfPointsInsideOnXAxis = + a.Key + c.Key - 1;
                            var res = 
                                numberOfPointsInUpperTriangle + 
                                numberOfPointsInLowerTriangle +
                                numberOfPointsInsideOnXAxis;

                            if (res.IsSquare())
                            {
                                count++;

                                //Take into account a symetry with respect to the axis X
                                if (c.Key != a.Key)
                                {
                                    count++;
                                }
                            }
                        }
                }
            }

            return count;
        }

        private static int CountPointsInTriangleExcludeXAxis(int a, int b, int c)
        {
            var inside = 0;

            for (var x = -c; x <= 0; ++x)
                for (var y = 1; y < b; ++y)
                {
                    if (det(-c, 0, 0, b, x, y) < 0)
                        inside++;
                }

            for (var x = 1; x < a; ++x)
                for (var y = 1; y < b; ++y)
                {
                    if (det(0, b, a, 0, x, y) < 0)
                        inside++;
                }

            return inside;
        }

        /// <summary>
        /// http://www.izdebski.edu.pl/WykladySIT/WykladSIT_09.pdf
        /// http://www.matematyka.pl/264419.htm
        /// </summary>
        private static int det(int xa, int ya, int xb, int yb, int xc, int yc)
        {
            return xa*yb + xb*yc + xc*ya - ya*xb - yb*xc - yc*xa;
        }

        #endregion
    }
}
