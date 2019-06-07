using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1
{
    public class Task1 : IRunnable
    {
        public void Run()
        {     
            var thousands = new List<string> { "M" };
            var hundreds = new List<string> { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            var tens = new List<string> { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            var units = new List<string> { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

            var listsWithRomanValues = new List<List<string>>();
            listsWithRomanValues.Add(units);
            listsWithRomanValues.Add(tens);
            listsWithRomanValues.Add(hundreds);
            listsWithRomanValues.Add(thousands);

            Console.WriteLine("Input a number to convert");
            string arabicNumberToConvert = Console.ReadLine();
            string convertedToRoman;
            int checkedNumberToConvert;

            if (Int32.TryParse(arabicNumberToConvert, out checkedNumberToConvert))
            {
                if (0 < checkedNumberToConvert && checkedNumberToConvert < 4000)
                {
                    convertedToRoman = ConvertToRoman(checkedNumberToConvert, listsWithRomanValues);
                    Console.WriteLine(convertedToRoman);
                }
                else
                {
                    Console.WriteLine("Input is incorrect");
                }
            }
            else
            {
                Console.WriteLine("Input is not an integer number");
            }

            Console.ReadKey();
        }

        public static string ConvertToRoman(int numberToConvert, List<List<string>> listsOfValuesForRoman)
        {

            int counter = 0;
            string convertedDigit = "";
            string numberToConvertString = numberToConvert.ToString();
            List<string> valuesOfDigits;

            for (int i = numberToConvertString.Length - 1; i >= 0; i--)
            {
                valuesOfDigits = listsOfValuesForRoman[counter];

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
