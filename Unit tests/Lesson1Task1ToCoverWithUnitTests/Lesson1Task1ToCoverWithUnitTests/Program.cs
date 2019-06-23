using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task1ToCoverWithUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ConverterToRoman converter = new ConverterToRoman();


            Console.WriteLine("Input a number to convert");
            string arabicNumberToConvert = Console.ReadLine();
            string convertedToRoman;
            int checkedNumberToConvert;

            if (Int32.TryParse(arabicNumberToConvert, out checkedNumberToConvert))
            {
                if (0 < checkedNumberToConvert && checkedNumberToConvert < 4000)
                {
                    convertedToRoman = converter.ConvertToRoman(checkedNumberToConvert);
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

        
    }
}
