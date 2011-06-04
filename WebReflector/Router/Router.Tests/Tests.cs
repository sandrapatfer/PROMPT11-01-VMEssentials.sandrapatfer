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

        static Router<IHandler> ConfigHandler()
        {
            // register templates
            var rootTemplate = new RoutingTemplate("/");
            var contextTemplate = new RoutingTemplate("/{ctx}");
            var ctxAssembliesTemplate = new RoutingTemplate("/{ctx}/as");
            var ctxNamespacesTemplate = new RoutingTemplate("/{ctx}/ns");
            var assemblyTemplate = new RoutingTemplate("/{ctx}/as/{assemblyName}");
            var namespaceTemplate = new RoutingTemplate("/{ctx}/ns/{namespacePrefix}");
            var typeTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}");
            var typeMethodTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/m/{methodName}");
            var typeConstructorsTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/c");
            var typeFieldTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}");
            var typePropertyTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/p/{propName}");
            var typeEventTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/e/{eventName}");

            // register templates to be mapped to handlers on requests
            var handlerRouter = new Router<IHandler>();
            handlerRouter.Register(rootTemplate, new RootHandler());
            handlerRouter.Register(contextTemplate, new ContextHandler());
            handlerRouter.Register(ctxAssembliesTemplate, new ContextAssembliesHandler());
            handlerRouter.Register(ctxNamespacesTemplate, new ContextNamespacesHandler());
            handlerRouter.Register(assemblyTemplate, new AssemblyHandler());
            handlerRouter.Register(namespaceTemplate, new NamespaceHandler());
            handlerRouter.Register(typeTemplate, new TypeHandler());
            handlerRouter.Register(typeMethodTemplate, new MethodHandler());
            handlerRouter.Register(typeConstructorsTemplate, new ConstructorsHandler());
            handlerRouter.Register(typeFieldTemplate, new FieldHandler());
            handlerRouter.Register(typePropertyTemplate, new PropertyHandler());
            handlerRouter.Register(typeEventTemplate, new WebReflector.EventHandler());

            return handlerRouter;
        }

        static IHtmlView HandleRequest(string uri)
        {
            var handlerRouter = ConfigHandler();
            Dictionary<string, string> parameters;
            var handler = handlerRouter.LookupHandler(uri, out parameters);
            return handler.Handle(parameters);
        }

        [TestMethod]
        public void test_response_type()
        {
//            Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");
            Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");

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

            response = HandleRequest("/v4.0/ns/System/String/f/Empty");
            Assert.IsTrue(response is FieldView, "response is FieldView");

            response = HandleRequest("/v4.0/ns/System/Array/p/Length");
            Assert.IsTrue(response is PropertyView, "response is PropertyView");

            response = HandleRequest("/v4.0/ns/System.Windows.Forms/Form/e/Activated");
            Assert.IsTrue(response is WebReflector.EventView, "response is EventView");
        }

        [TestMethod]
        public void test_context_template_register()
        {
            var ctxAssembliesTemplate = new RoutingTemplate("/{ctx}/as");
            ctxAssembliesTemplate.Register(typeof(Reflector.Context), "ContextAssembliesUri");
            Assert.AreEqual(ctxAssembliesTemplate, Reflector.Context.AssemblyUriTemplate);
        }
    }
}
