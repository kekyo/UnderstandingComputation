using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnderstandingComputation
{
    [TestFixture]
    public static class Section613Test
    {
        [Test]
        public static void Section6130Test1()
        {
            T one<T>(Func<T, T> proc, T x) =>
                proc(x);

            T two<T>(Func<T, T> proc, T x) =>
                proc(proc(x));

            T three<T>(Func<T, T> proc, T x) =>
                proc(proc(proc(x)));

            T zero<T>(Func<T, T> prox, T x) =>
                x;
        }

        [Test]
        public static void Section6130Test2()
        {
            Func<T, T> one<T>(Func<T, T> proc) =>
                x => proc(x);

            Func<T, T> two<T>(Func<T, T> proc) =>
                x => proc(proc(x));

            Func<T, T> three<T>(Func<T, T> proc) =>
                x => proc(proc(proc(x)));

            Func<T, T> zero<T>(Func<T, T> proc) =>
                x => x;
        }

        public static class ChurchNumerals<T>
        {
            // Church numerals

            public static readonly Func<Func<T, T>, Func<T, T>> ZERO = p => x => x;
            public static readonly Func<Func<T, T>, Func<T, T>> ONE = p => x => p(x);
            public static readonly Func<Func<T, T>, Func<T, T>> TWO = p => x => p(p(x));
            public static readonly Func<Func<T, T>, Func<T, T>> THREE = p => x => p(p(p(x)));
            public static readonly Func<Func<T, T>, Func<T, T>> FIVE = p => x => p(p(p(p(p(x)))));
            public static readonly Func<Func<T, T>, Func<T, T>> FIFTEEN = p => x => p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(x)))))))))))))));
            public static readonly Func<Func<T, T>, Func<T, T>> HUNDRED = p => x =>
                p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(x))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));
        }

        [Test]
        public static void Section6130Test3()
        {
            // def to_integer(proc)
            //   proc[=> n { n + 1 }][0]
            // end
            int to_integer(Func<Func<int, int>, Func<int, int>> proc) =>
                proc(n => (n + 1))(0);

            // to_integer(0)
            Assert.AreEqual(0, to_integer(ChurchNumerals<int>.ZERO));
            // to_integer(3)
            Assert.AreEqual(3, to_integer(ChurchNumerals<int>.THREE));
            // to_integer(5)
            Assert.AreEqual(5, to_integer(ChurchNumerals<int>.FIVE));
            // to_integer(15)
            Assert.AreEqual(15, to_integer(ChurchNumerals<int>.FIFTEEN));
            // to_integer(100)
            Assert.AreEqual(100, to_integer(ChurchNumerals<int>.HUNDRED));
        }
    }
}
