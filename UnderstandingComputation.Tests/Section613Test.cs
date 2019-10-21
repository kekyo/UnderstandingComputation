using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnderstandingComputation
{
    public static class ChurchNumerals<T>
    {
        // Church numerals (true lambda expressions, required generic argument)

        public static readonly Func<Func<T, T>, Func<T, T>> ZERO = p => x => x;
        public static readonly Func<Func<T, T>, Func<T, T>> ONE = p => x => p(x);
        public static readonly Func<Func<T, T>, Func<T, T>> TWO = p => x => p(p(x));
        public static readonly Func<Func<T, T>, Func<T, T>> THREE = p => x => p(p(p(x)));
        public static readonly Func<Func<T, T>, Func<T, T>> FIVE = p => x => p(p(p(p(p(x)))));
        public static readonly Func<Func<T, T>, Func<T, T>> FIFTEEN = p => x => p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(x)))))))))))))));
        public static readonly Func<Func<T, T>, Func<T, T>> HUNDRED = p => x =>
            p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(x))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));
    }

    public static class ChurchNumerals
    {
        // Church numerals (performs generic type implicitly)

        public static Func<T, T> ZERO<T>(Func<T, T> p) => x => x;
        public static Func<T, T> ONE<T>(Func<T, T> p) => x => p(x);
        public static Func<T, T> TWO<T>(Func<T, T> p) => x => p(p(x));
        public static Func<T, T> THREE<T>(Func<T, T> p) => x => p(p(p(x)));
        public static Func<T, T> FIVE<T>(Func<T, T> p) => x => p(p(p(p(p(x)))));
        public static Func<T, T> FIFTEEN<T>(Func<T, T> p) => x => p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(x)))))))))))))));
        public static Func<T, T> HUNDRED<T>(Func<T, T> p) => x =>
            p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(p(x))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));

        // def to_integer(proc)
        //   proc[=> n { n + 1 }][0]
        // end
        public static int to_integer(Func<Func<int, int>, Func<int, int>> proc) =>
            proc(n => (n + 1))(0);
    }

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

        [Test]
        public static void Section6130Test3()
        {
            // to_integer(0)
            Assert.AreEqual(0, ChurchNumerals.to_integer(ChurchNumerals<int>.ZERO));
            // to_integer(3)
            Assert.AreEqual(3, ChurchNumerals.to_integer(ChurchNumerals<int>.THREE));
            // to_integer(5)
            Assert.AreEqual(5, ChurchNumerals.to_integer(ChurchNumerals<int>.FIVE));
            // to_integer(15)
            Assert.AreEqual(15, ChurchNumerals.to_integer(ChurchNumerals<int>.FIFTEEN));
            // to_integer(100)
            Assert.AreEqual(100, ChurchNumerals.to_integer(ChurchNumerals<int>.HUNDRED));
        }

        [Test]
        public static void Section6130Test4()
        {
            // to_integer(0)
            Assert.AreEqual(0, ChurchNumerals.to_integer(ChurchNumerals.ZERO));
            // to_integer(3)
            Assert.AreEqual(3, ChurchNumerals.to_integer(ChurchNumerals.THREE));
            // to_integer(5)
            Assert.AreEqual(5, ChurchNumerals.to_integer(ChurchNumerals.FIVE));
            // to_integer(15)
            Assert.AreEqual(15, ChurchNumerals.to_integer(ChurchNumerals.FIFTEEN));
            // to_integer(100)
            Assert.AreEqual(100, ChurchNumerals.to_integer(ChurchNumerals.HUNDRED));
        }
    }
}
