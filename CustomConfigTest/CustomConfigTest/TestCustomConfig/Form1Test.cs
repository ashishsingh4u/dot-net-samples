using CustomConfigTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCustomConfig
{
    
    
    /// <summary>
    ///This is a test class for Form1Test and is intended
    ///to contain all Form1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Form1Test
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetRemoteOnly
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomConfigTest.exe")]
        public void GetRemoteOnlyTest()
        {
            Form1_Accessor target = new Form1_Accessor(); // TODO: Initialize to an appropriate value
            string section = "pageAppearanceGroup/pageAppearance";
            bool expected = true; 
            bool actual;
            actual = target.GetRemoteOnly(section);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProcessPendingTasks
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomConfigTest.exe")]
        public void ProcessPendingTasksTest()
        {
            Form1_Accessor target = new Form1_Accessor(); // TODO: Initialize to an appropriate value
            object state = null; // TODO: Initialize to an appropriate value
            bool signaled = false; // TODO: Initialize to an appropriate value
            target.ProcessPendingTasks(state, signaled);
        }
    }
}
