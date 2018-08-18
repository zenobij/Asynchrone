using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryMath;

namespace UnitTestProjectMath
{
    /// <summary>
    /// Summary description for UnitTestPrimeNumber
    /// </summary>
    [TestClass]
    public class UnitTestPrimeNumber
    {
        public UnitTestPrimeNumber()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestPrimeNumberOne()
        {
            // Arrangement
            int number = 1;
            bool expectedValue = false;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsFalse(actualValue);
        }

        [TestMethod]
        public void TestPrimeNumberTwo()
        {
            // Arrangement
            int number = 2;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsTrue(actualValue);
        }

        [TestMethod]
        public void TestPrimeNumberThree()
        {
            // Arrangement
            int number = 3;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsTrue(actualValue);
        }

        [TestMethod]
        public void TestPrimeNumberFour()
        {
            // Arrangement
            int number = 4;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsFalse(actualValue);
        }

        [TestMethod]
        public void TestPrimeNumber97()
        {
            // Arrangement
            int number = 97;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsTrue(actualValue);
        }

        [TestMethod]
        public void BugPrimeNumber6()
        {
            // Arrangement
            int number = 6;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsFalse(actualValue);
        }

        [TestMethod]
        public void TestPrimeNumberBig()
        {
            // Arrangement
            int number = 179426549;
            PrimeNumber myPrime = new PrimeNumber();

            //Action
            bool actualValue = myPrime.IsPrimeNumber(number);

            // assert
            Assert.IsTrue(actualValue);
        }

    }
}
