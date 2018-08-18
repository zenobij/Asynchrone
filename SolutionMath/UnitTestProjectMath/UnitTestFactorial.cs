using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryMath;
using System.Numerics;

namespace UnitTestProjectMath
{
    [TestClass]
    public class UnitTestFactorial
    {
        [TestMethod]
        public void TestFactorialZero()
        {
            // Arrangement
            int number = 0;
            int expectedValue = 1;
            Factorial myFact = new Factorial();
            // Action
            BigInteger currentValue = myFact.Calculate(number);
            // Assert
            Assert.AreEqual(expectedValue, currentValue);
        }

        [TestMethod]
        public void TestFactorialOne()
        {
            // Arrangement
            int number = 1;
            int expectedValue = 1;
            Factorial myFact = new Factorial();
            // Action
            BigInteger currentValue = myFact.Calculate(number);
            // Assert
            Assert.AreEqual(expectedValue, currentValue);
        }

        [TestMethod]
        public void TestFactorialSixNormalCase()
        {
            // Arrangement
            int number = 6;
            int expectedValue = 720;
            Factorial myFact = new Factorial();
            // Action
            BigInteger currentValue = myFact.Calculate(number);
            // Assert
            Assert.AreEqual(expectedValue, currentValue);
        }
        [TestMethod]
        public void TestFactorial100()
        {
            // Arrangement
            int number = 100;
            BigInteger  expectedValue = BigInteger.Parse("93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000");
            Factorial myFact = new Factorial();
            // Action
            BigInteger currentValue = myFact.Calculate(number);
            // Assert
            Assert.AreEqual(expectedValue, currentValue);
        }

        [TestMethod]
        public void BugFactorial13()
        {
            // Arrangement
            int number = 13;
            BigInteger expectedValue = 6227020800;
            Factorial myFact = new Factorial();
            // Action
            BigInteger currentValue = myFact.Calculate(number);
            // Assert
            Assert.AreEqual(expectedValue, currentValue);
        }

        [TestMethod]
        public void BugFactorial21()
        {
            // Arrangement
            int number = 21;
            BigInteger expectedValue = 
                    BigInteger.Parse("51090942171709440000");
            Factorial myFact = new Factorial();
            // Action
            BigInteger currentValue = myFact.Calculate(number);
            // Assert
            Assert.AreEqual(expectedValue, currentValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFactorialNegativeNumber()
        {
            // arrangement
            int number = -5;
            Factorial myFact = new Factorial();
            // action
            BigInteger currentValue = myFact.Calculate(number);
        }
    }
}
