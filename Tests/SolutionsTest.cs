using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectEuler
{
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem(@"Input\cipher.txt", "Input")]
    [DeploymentItem(@"Input\keylog.txt", "Input")]
    [DeploymentItem(@"Input\matrix.txt", "Input")]
    [DeploymentItem(@"Input\base_exp.txt", "Input")]
    [DeploymentItem(@"Input\triangles.txt", "Input")]
    [DeploymentItem(@"Input\romans.txt", "Input")]
    [DeploymentItem(@"Input\sudoku.txt", "Input")]
    public class SolutionsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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

        [TestMethod]
        public void Problem52Test()
        {
            Assert.AreEqual(142857, Solutions.Problem52());
        }

        [TestMethod]
        public void Problem53Test()
        {
            Assert.AreEqual(4075, Solutions.Problem53());
        }

        [TestMethod]
        public void Problem59Test()
        {
            Assert.AreEqual(107359, Solutions.Problem59());
        }

        [TestMethod]
        public void Problem62Test()
        {
            Assert.AreEqual(127035954683, Solutions.Problem62());
        }

        [TestMethod]
        public void Problem63Test()
        {
            Assert.AreEqual(49, Solutions.Problem63());
        }

        [TestMethod]
        public void Problem64Test()
        {
            Assert.AreEqual(1322, Solutions.Problem64());
        }

        [TestMethod]
        public void Problem65Test()
        {
            Assert.AreEqual(272, Solutions.Problem65());
        }

        [TestMethod]
        public void Problem66Test()
        {
            Assert.AreEqual(661, Solutions.Problem66());
        }

        [TestMethod]
        public void Problem68Test()
        {
            Assert.AreEqual("6531031914842725", Solutions.Problem68());
        }

        [TestMethod]
        public void Problem69Test()
        {
            Assert.AreEqual(510510, Solutions.Problem69());
        }

        [TestMethod]
        public void Problem70Test()
        {
            Assert.AreEqual(8319823, Solutions.Problem70());
        }

        [TestMethod]
        public void Problem71Test()
        {
            Assert.AreEqual(428570, Solutions.Problem71());
        }

        [TestMethod]
        public void Problem73Test()
        {
            Assert.AreEqual(7295372, Solutions.Problem73());
        }

        [TestMethod]
        public void Problem75Test()
        {
            Assert.AreEqual(161667, Solutions.Problem75());
        }

        [TestMethod]
        public void Problem77Test()
        {
            Assert.AreEqual(71, Solutions.Problem77());
        }

        [TestMethod]
        public void Problem78Test()
        {
            Assert.AreEqual(55374, Solutions.Problem78());
        }

        [TestMethod]
        public void Problem79Test()
        {
            Assert.AreEqual("73162890", Solutions.Problem79());
        }

        [TestMethod]
        public void Problem80Test()
        {
            Assert.AreEqual(40886, Solutions.Problem80());
        }

        [TestMethod]
        public void Problem81Test()
        {
            Assert.AreEqual(427337, Solutions.Problem81());
        }

        [TestMethod]
        public void Problem82Test()
        {
            Assert.AreEqual(260324, Solutions.Problem82());
        }

        [TestMethod]
        public void Problem83Test()
        {
            Assert.AreEqual(425185, Solutions.Problem83());
        }

        [TestMethod]
        public void Problem85Test()
        {
            Assert.AreEqual(2772, Solutions.Problem85());
        }

        [TestMethod]
        public void Problem87Test()
        {
            Assert.AreEqual(1097343, Solutions.Problem87());
        }

        [TestMethod]
        public void Problem89Test()
        {
            Assert.AreEqual(743, Solutions.Problem89());
        }

        [TestMethod]
        public void Problem92Test()
        {
            Assert.AreEqual(8581146, Solutions.Problem92());
        }

        [TestMethod]
        public void Problem96Test()
        {
            Assert.AreEqual(24702, Solutions.Problem96());
        }

        [TestMethod]
        public void Problem97Test()
        {
            Assert.AreEqual("8739992577", Solutions.Problem97());
        }

        [TestMethod]
        public void Proble99Test()
        {
            Assert.AreEqual(709, Solutions.Problem99());
        }

        [TestMethod]
        public void Proble100Test()
        {
            Assert.AreEqual(756872327473, Solutions.Problem100());
        }
        
        [TestMethod]
        public void Proble102Test()
        {
            Assert.AreEqual(228, Solutions.Problem102());
        }

        [TestMethod]
        public void Proble104Test()
        {
            Assert.AreEqual(329468, Solutions.Problem104());
        }

        [TestMethod]
        public void Proble112Test()
        {
            Assert.AreEqual(1587000, Solutions.Problem112());
        }

        [TestMethod]
        public void Proble145Test()
        {
            //Assert.AreEqual(608720, Solutions.Problem145());
        }

        [TestMethod]
        public void Proble205Test()
        {
            Assert.AreEqual(0.5731441m, Solutions.Problem205());
        }
    }
}
