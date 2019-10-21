using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnderstandingComputation
{
    [TestFixture]
    public static class Section614Test
    {
        [Test]
        public static void Section6140Test1()
        {
            var success1 = true;
            Assert.AreEqual(true, success1);

            var r1 = success1 ? "happy" : "sad";
            Assert.AreEqual(r1, "happy");

            var success2 = false;
            Assert.AreEqual(false, success2);

            var r2 = success2 ? "happy" : "sad";
            Assert.AreEqual(r2, "sad");
        }

        [Test]
        public static void Section6140Test2()
        {
            // def true(x, y)
            //   x
            // end
            T _true<T>(T x, T y) =>
                x;

            // def false(x, y)
            //   y
            // end
            T _false<T>(T x, T y) =>
                y;

            Func<string, string, string> success1 = _true;

            var r1 = success1("happy", "sad");
            Assert.AreEqual("happy", r1);

            Func<string, string, string> success2 = _false;

            var r2 = success2("happy", "sad");
            Assert.AreEqual("sad", r2);
        }

        public static class ChurchBoolean<T>
        {
            // TRUE  = -> x { -> y { x } }
            // FALSE = -> x { -> y { y } }
            public static readonly Func<T, Func<T, T>> TRUE = x => y => x;
            public static readonly Func<T, Func<T, T>> FALSE = x => y => y;
        }

        [Test]
        public static void Section6140Test3()
        {
            // def to_boolean(proc)
            //   proc[true][false]
            // end
            bool to_boolean(Func<bool, Func<bool, bool>> proc) =>
                proc(true)(false);

            // to_boolean(TRUE)
            var r1 = to_boolean(ChurchBoolean<bool>.TRUE);
            Assert.AreEqual(true, r1);

            // to_boolean(FALSE)
            var r2 = to_boolean(ChurchBoolean<bool>.FALSE);
            Assert.AreEqual(false, r2);
        }

        [Test]
        public static void Section6140Test4()
        {
            // def if (proc, x, y)
            //   proc[x][y]
            // end
            T _if<T>(Func<T, Func<T, T>> proc, T x, T y) =>
                proc(x)(y);

            // IF[TRUE]['happy']['sad']
            var r1 = _if(ChurchBoolean<string>.TRUE, "happy", "sad");
            Assert.AreEqual("happy", r1);

            // IF[FALSE]['happy']['sad']
            var r2 = _if(ChurchBoolean<string>.FALSE, "happy", "sad");
            Assert.AreEqual("sad", r2);
        }

        public static class ChurchBooleanIf<T>
        {
            // IF =
            //   -> b {
            //     -> x {
            //       -> y {
            //         b[x][y]
            //       }
            //     }
            //   }
            public static readonly Func<Func<T, Func<T, T>>, Func<T, Func<T, T>>> IF =
                b => x => y => b(x)(y);
        }

        [Test]
        public static void Section6140Test5()
        {
            // IF[TRUE]['happy']['sad']
            var r1 = ChurchBooleanIf<string>.IF(ChurchBoolean<string>.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r1);

            // IF[FALSE]['happy']['sad']
            var r2 = ChurchBooleanIf<string>.IF(ChurchBoolean<string>.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r2);
        }

        [Test]
        public static void Section6140Test6()
        {
            // def to_boolean(proc)
            //   IF[proc][true][false]
            // end
            bool to_boolean(Func<bool, Func<bool, bool>> proc) =>
                proc(true)(false);

            var r1 = to_boolean(ChurchBoolean<bool>.TRUE);
            Assert.AreEqual(true, r1);

            var r2 = to_boolean(ChurchBoolean<bool>.FALSE);
            Assert.AreEqual(false, r2);
        }

        public static class ChurchBooleanIf2<T>
        {
            // IF =
            //   -> b {
            //     -> x {
            //       b[x]
            //     }
            //   }
            public static readonly Func<Func<T, Func<T, T>>, Func<T, Func<T, T>>> IF1 =
                b => x => b(x);

            // IF = -> b { b }
            public static readonly Func<Func<T, Func<T, T>>, Func<T, Func<T, T>>> IF2 =
                b => b;
        }

        [Test]
        public static void Section6140Test7()
        {
            // IF[TRUE]['happy']['sad']
            var r11 = ChurchBooleanIf2<string>.IF1(ChurchBoolean<string>.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r11);

            // IF[FALSE]['happy']['sad']
            var r12 = ChurchBooleanIf2<string>.IF1(ChurchBoolean<string>.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r12);

            // IF[TRUE]['happy']['sad']
            var r21 = ChurchBooleanIf2<string>.IF2(ChurchBoolean<string>.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r21);

            // IF[FALSE]['happy']['sad']
            var r22 = ChurchBooleanIf2<string>.IF2(ChurchBoolean<string>.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r22);
        }
    }
}
