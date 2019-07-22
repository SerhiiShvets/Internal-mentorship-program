using System;
using System.Drawing;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var image = ExtendedImage.CreateImage("pair_hiking.png");
            var charsByDarkness = new char[] { '#', '@', 'X', 'L', 'I', ':', '.', ' ' };
            int max = 0;
            int min = 255 * 3;
            int stepY = image.GetHeight() / 45;
            int stepX = image.GetWidth() / 150;
            int min2;
            int max2;

            GoThroughImageHeightAndWidth(image, stepX, stepY, min, max, out min2, out max2);

            GoThroughImageHeightAndWidth(image, stepX, stepY, min2, max2, charsByDarkness);


            // This is the original code to make shorter and more readable
            //
            //for (int y = 0; y < image.GetHeight(); y += stepY)
            //{
            //    for (int x = 0; x < image.GetWidth(); x += stepX)
            //    {
            //        int sum = 0;
            //        for (int avgy = 0; avgy < stepY; avgy++)
            //        {
            //            for (int avgx = 0; avgx < stepX; avgx++)
            //            {
            //                sum = sum + (image.GetRed(new Point(x, y)) + image.GetBlue(new Point(x, y)) + image.GetGreen(new Point(x, y)));
            //            }
            //        }
            //        sum = sum / stepY / stepX;
            //        if (max < sum)
            //        {
            //            max = sum;
            //        }
            //        if (min > sum)
            //        {
            //            min = sum;
            //        }
            //    }
            //}


            //for (int y = 0; y < image.GetHeight() - stepY; y += stepY)
            //{
            //    for (int x = 0; x < image.GetWidth() - stepX; x += stepX)
            //    {
            //        int sum = 0;
            //        for (int avgy = 0; avgy < stepY; avgy++)
            //        {
            //            for (int avgx = 0; avgx < stepX; avgx++)
            //            {
            //                sum = sum + (image.GetRed(new Point(x, y)) + image.GetBlue(new Point(x, y)) + image.GetGreen(new Point(x, y)));
            //            }
            //        }

            //        sum = sum / stepY / stepX;
            //        Console.Write(charsByDarkness[(sum - min2) * charsByDarkness.Length / (max2 - min2 + 1)]);
            //    }

            //    Console.WriteLine();
            //}

            Console.ReadLine();
        }

        public static void GoThroughImageHeightAndWidth(ExtendedImage image, int stepX, int stepY, int min, int max, out int min2, out int max2)
        {
            for (int y = 0; y < image.GetHeight(); y += stepY)
            {
                for (int x = 0; x < image.GetWidth(); x += stepX)
                {
                    int sum = 0;

                    sum = GetSum(image, stepX, stepY, x, y, min, max, sum);

                    sum = sum / stepY / stepX;
                    if (max < sum)
                    {
                        max = sum;
                    }

                    if (min > sum)
                    {
                        min = sum;
                    }
                }
            }

            min2 = min;
            max2 = max;
        }

        public static int GetSum(ExtendedImage image, int stepX, int stepY, int x, int y, int min2, int max2, int sum)
        {
            for (int avgy = 0; avgy < stepY; avgy++)
            {
                for (int avgx = 0; avgx < stepX; avgx++)
                {
                    sum = sum + (image.GetRed(new Point(x, y)) + image.GetBlue(new Point(x, y)) + image.GetGreen(new Point(x, y)));
                }
            }

            return sum;
        }

        public static void GoThroughImageHeightAndWidth(ExtendedImage image, int stepX, int stepY, int min2, int max2, char[] charsByDarkness)
        {
            for (int y = 0; y < image.GetHeight() - stepY; y += stepY)
            {
                for (int x = 0; x < image.GetWidth() - stepX; x += stepX)
                {
                    int sum = 0;

                    sum = GetSum(image, stepX, stepY, x, y, min2, max2, sum);

                    sum = sum / stepY / stepX;
                    Console.Write(charsByDarkness[(sum - min2) * charsByDarkness.Length / (max2 - min2 + 1)]);
                }

                Console.WriteLine();
            }
        }
    }
}
