using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectEuler
{
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HelperTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Permutations

        [TestMethod]
        public void Permutations_SetWith3Elements_5PermutationsShoudBeReturned()
        {
            var perm = new Permutations(new[] {1, 2, 3});
            var res = new List<string>();
            while (perm.GetNextPerm())
                res.Add(perm.Set.Select(i => i.ToString(CultureInfo.InvariantCulture)).Aggregate((a,b) => a + b));

            Assert.IsTrue(res.Count == 5);
            Assert.IsTrue(res.Contains("132"));
            Assert.IsTrue(res.Contains("213"));
            Assert.IsTrue(res.Contains("231"));
            Assert.IsTrue(res.Contains("312"));
            Assert.IsTrue(res.Contains("321"));
        }

        #endregion

        #region IsPermutation

        [TestMethod]
        public void IsPermutation_4digits_True()
        {
            Assert.IsTrue("1234".IsPermutation("4321"));
        }

        [TestMethod]
        public void IsPermutation_8digits_True()
        {
            Assert.IsTrue("12345678".IsPermutation("87123465"));
        }

        [TestMethod]
        public void IsPermutation_8symbols_True()
        {
            Assert.IsTrue("1234abcd".IsPermutation("a1b2c3d4"));
        }

        [TestMethod]
        public void IsPermutation_8SymbolsWithDuplications_True()
        {
            Assert.IsTrue("1144aaac".IsPermutation("1a4a1c4a"));
        }

        [TestMethod]
        public void IsPermutation_8SymbolsWithDuplications_False()
        {
            Assert.IsFalse("1144aaac".IsPermutation("1a4a1c4b"));
        }

        #endregion

        #region IsSquare

        [TestMethod]
        public void IsSquare_IntegerNumbersThatAreSquares_True()
        {
            Assert.IsTrue(100.IsSquare());
            Assert.IsTrue(22801.IsSquare());
            Assert.IsTrue(927369.IsSquare());
        }

        [TestMethod]
        public void IsSquare_IntegerNumbersThatAreNotSquares_False()
        {
            Assert.IsFalse(101.IsSquare());
            Assert.IsFalse(22803.IsSquare());
            Assert.IsFalse(927368.IsSquare());
        }

        #endregion

        #region ContinuedFractionsExpansion

        [TestMethod]
        public void ContinuedFractionsExpansionOfSquareRoot_2()
        {
            var res = 2.ContinuedFractionsExpansionOfSquareRoot();
            var expect = new List<int> { 1, 2 };

            Assert.AreEqual(expect.Count, res.Count);
            Enumerable.Range(0, expect.Count).ToList().ForEach(i => Assert.AreEqual(expect[i], res[i]));
        }

        [TestMethod]
        public void ContinuedFractionsExpansionOfSquareRoot_53()
        {
            var res = 53.ContinuedFractionsExpansionOfSquareRoot();
            var expect = new List<int> { 7, 3, 1, 1, 3, 14 };

            Assert.AreEqual(expect.Count, res.Count);
            Enumerable.Range(0, expect.Count).ToList().ForEach(i => Assert.AreEqual(expect[i], res[i]));
        }

        [TestMethod]
        public void ContinuedFractionsExpansionOfSquareRoot_100()
        {
            var res = 100.ContinuedFractionsExpansionOfSquareRoot();
            var expect = new List<int> { 10 };

            Assert.AreEqual(expect.Count, res.Count);
            Enumerable.Range(0, expect.Count).ToList().ForEach(i => Assert.AreEqual(expect[i], res[i]));
        }

        [TestMethod]
        public void ContinuedFractionsExpansionOfSquareRoot_200()
        {
            var res = 200.ContinuedFractionsExpansionOfSquareRoot();
            var expect = new List<int> { 14, 7, 28 };

            Assert.AreEqual(expect.Count, res.Count);
            Enumerable.Range(0, expect.Count).ToList().ForEach(i => Assert.AreEqual(expect[i], res[i]));
        }

        #endregion

        #region ContinuedFractionsValue
        
        [TestMethod]
        public void ContinuedFractionsValue_0thFractionFromSqrt2()
        {
            var list = 2.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(0);

            Assert.AreEqual(1, res.Item1);
            Assert.AreEqual(1, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_1thFractionFromSqrt2()
        {
            var list = 2.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(1);

            Assert.AreEqual(3, res.Item1);
            Assert.AreEqual(2, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_2thFractionFromSqrt2()
        {
            var list = 2.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(2);

            Assert.AreEqual(7, res.Item1);
            Assert.AreEqual(5, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_2thFractionFromSqrt3()
        {
            var list = 2.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(3);

            Assert.AreEqual(17, res.Item1);
            Assert.AreEqual(12, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_2thFractionFromSqrt4()
        {
            var list = 2.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(4);

            Assert.AreEqual(41, res.Item1);
            Assert.AreEqual(29, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_0thFractionFromSqrt53()
        {
            var list = 53.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(0);

            Assert.AreEqual(7, res.Item1);
            Assert.AreEqual(1, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_1thFractionFromSqrt53()
        {
            var list = 53.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(1);

            Assert.AreEqual(22, res.Item1);
            Assert.AreEqual(3, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_9thFractionFromSqrt53()
        {
            var list = 53.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(9);

            Assert.AreEqual(66249, res.Item1);
            Assert.AreEqual(9100, res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_77thFractionFromSqrt661()
        {
            var list = 661.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(77);

            Assert.AreEqual(BigInteger.Parse("16421658242965910275055840472270471049"), res.Item1);
            Assert.AreEqual(BigInteger.Parse("638728478116949861246791167518480580"), res.Item2);
        }

        [TestMethod]
        public void ContinuedFractionsValue_2thFractionFromSqrt100()
        {
            var list = 100.ContinuedFractionsExpansionOfSquareRoot();
            var res = list.ContinuedFractionsValue(2);

            Assert.AreEqual(10, res.Item1);
            Assert.AreEqual(1, res.Item2);
        }

        #endregion

        #region GreatestCommonDivisor

        [TestMethod]
        public void GreatestCommonDivisor_FirstNumberSmallerThanSecond_True()
        {
            Assert.AreEqual(10.GreatestCommonDivisor(100), 10);
            Assert.AreEqual(234.GreatestCommonDivisor(3211), 13);
            Assert.AreEqual(43231.GreatestCommonDivisor(453543), 17);
            Assert.AreEqual(0.GreatestCommonDivisor(453543), 453543);
        }

        [TestMethod]
        public void GreatestCommonDivisor_FirstNumberGreaterThanSecond_True()
        {
            Assert.AreEqual(100.GreatestCommonDivisor(10), 10);
            Assert.AreEqual(3211.GreatestCommonDivisor(234), 13);
            Assert.AreEqual(453543.GreatestCommonDivisor(43231), 17);
            Assert.AreEqual(453543.GreatestCommonDivisor(0), 453543);
        }

        #endregion

        #region EulersTotient

        [TestMethod]
        public void EulersTotient()
        {
            Assert.AreEqual(9.EulersTotient().Count, 6);
            Assert.AreEqual(1.EulersTotient().Count, 1);
            Assert.AreEqual(2.EulersTotient().Count, 1);
            Assert.AreEqual(91.EulersTotient().Count, 72);
            Assert.AreEqual(111.EulersTotient().Count, 72);
            Assert.AreEqual(1113.EulersTotient().Count, 624);
            Assert.AreEqual((-1).EulersTotient().Count, 1);
            Assert.AreEqual((-2).EulersTotient().Count, 1);
            Assert.AreEqual((-91).EulersTotient().Count, 72);
            Assert.AreEqual((-111).EulersTotient().Count, 72);
            Assert.AreEqual((-1113).EulersTotient().Count, 624);
        }

        #endregion

        #region EulersTotient

        [TestMethod]
        public void FirstDitig()
        {
            Assert.AreEqual(1.FirstDigit(), 1);
            Assert.AreEqual(345.FirstDigit(), 3);
            Assert.AreEqual(65765756.FirstDigit(),6);
            Assert.AreEqual(0.FirstDigit(), 0);
            Assert.AreEqual((-46553).FirstDigit(), 4);
        }

        #endregion

        #region IsPandigital

        [TestMethod]
        public void IsPandigital()
        {
            Assert.IsTrue(123456789L.IsPandigital());
            Assert.IsTrue(356987412L.IsPandigital());
            Assert.IsTrue(853214796L.IsPandigital());
            Assert.IsTrue(123L.IsPandigital());

            Assert.IsFalse(123567899L.IsPandigital());
            Assert.IsFalse(903564417L.IsPandigital());

            Assert.IsTrue(123456789.IsPandigital(9));
            Assert.IsTrue(356987412.IsPandigital(9));
            Assert.IsTrue(853214796.IsPandigital(9));
            Assert.IsTrue(123.IsPandigital(3));

            Assert.IsFalse(123567899.IsPandigital(9));
            Assert.IsFalse(903564417.IsPandigital(9));
        }

        #endregion
    }
}
