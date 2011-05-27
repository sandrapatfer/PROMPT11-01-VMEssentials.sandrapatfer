using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebReflector
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
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
        public void test_response_type()
        {
            Router r = new Router();

            IHtmlView response = r.HandleRequest("/");
            Assert.IsTrue(response is RootView, "response is RootView");

            response = r.HandleRequest("/folder");
            Assert.IsTrue(response is ContextView, "response is ContextView");

            response = r.HandleRequest("/folder/as");
            Assert.IsTrue(response is ContextAssembliesView, "response is ContextAssembliesView");

            response = r.HandleRequest("/folder/ns");
            Assert.IsTrue(response is ContextNamespacesView, "response is ContextNamespacesView");

            response = r.HandleRequest("/folder/as");
            Assert.IsTrue(response is ContextAssembliesView, "response is ContextAssembliesView");

            response = r.HandleRequest("/folder/as/System");
            Assert.IsTrue(response is AssemblyView, "response is AssemblyView");

            response = r.HandleRequest("/folder/ns/System");
            Assert.IsTrue(response is NamespaceView, "response is NamespaceView");

            response = r.HandleRequest("/folder/ns/System/String");
            Assert.IsTrue(response is TypeView, "response is TypeView");

            response = r.HandleRequest("/folder/ns/System/String/m/CompareTo");
            Assert.IsTrue(response is MethodView, "response is MethodView");

            response = r.HandleRequest("/folder/ns/System/String/c");
            Assert.IsTrue(response is ConstructorsView, "response is ConstructorsView");

            response = r.HandleRequest("/folder/ns/System/String/f/OneField");
            Assert.IsTrue(response is FieldView, "response is FieldView");
            
            response = r.HandleRequest("/folder/ns/System/String/p/OneProp");
            Assert.IsTrue(response is PropertyView, "response is PropertyView");

            response = r.HandleRequest("/folder/ns/System/String/e/OneEvent");
            Assert.IsTrue(response is WebReflector.EventView, "response is EventView");
        }
    }
}
