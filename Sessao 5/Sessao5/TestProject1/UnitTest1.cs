using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sessao5.Tests
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
        public void TestMethod1()
        {
            var l = new List<int> { 1, 2, 1, 3, 5, 2 };
            var l2 = l.RemoveRepeated();

            Assert.AreEqual(4, l2.Count());
        }

        public struct Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime PublishDate { get; set; }
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<Book> books = new List<Book> {
                new Book {Title = "Ensaio sobre a cegueira", Author = "Saramago",
                    PublishDate = new DateTime(2005, 12, 3) },
                new Book {Title = "Memorial do Convento", Author = "Saramago",
                    PublishDate = new DateTime(1984, 12, 3) },
                new Book {Title = "Lusiadas", Author = "Camoes",
                    PublishDate = new DateTime(1600, 12, 3) }
            };

            var orderedBooks = books.OrderBy(b => b.Author).ThenBy(b => b.Title);
            var list = orderedBooks.ToList();

            Assert.AreEqual("Camoes", list[0].Author);
            Assert.AreEqual("Ensaio sobre a cegueira", list[1].Title);
        }
    }
}
