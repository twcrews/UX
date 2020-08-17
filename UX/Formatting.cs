using System;
using System.Collections.Generic;

namespace Crews.UX
{
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

        /// <summary>
        /// Formats a quantity with a given unit.
        /// </summary>
        /// <param name="quantity">The value of the quantity.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <param name="textQuantity">Indicates whether the quantity number should be converted to text.</param>
        /// <returns>Returns a string representing the formatted quantity and unit of measurement.</returns>
        public static string QuantityFormat(double quantity, string unit, bool textQuantity)
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
                quantityStr = NumberAsText((int)quantity);
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


        public static string QuantityFormat(int quantity, string unit, bool textQuantity)
        {
            return QuantityFormat((double)quantity, unit, textQuantity);
        }


        public static string QuantityFormat(double quantity, string unit)
        {
            return QuantityFormat(quantity, unit, false);
        }


        public static string QuantityFormat(int quantity, string unit)
        {
            return QuantityFormat((double)quantity, unit, false);
        }
    }
}
