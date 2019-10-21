using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnderstandingComputation
{
    [TestFixture]
    public static class Section612Test
    {
        [Test]
        public static void Section6120Test1()
        {
            // (1..100).each do |n|
            //   if (n % 15).zero?
            //     puts 'FizzBuzz'
            //   elsif (n % 3).zero?
            //     puts 'Fizz'
            //   elsif (n % 5).zero?
            //     puts 'Buzz'
            //   else
            //     puts n.to_s
            //   end
            // end
            for (var n = 1; n <= 100; n++)
            {
                if ((n % 15) == 0)
                    Trace.WriteLine("FizzBuzz");
                else if ((n % 3) == 0)
                    Trace.WriteLine("Fizz");
                else if ((n % 5) == 0)
                    Trace.WriteLine("Buzz");
                else
                    Trace.WriteLine(n.ToString());
            }
        }

        [Test]
        public static void Section6120Test2()
        {
            // (1..100).map do |n|
            //   if (n % 15).zero?
            //     'FizzBuzz'
            //   elsif (n % 3).zero?
            //     'Fizz'
            //   elsif (n % 5).zero?
            //     'Buzz'
            //   else
            //     n.to_s
            //   end
            // end
            var r = Enumerable.Range(1, 100).Select(n =>
                {
                    if ((n % 15) == 0)
                        return "FizzBuzz";
                    else if ((n % 3) == 0)
                        return "Fizz";
                    else if ((n % 5) == 0)
                        return "Buzz";
                    else
                        return n.ToString();
                }).
                ToArray();
        }
    }
}
