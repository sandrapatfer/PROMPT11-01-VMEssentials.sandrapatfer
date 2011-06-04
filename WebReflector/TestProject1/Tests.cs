using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebReflector.Reflector;

namespace WebReflector
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class Tests
    {
        public Tests()
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
        [ExpectedException(typeof(Reflector.InvalidPathReflectorException))]
        public void test_register_invalid_path()
        {
            Reflector.Reflector.RegisterContext("ctx1", @"c:\XXX");
        }


        [TestMethod]
        public void test_register()
        {
            Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");
            Assert.AreEqual(Reflector.Reflector.GetContext("v4.0").GetNamespace("System").GetType("Object").Name, typeof(object).Name);
        }

        [TestMethod]
        public void test_namespace_tree()
        {
            ContextNamespace m_nspaceTree = new ContextNamespace();

            ContextNamespace nspace = m_nspaceTree.Find(".".Split('.'));
            Assert.AreEqual("Root", nspace.FriendlyName, "Find failed with object");

            ContextNamespace nspace1 = m_nspaceTree.FindOrCreateNamespace(typeof(System.Reflection.Assembly).Namespace.Split('.'));
            Assert.AreEqual(nspace1.FullName, "System.Reflection", "FindOrCreateNamespace failed with System.Reflection.Assembly");

            ContextNamespace nspace2 = m_nspaceTree.Find(typeof(System.Reflection.Assembly).Namespace.Split('.'));
            Assert.AreEqual(nspace2.FullName, "System.Reflection", "Find failed with System.Reflection.Assembly");

            ContextNamespace nspace3 = m_nspaceTree.FindOrCreateNamespace(typeof(object).Namespace.Split('.'));
            Assert.AreEqual(nspace3.FullName, "System", "FindOrCreateNamespace failed with object");

            ContextNamespace nspace4 = m_nspaceTree.Find(typeof(object).Namespace.Split('.'));
            Assert.AreEqual(nspace4.FullName, "System", "Find failed with object");

        } 
    }
}
