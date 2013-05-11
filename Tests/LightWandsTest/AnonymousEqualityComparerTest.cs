using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Reactive.Testing;
using System.Collections.Generic;
using System.Linq;
using Kirinji.LightWands;

namespace KIRINJI.Toolkit.Test
{
    [TestClass]
    public class AnonymousEqualityComparerTest : ReactiveTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCase(new string[] { }, new string[] { }, true)]
        [TestCase(new[] { "a" }, new[] { "a" }, true)]
        [TestCase(new string[] { }, new[] { "a" }, false)]
        [TestCase(new[] { "a", "b", "c" }, new[] { "a", "c", "b" }, false)]
        public void EnumerableOfTest()
        {
            this.TestContext.Run((IEnumerable<string> l, IEnumerable<string> r, bool expected) =>
                {
                    var eq = EqualityComparer.EnumerableOf<string>();
                    eq.Equals(l, r).Is(expected);
                    if (expected == true)
                    {
                        eq.GetHashCode(l).Is(eq.GetHashCode(r));
                    }
                });
        }

        [TestMethod]
        [TestCase(new string[] { }, new string[] { }, true)]
        [TestCase(new[] { "a" }, new[] { "a" }, true)]
        [TestCase(new string[] { }, new[] { "a" }, false)]
        [TestCase(new[] { "a", "b", "c" }, new[] { "a", "c", "b" }, true)]
        public void EnumerableOfUnorderdTest()
        {
            this.TestContext.Run((IEnumerable<string> l, IEnumerable<string> r, bool expected) =>
            {
                var eq = EqualityComparer.EnumerableOfUnordered<string>();
                eq.Equals(l, r).Is(expected);
                if (expected == true)
                {
                    eq.GetHashCode(l).Is(eq.GetHashCode(r));
                }
            });
        }

        static readonly DateTime d1 = new DateTime(1, 2, 3);

        [TestMethod]
        [TestCase("a", "a", true)]
        [TestCase("ab", "ac", true)]
        [TestCase("adsgas", "fgrbrfghdfsh", false)]
        [TestCase("123456", "135791", true)]
        public void CreateTest()
        {
            this.TestContext.Run((string l, string r, bool expected) =>
            {
                var eq = EqualityComparer.Create<string>(t => t.FirstOrNull(), t => t.Count());
                eq.Equals(l, r).Is(expected);
                if (expected == true)
                {
                    eq.GetHashCode(l).Is(eq.GetHashCode(r));
                }
            });
        }

        [TestMethod]
        public void ReferenceEqualsTest()
        {
            var obj1 = new object();
            var obj2 = new object();
            var support1 = new ReferenceEqualsTestSupportClass();
            var support2 = new ReferenceEqualsTestSupportClass();
            ReferenceEqualsTestSupport<object>(obj1, obj1, true);
            ReferenceEqualsTestSupport<object>(obj1, null, false);
            ReferenceEqualsTestSupport<object>(obj1, obj2, false);
            ReferenceEqualsTestSupport<object>(support1, support1, true);
            ReferenceEqualsTestSupport<object>(support1, support2, false);
        }

        private void ReferenceEqualsTestSupport<T>(T l, T r, bool expected) where T : class
        {
            var eq = EqualityComparer.ReferenceEquals<T>();
            eq.Equals(l, r).Is(expected);;
            if (expected == true)
            {
                eq.GetHashCode(l).Is(eq.GetHashCode(r));
            }
        }

        private class ReferenceEqualsTestSupportClass
        {
            public override int GetHashCode()
            {
                return new Random().Next();
            }

            public override bool Equals(object obj)
            {
                return false;
            }
        }
    }
}
