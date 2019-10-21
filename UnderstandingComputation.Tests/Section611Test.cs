using NUnit.Framework;
using System;

namespace UnderstandingComputation
{
    [TestFixture]
    public static class Section611Test
    {
        [Test]
        public static void Section6111Test()
        {
            // -> x { x + 2 }.call(1)
            Func<int, int> body = x => x + 2;
            var r = body.Invoke(1);

            Assert.AreEqual(3, r);
        }

        [Test]
        public static void Section6112Test1()
        {
            // -> x, y {
            //   x+y
            // }.call(3, 4)
            Func<int, int, int> body = (x, y) =>
                x + y;
            var r = body.Invoke(3, 4);

            Assert.AreEqual(7, r);
        }

        [Test]
        public static void Section6112Test2()
        {
            // -> x {
            //   -> y {
            //     x+y
            //   }
            // }.call(3).call(4)
            Func<int, Func<int, int>> body =
                x =>
                    y =>
                        x + y;
            var r = body.Invoke(3).Invoke(4);

            Assert.AreEqual(7, r);
        }

        [Test]
        public static void Section6113Test()
        {
            // p = -> n { n * 2 }
            Func<int, int> p = n => n * 2;
            // q = -> x { p.call(x) }
            Func<int, int> q = x => p.Invoke(x);

            var rp = p.Invoke(5);
            Assert.AreEqual(10, rp);

            var rq = q.Invoke(5);
            Assert.AreEqual(10, rq);
        }

        [Test]
        public static void Section6114Test()
        {
            // -> x { x + 5 }[6]
            Func<int, int> body = x => x + 5;
            var r = body(6);

            Assert.AreEqual(11, r);
        }
    }
}
