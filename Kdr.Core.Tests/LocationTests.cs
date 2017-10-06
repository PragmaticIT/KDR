using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kdr.Core.Tests
{
    /// <summary>
    /// Summary description for LocationTests
    /// </summary>
    [TestClass]
    public class LocationTests
    {
        public LocationTests()
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
        public void CanCompareToNull()
        {
            var sut = new Location();
            Assert.IsFalse(sut.Equals(null));

        }

        [TestMethod]
        public void LocationsCanCompareToOther()
        {
            var sut1 = new Location
            {
                Lat = "ABC",
                Long = "XYZ"
            };
            var sut2 = new Location
            {

                Lat = "ABC",
                Long = "XYZ"
            };
            Assert.IsTrue(sut1.Equals(sut2));
            Assert.AreEqual(sut1.GetHashCode(), sut2.GetHashCode());
            Assert.AreEqual(sut1, sut2);
        }
        [TestMethod]
        public void CanCompareEmptyLocations()
        {
            var sut = new Location();
            Assert.AreEqual(sut, new Location());

        }
    }
}
