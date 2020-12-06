using System;
using System.Collections.Generic;
using System.Text;

namespace Crews.UX
{
    /// <summary>
    /// A class used as a "string enumerable" type for passing units of time.
    /// </summary>
    public class TimeUnit
    {
        /// <summary>
        /// Stored value of the class.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///  Built-in ToString method override.
        /// </summary>
        /// <returns>Returns a string representation of the class.</returns>
        public override string ToString() => Value;

        /// <summary>
        /// Type cast override for strings.
        /// </summary>
        /// <param name="unit">The TimeUnit object to use in the override.</param>
        public static implicit operator string(TimeUnit unit) => unit.Value;

        private TimeUnit(string value) => Value = value; 

        /// <summary>
        /// TimeUnit object representing a year.
        /// </summary>
        public static TimeUnit Year { get { return new TimeUnit("year"); } }

        /// <summary>
        /// TimeUnit object representing a month.
        /// </summary>
        public static TimeUnit Month { get { return new TimeUnit("month"); } }

        /// <summary>
        /// TimeUnit object representing a week.
        /// </summary>
        public static TimeUnit Week { get { return new TimeUnit("week"); } }

        /// <summary>
        /// TimeUnit object representing a day.
        /// </summary>
        public static TimeUnit Day { get { return new TimeUnit("day"); } }

        /// <summary>
        /// TimeUnit object representing an hour.
        /// </summary>
        public static TimeUnit Hour { get { return new TimeUnit("hour"); } }

        /// <summary>
        /// TimeUnit object representing a minute.
        /// </summary>
        public static TimeUnit Minute { get { return new TimeUnit("minute"); } }

        /// <summary>
        /// TimeUnit object representing a second.
        /// </summary>
        public static TimeUnit Second { get { return new TimeUnit("second"); } }

    }
}
