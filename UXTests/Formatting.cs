using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Crews.UX.Tests
{
    [TestClass]
    public class Formatting
    {
        [DataTestMethod]
        [DataRow(70000000)]
        [DataRow(20000000)]
        [DataRow(100000)]
        [DataRow(57)]
        public void FormatTimeSpan_SecondsPrecision(int value)
        {
            Console.WriteLine(UX.Formatting.FormatTimespan(new TimeSpan(0, 0, value)));
        }


        [DataTestMethod]
        [DataRow(70000000)]
        [DataRow(20000000)]
        [DataRow(100000)]
        [DataRow(57)]
        public void FormatTimeSpan_WeeksPrecision(int value)
        {
            Console.WriteLine(UX.Formatting.FormatTimespan(new TimeSpan(0, 0, value), TimeUnit.Week, true));
        }


        [DataTestMethod]
        [DataRow("this is a good test.")]
        [DataRow("This. is A REally good Test.. . .. .")]
        [DataRow("Hello")]
        [DataRow("little")]
        public void Capitalize(string value)
        {
            Console.WriteLine(UX.Formatting.Capitalize(value, false));
        }
    }
}
