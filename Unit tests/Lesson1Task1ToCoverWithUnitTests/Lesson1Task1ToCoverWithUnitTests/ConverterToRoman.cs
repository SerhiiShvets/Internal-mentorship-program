using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task1ToCoverWithUnitTests
{
    public class ConverterToRoman
    {
        public string ConvertToRoman(int numberToConvert)
        {
            var thousands = new List<string> { "", "M", "MM", "MMM" };
            var hundreds = new List<string> { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            var tens = new List<string> { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            var units = new List<string> { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

            var listsWithRomanValues = new List<List<string>>();
            listsWithRomanValues.Add(units);
            listsWithRomanValues.Add(tens);
            listsWithRomanValues.Add(hundreds);
            listsWithRomanValues.Add(thousands);

            int counter = 0;
            string convertedDigit = "";
            string numberToConvertString = numberToConvert.ToString();
            List<string> valuesOfDigits;

            for (int i = numberToConvertString.Length - 1; i >= 0; i--)
            {
                valuesOfDigits = listsWithRomanValues[counter];

                for (int j = 0; j < valuesOfDigits.Count; j++)
                {
                    string tempString = Convert.ToString(numberToConvertString[i]);
                    int tempInt = Convert.ToInt32(tempString);

                    if (tempInt == j)
                    {
                        convertedDigit = valuesOfDigits[j] + convertedDigit;
                    }
                }
                counter++;
            }
            return convertedDigit;
        }
    }
}
