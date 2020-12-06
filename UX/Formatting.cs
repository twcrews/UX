using System;
using System.Collections.Generic;

namespace Crews.UX
{  
    /// <summary>
    /// Provides a collection of static functions for formatting strings in a UX/UI.
    /// </summary>
    public static class Formatting
    {
        private static readonly Dictionary<int, string> TextNumbers = new Dictionary<int, string>()
        {
            {90, "ninety"}, {80, "eighty"}, {70, "seventy"}, {60, "sixty"}, {50, "fifty"}, 
            {40, "forty"}, {30, "thirty"}, {20, "twenty"}, {1, "one"}, {2, "two"}, {3, "three"}, 
            {4, "four"}, {5, "five"}, {6, "six"}, {7, "seven"}, {8, "eight"}, {9, "nine"}, 
            {10, "ten"}, {11, "eleven"}, {12, "twelve"}, {13, "thirteen"}, {14, "fourteen"}, 
            {15, "fifteen"}, {16, "sixteen"}, {17, "seventeen"}, {18, "eighteen"}, {19, "nineteen"}
        };

        private static readonly Dictionary<string, int> PrecisionMap = 
            new Dictionary<string, int>()
        {
            {TimeUnit.Year, 0}, {TimeUnit.Month, 1}, {TimeUnit.Week, 2}, {TimeUnit.Day, 3 }, 
                {TimeUnit.Hour, 4}, {TimeUnit.Minute, 5}, {TimeUnit.Second, 6}
        };

        /// <summary>
        /// Formats an integer between 1 and 99 (inclusive) as text. 
        /// Other integers return a ToString of themselves.
        /// </summary>
        /// <param name="number">The integer to format.</param>
        /// <returns>Returns a string representing the number.</returns>
        public static string NumberAsText(int number)
        {
            if (number < 1 || number > 99)
            {
                return number.ToString();
            }


            if (TextNumbers.TryGetValue(number, out string returnString))
            {
                return returnString;
            }

            foreach (KeyValuePair<int, string> pair in TextNumbers)
            {
                if (number < pair.Key)
                {
                    continue;
                }

                return pair.Value + "-" + TextNumbers[number - pair.Key];
            }

            return number.ToString();
        }

        #region "FormatQuantity"

        /// <summary>
        /// Formats a double with a given unit.
        /// </summary>
        /// <param name="quantity">The value of the quantity.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <param name="textQuantity">
        /// Indicates whether the quantity number should be converted to text.
        /// </param>
        /// <returns>
        /// Returns a string representing the formatted quantity and unit of measurement.
        /// </returns>
        public static string FormatQuantity(double quantity, string unit, bool textQuantity)
        {
            string prefix = string.Empty;
            string quantityStr;
            string unitStr;

            if (textQuantity)
            {
                if (quantity < 1)
                {
                    prefix = "less than";
                }
                else
                {
                    // Checks if number is fractional
                    if (!(Math.Abs(quantity % 1) <= (double.Epsilon * 100)))
                    {
                        prefix = "about";
                    }
                }
                quantityStr = NumberAsText((int)Math.Ceiling(quantity));
            }
            else
            {
                quantityStr = quantity.ToString();
            }


            if (quantity != 1)
            {
                if (unit.EndsWith("y"))
                {
                    unitStr = unit.TrimEnd('y') + "ies";
                }
                else
                {
                    unitStr = unit + "s";
                }
            }
            else
            {
                unitStr = unit;
            }

            return string.Format("{0} {1} {2}", prefix, quantityStr, unitStr).Trim();
        }

        /// <summary>
        /// Formats an integer with a given unit.
        /// </summary>
        /// <param name="quantity">The value of the quantity.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <param name="textQuantity">
        /// Indicates whether the quantity number should be converted to text.
        /// </param>
        /// <returns>
        /// Returns a string representing the formatted quantity and unit of measurement.
        /// </returns>
        public static string FormatQuantity(int quantity, string unit, bool textQuantity) =>
            FormatQuantity((double)quantity, unit, textQuantity);

