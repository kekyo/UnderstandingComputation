using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnderstandingComputation
{
    public static class ChurchOperator
    {
        // IS_ZERO = -> n { n[-> x { FALSE }][TRUE] }
        //public static Func<T, Func<T, T>> IS_ZERO<T>(Func<Func<T, Func<T, T>>, Func<Func<T, Func<T, T>>, Func<T, Func<T, T>>>> n) =>
        //    n(x => ChurchBoolean.FALSE(x))(ChurchBoolean.TRUE);

        public static Func<T, Func<T, T>> IS_ZERO<T>(Func<Func<T, T>, Func<T, T>> n) =>
           n(x => ChurchBoolean.FALSE)(ChurchBoolean.TRUE);
    }

    [TestFixture]
    public static class Section615Test
    {
        [Test]
        public static void Section6150Test1()
        {
            var r1 = ChurchBoolean2.to_boolean(ChurchOperator.IS_ZERO<bool>(ChurchNumerals.ZERO));
            Assert.AreEqual(true, r1);

            var r2 = ChurchBoolean2.to_boolean(ChurchOperator.IS_ZERO<bool>(ChurchNumerals.ONE));
            Assert.AreEqual(true, r2);
        }
    }
}
