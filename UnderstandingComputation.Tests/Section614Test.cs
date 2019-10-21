using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnderstandingComputation
{
    public static class ChurchBoolean<T>
    {
        // Church boolean (true lambda expressions, required generic argument)

        // TRUE  = -> x { -> y { x } }
        // FALSE = -> x { -> y { y } }
        public static readonly Func<T, Func<T, T>> TRUE = x => y => x;
        public static readonly Func<T, Func<T, T>> FALSE = x => y => y;
    }

    public static class ChurchBoolean
    {
        // Church numerals (performs generic type implicitly)

        // TRUE  = -> x { -> y { x } }
        // FALSE = -> x { -> y { y } }
        public static Func<T, T> TRUE<T>(T x) => y => x;
        public static Func<T, T> FALSE<T>(T x) => y => y;

        // def to_boolean(proc)
        //   proc[true][false]
        // end
        public static bool to_boolean(Func<bool, Func<bool, bool>> proc) =>
            proc(true)(false);
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

    public static class ChurchBooleanIf
    {
        // IF =
        //   -> b {
        //     -> x {
        //       -> y {
        //         b[x][y]
        //       }
        //     }
        //   }
        public static Func<T, Func<T, T>> IF<T>(Func<T, Func<T, T>> b) =>
            x => y => b(x)(y);
    }

    public static class ChurchBoolean2
    {
        // def to_boolean(proc)
        //   IF[proc][true][false]
        // end
        public static bool to_boolean(Func<bool, Func<bool, bool>> proc) =>
            ChurchBooleanIf.IF(proc)(true)(false);
    }

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

        [Test]
        public static void Section6140Test31()
        {
            // to_boolean(TRUE)
            var r1 = ChurchBoolean.to_boolean(ChurchBoolean<bool>.TRUE);
            Assert.AreEqual(true, r1);

            // to_boolean(FALSE)
            var r2 = ChurchBoolean.to_boolean(ChurchBoolean<bool>.FALSE);
            Assert.AreEqual(false, r2);
        }

        [Test]
        public static void Section6140Test32()
        {
            // to_boolean(TRUE)
            var r1 = ChurchBoolean.to_boolean(ChurchBoolean.TRUE);
            Assert.AreEqual(true, r1);

            // to_boolean(FALSE)
            var r2 = ChurchBoolean.to_boolean(ChurchBoolean.FALSE);
            Assert.AreEqual(false, r2);
        }

        [Test]
        public static void Section6140Test41()
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

        [Test]
        public static void Section6140Test42()
        {
            // def if (proc, x, y)
            //   proc[x][y]
            // end
            T _if<T>(Func<T, Func<T, T>> proc, T x, T y) =>
                proc(x)(y);

            // IF[TRUE]['happy']['sad']
            var r1 = _if(ChurchBoolean.TRUE, "happy", "sad");
            Assert.AreEqual("happy", r1);

            // IF[FALSE]['happy']['sad']
            var r2 = _if(ChurchBoolean.FALSE, "happy", "sad");
            Assert.AreEqual("sad", r2);
        }

        [Test]
        public static void Section6140Test51()
        {
            // IF[TRUE]['happy']['sad']
            var r1 = ChurchBooleanIf<string>.IF(ChurchBoolean<string>.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r1);

            // IF[FALSE]['happy']['sad']
            var r2 = ChurchBooleanIf<string>.IF(ChurchBoolean<string>.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r2);
        }

        [Test]
        public static void Section6140Test52()
        {
            // IF[TRUE]['happy']['sad']
            var r1 = ChurchBooleanIf<string>.IF(ChurchBoolean.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r1);

            // IF[FALSE]['happy']['sad']
            var r2 = ChurchBooleanIf<string>.IF(ChurchBoolean.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r2);
        }

        [Test]
        public static void Section6140Test61()
        {
            var r1 = ChurchBoolean2.to_boolean(ChurchBoolean<bool>.TRUE);
            Assert.AreEqual(true, r1);

            var r2 = ChurchBoolean2.to_boolean(ChurchBoolean<bool>.FALSE);
            Assert.AreEqual(false, r2);
        }

        [Test]
        public static void Section6140Test62()
        {
            var r1 = ChurchBoolean2.to_boolean(ChurchBoolean.TRUE);
            Assert.AreEqual(true, r1);

            var r2 = ChurchBoolean2.to_boolean(ChurchBoolean.FALSE);
            Assert.AreEqual(false, r2);
        }

        [Test]
        public static void Section6140Test71()
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

        [Test]
        public static void Section6140Test72()
        {
            // IF[TRUE]['happy']['sad']
            var r11 = ChurchBooleanIf2<string>.IF1(ChurchBoolean.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r11);

            // IF[FALSE]['happy']['sad']
            var r12 = ChurchBooleanIf2<string>.IF1(ChurchBoolean.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r12);

            // IF[TRUE]['happy']['sad']
            var r21 = ChurchBooleanIf2<string>.IF2(ChurchBoolean.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r21);

            // IF[FALSE]['happy']['sad']
            var r22 = ChurchBooleanIf2<string>.IF2(ChurchBoolean.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r22);
        }

        [Test]
        public static void Section6140Test73()
        {
            // IF[TRUE]['happy']['sad']
            var r11 = ChurchBooleanIf.IF<string>(ChurchBoolean.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r11);

            // IF[FALSE]['happy']['sad']
            var r12 = ChurchBooleanIf.IF<string>(ChurchBoolean.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r12);

            // IF[TRUE]['happy']['sad']
            var r21 = ChurchBooleanIf.IF<string>(ChurchBoolean.TRUE)("happy")("sad");
            Assert.AreEqual("happy", r21);

            // IF[FALSE]['happy']['sad']
            var r22 = ChurchBooleanIf.IF<string>(ChurchBoolean.FALSE)("happy")("sad");
            Assert.AreEqual("sad", r22);
        }
    }
}