        /// <summary>
        /// Formats a double with a given unit.
        /// </summary>
        /// <param name="quantity">The value of the quantity.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>
        /// Returns a string representing the formatted quantity and unit of measurement.
        /// </returns>
        public static string FormatQuantity(double quantity, string unit) =>
            FormatQuantity(quantity, unit, false;

        /// <summary>
        /// Formats an integer with a given unit.
        /// </summary>
        /// <param name="quantity">The value of the quantity.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>
        /// Returns a string representing the formatted quantity and unit of measurement.
        /// </returns>
        public static string FormatQuantity(int quantity, string unit) =>
            FormatQuantity((double)quantity, unit, false);

        #endregion

        #region "FormatTimeSpan"

        /// <summary>
        /// Formats a TimeSpan as text.
        /// </summary>
        /// <param name="timeSpan">The TimeSpan object to format.</param>
        /// <param name="precision">The TimeUnit of precision to use.</param>
        /// <param name="textQuantity">
        /// Indicates whether the TimeSpan values should be converted to text.
        /// </param>
        /// <returns>Returns a string representation of the TimeSpan.</returns>
        public static string FormatTimespan(
            TimeSpan timeSpan, 
            TimeUnit precision, 
            bool textQuantity)
        {
            if (timeSpan.TotalSeconds <= 0)
            {
                throw new ArgumentException("Cannot format zero or negative TimeStamps.");
            }

            string returnString = string.Empty;

            int years = 0;
            int months = 0;
            int weeks = 0;

            if (timeSpan.TotalDays >= 365)
            {
                years = timeSpan.Days / 365;
                returnString += FormatQuantity(years, TimeUnit.Year, textQuantity) + ", ";
            }
            else if (PrecisionMap[precision] == 0)
            {
                return FormatQuantity(timeSpan.TotalDays / 365, TimeUnit.Year, textQuantity);
            }

            if (timeSpan.TotalDays >= 30 && PrecisionMap[precision] >= 1)
            {
                months = timeSpan.Days / 30 - years * 12;
                returnString += months > 0 ? 
                    FormatQuantity(months, TimeUnit.Month, textQuantity) + ", " : string.Empty;
            }
            else if (PrecisionMap[precision] == 1)
            {
                return FormatQuantity(timeSpan.TotalDays / 30, TimeUnit.Month, textQuantity);
            }

            if (timeSpan.TotalDays >= 7 && PrecisionMap[precision] >= 2)
            {
                weeks = timeSpan.Days / 7 - years * 52 - months * 4;
                returnString += weeks > 0 ? 
                    FormatQuantity(weeks, TimeUnit.Week, textQuantity) + ", " : string.Empty;
            }
            else if (PrecisionMap[precision] == 2)
            {
                return FormatQuantity(timeSpan.TotalDays / 7, TimeUnit.Week, textQuantity);
            }

            if (PrecisionMap[precision] >= 3)
            {
                int days = timeSpan.Days - years * 365 - months * 30 - weeks * 7;
                returnString += days > 0 ? 
                    FormatQuantity(days, TimeUnit.Day, textQuantity) + ", " : string.Empty;
                if (timeSpan.TotalDays < 1 && PrecisionMap[precision] == 3)
                {
                    return FormatQuantity(timeSpan.TotalDays, TimeUnit.Day, textQuantity);
                }
            }

            if (PrecisionMap[precision] >= 4)
            {
                returnString += timeSpan.Hours > 0 ?
                    FormatQuantity(timeSpan.Hours, TimeUnit.Hour, textQuantity) + 
                    ", " : string.Empty;
                if (timeSpan.TotalHours < 1 && PrecisionMap[precision] == 4)
                {
                    return FormatQuantity(timeSpan.TotalHours, TimeUnit.Hour, textQuantity);
                }
            }

            if (PrecisionMap[precision] >= 5)
            {
                returnString += timeSpan.Minutes > 0 ?
                    FormatQuantity(timeSpan.Minutes, TimeUnit.Minute, textQuantity) + 
                    ", " : string.Empty;
                if (timeSpan.TotalMinutes < 1 && PrecisionMap[precision] == 5)
                {
                    return FormatQuantity(timeSpan.TotalMinutes, TimeUnit.Minute, textQuantity);
                }
            }

            if (PrecisionMap[precision] >= 6)
            {
                returnString += timeSpan.Seconds > 0 ?
                    FormatQuantity(timeSpan.Seconds, TimeUnit.Second, textQuantity) : string.Empty;
                if (timeSpan.TotalSeconds < 1 && PrecisionMap[precision] == 6)
                {
                    return FormatQuantity(timeSpan.TotalSeconds, TimeUnit.Second, textQuantity);
                }
            }

            returnString = returnString.TrimEnd(',', ' ');
            if (returnString.Contains(", "))
            {
                returnString = returnString.Remove(returnString.LastIndexOf(", "), 2)
                    .Insert(returnString.LastIndexOf(", "), " and ");
            }
            return returnString;
        }

        /// <summary>
        /// Formats a TimeSpan as text.
        /// </summary>
        /// <param name="timeSpan">The TimeSpan object to format.</param>
        /// <param name="precision">The TimeUnit of precision to use.</param>
        /// <returns>Returns a string representation of the TimeSpan.</returns>
        public static string FormatTimespan(TimeSpan timeSpan, TimeUnit precision) =>
            FormatTimespan(timeSpan, precision, false);

        /// <summary>
        /// Formats a TimeSpan as text.
        /// </summary>
        /// <param name="timeSpan">The TimeSpan object to format.</param>
        /// <returns>Returns a string representation of the TimeSpan.</returns>
        public static string FormatTimespan(TimeSpan timeSpan) =>
            FormatTimespan(timeSpan, TimeUnit.Second, false;

        #endregion

        #region "Capitalize"

        /// <summary>
        /// Capitalizes one or all words in a string.
        /// </summary>
        /// <param name="text">The string to capitalize.</param>
        /// <param name="allWords">
        /// Indicates whether all words detected should be capitalized.
        /// </param>
        /// <returns>Returns a formatted string.</returns>
        public static string Capitalize(string text, bool allWords)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("String cannot be empty or null.");
            }

            if (allWords)
            {
                string textStr = string.Empty;
                foreach (string word in text.Trim().Split(' '))
                {
                    textStr += CapitalizeString(word) + " ";
                }
                return textStr.Trim();
            }
            return CapitalizeString(text);
        }

        /// <summary>
        /// Capitalizes one or all words in a string.
        /// </summary>
        /// <param name="text">The string to capitalize.</param>
        /// <returns>Returns a formatted string.</returns>
        public static string Capitalize(string text) => Capitalize(text, true);

        private static string CapitalizeString(string text) => 
            char.ToUpper(text[0]).ToString() + (text.Length > 1 ? 
            text.Substring(1) : string.Empty);

        #endregion
    }
}
