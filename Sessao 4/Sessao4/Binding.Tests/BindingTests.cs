using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Binding.Tests
{
    [TestFixture]
    class BindingTests
    {
        class A
        {
            public int AnInteger { get; set; }
            public string AString { get; set; }
        }

        [Test]
        public void can_bind_to_A()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string,string> ("AnInteger", "2"),
                new KeyValuePair<string, string>("AString","123")
            };
            var a = binder.BindTo<A>(pairs);

            Assert.AreEqual(2, a.AnInteger);
            Assert.AreEqual("123", a.AString);
        }
    }
}
