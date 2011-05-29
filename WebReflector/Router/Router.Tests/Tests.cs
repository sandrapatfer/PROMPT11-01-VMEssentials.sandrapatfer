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
            // register templates
            Router<IHandler>.Register("/", new RootHandler());
            Router<IHandler>.Register("/{ctx}", new ContextHandler());
            Router<IHandler>.Register("/{ctx}/as", new ContextAssembliesHandler());
            Router<IHandler>.Register("/{ctx}/ns", new ContextNamespacesHandler());
            Router<IHandler>.Register("/{ctx}/as/{assemblyName}", new AssemblyHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespacePrefix}", new NamespaceHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}", new TypeHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/m/{methodName}", new MethodHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/c", new ConstructorsHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}", new FieldHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/p/{propName}", new PropertyHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/e/{eventName}", new WebReflector.EventHandler());
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

        static IHtmlView HandleRequest(string uri)
        {
            // TODO pensar sobre os parametros, não sei se a ideia era criar sempre o dicionario
            Dictionary<string, string> parameters;
            var handler = Router<IHandler>.LookupHandler(uri, out parameters);
            return handler.Handle(parameters);
        }

        [TestMethod]
        public void test_response_type()
        {
            Reflector.RegisterContext("v4.0", @"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");

            IHtmlView response = HandleRequest("/");
            Assert.IsTrue(response is RootView, "response is RootView");

            response = HandleRequest("/v4.0");
            Assert.IsTrue(response is ContextView, "response is ContextView");

            response = HandleRequest("/v4.0/as");
            Assert.IsTrue(response is ContextAssembliesView, "response is ContextAssembliesView");

            response = HandleRequest("/v4.0/ns");
            Assert.IsTrue(response is ContextNamespacesView, "response is ContextNamespacesView");

            response = HandleRequest("/v4.0/as");
            Assert.IsTrue(response is ContextAssembliesView, "response is ContextAssembliesView");

            response = HandleRequest("/v4.0/as/System");
            Assert.IsTrue(response is AssemblyView, "response is AssemblyView");

            response = HandleRequest("/v4.0/ns/System");
            Assert.IsTrue(response is NamespaceView, "response is NamespaceView");

            response = HandleRequest("/v4.0/ns/System/Object");
            Assert.IsTrue(response is TypeView, "response is TypeView");

            response = HandleRequest("/v4.0/ns/System/String/m/CompareTo");
            Assert.IsTrue(response is MethodView, "response is MethodView");

            response = HandleRequest("/v4.0/ns/System/String/c");
            Assert.IsTrue(response is ConstructorsView, "response is ConstructorsView");

            response = HandleRequest("/v4.0/ns/System/String/f/OneField");
            Assert.IsTrue(response is FieldView, "response is FieldView");

            response = HandleRequest("/v4.0/ns/System/String/p/OneProp");
            Assert.IsTrue(response is PropertyView, "response is PropertyView");

            response = HandleRequest("/v4.0/ns/System/String/e/OneEvent");
            Assert.IsTrue(response is WebReflector.EventView, "response is EventView");
        }
    }
}
