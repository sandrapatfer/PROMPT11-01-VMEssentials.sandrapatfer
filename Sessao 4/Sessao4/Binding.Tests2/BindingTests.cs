using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Binding.Tests2
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BindingTests
    {
        public BindingTests()
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

        class A
        {
            [BindableAttribute (Required=false, Name="AnInteger")]
            public int TheInteger { get; set; }
            [BindableAttribute(Required = false, Name = "AString")]
            public string TheString { get; set; }
            [BindableAttribute(Required = true, Name = "AFlag")]
            public bool TheFlag;
        }

        [TestMethod]
        public void can_bind1_to_string_int_and_bool()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("TheInteger", "2"),
                new KeyValuePair<string, string>("TheString","123"),
                new KeyValuePair<string, string>("TheFlag","true")
            };
            var a = binder.BindTo1<A>(pairs);

            Assert.AreEqual(2, a.TheInteger);
            Assert.AreEqual("123", a.TheString);
            Assert.AreEqual(true, a.TheFlag);
        }

        [TestMethod]
        public void can_bind2_to_string_int_and_bool()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("AnInteger", "2"),
                new KeyValuePair<string, string>("AString","123"),
                new KeyValuePair<string, string>("AFlag","true")
            };
            var a = binder.BindTo2<A>(pairs);

            Assert.AreEqual(2, a.TheInteger);
            Assert.AreEqual("123", a.TheString);
            Assert.AreEqual(true, a.TheFlag);
        }

        class B
        {
            [BindableAttribute(Required = true, Name = "AttrInteger")]
            public int AnInteger { get; set; }
            [BindableAttribute(Required = true, Name = "AttrString")]
            public string AString { get; set; }
            [BindableAttribute(Required = false, Name = "AttrPoint")]
            public Point ThePoint;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidMemberTypeException))]
        public void bind1_when_member_has_invalid_type_throws_exception()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("AnInteger", "2"),
                new KeyValuePair<string, string>("AString","123"),
                new KeyValuePair<string, string>("ThePoint","1,2")
            };
            try
            {
                var b = binder.BindTo1<B>(pairs);
            }
            catch (InvalidMemberTypeException ex)
            {
                Assert.AreEqual(typeof(B).GetField("ThePoint"), ex.MemberInfo);
                Assert.AreEqual(typeof(B).GetField("ThePoint").FieldType, ex.MemberType);
                throw ex;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidMemberTypeException))]
        public void bind2_when_member_has_invalid_type_throws_exception()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("AttrInteger", "2"),
                new KeyValuePair<string, string>("AttrString","123"),
                new KeyValuePair<string, string>("AttrPoint","1,2")
            };
            try
            {
                var b = binder.BindTo2<B>(pairs);
            }
            catch (InvalidMemberTypeException ex)
            {
                Assert.AreEqual(typeof(B).GetField("ThePoint"), ex.MemberInfo);
                Assert.AreEqual(typeof(B).GetField("ThePoint").FieldType, ex.MemberType);
                throw ex;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(RequiredMemberException))]
        public void bind2_when_member_is_required_throws_exception()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("AttrInteger", "2")
            };
            try
            {
                var b = binder.BindTo2<B>(pairs);
            }
            catch (RequiredMemberException ex)
            {
                Assert.AreEqual("AString", ex.MemberName);
                throw ex;
            }
        }

        [TestMethod]
        public void bind2_convert_point()
        {
            var binder = new Binder();
            binder.WithConverterFor<Point>(new PointConverter());
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("AttrInteger", "2"),
                new KeyValuePair<string, string>("AttrString","123"),
                new KeyValuePair<string, string>("AttrPoint","1,2")
            };
            
            var b = binder.BindTo2<B>(pairs);
            Assert.AreEqual(2, b.AnInteger);
            Assert.AreEqual("123", b.AString);
            Assert.AreEqual(1, b.ThePoint.X);
            Assert.AreEqual(2, b.ThePoint.Y);
        }
    }
}


/*
class PointBindingConverter : IBindingConverter { ... }

Binder b = new Binder;

binder.WithConverterFor<Point>(new PointBindingConverter())
binder.BindTo(...)

*/