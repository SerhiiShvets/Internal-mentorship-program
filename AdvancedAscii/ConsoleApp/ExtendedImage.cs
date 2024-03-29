﻿using System;
using System.Drawing;
using System.IO;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    public class ExtendedImage
    {
        // 255
        private const int LastByte = 0xFF;
        private const int Byte = 8;
        private const int TwoBytes = 16;
        private readonly Bitmap image;

        public static ExtendedImage CreateImage(string fileName)
        {
            return new ExtendedImage(fileName);
        }

        private ExtendedImage(string fileName)
        {
            this.image = this.LoadImageFromFile(fileName);
        }

        public int GetWidth()
        {
            return this.image.Width;
        }

        public int GetHeight()
        {
            return this.image.Height;
        }

        public int GetIntensity(Point point)
        {
            return this.GetRed(point) + this.GetBlue(point) + this.GetGreen(point);
        }

        public int GetGreen(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return (rgbValue >> Byte) & LastByte;
        }

        public int GetBlue(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return rgbValue & LastByte;
        }

        public int GetRed(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return (rgbValue >> TwoBytes) & LastByte;
        }

        private int GetRgbValue(Point point)
        {
            if (point.X < 0 || point.X >= this.image.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(point));
            }
            else if (point.Y < 0 || point.Y >= this.image.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(point));
            }
            else
            {
                return this.image.GetPixel(point.X, point.Y).ToArgb();
            }
        }

        private Bitmap LoadImageFromFile(string fileName, bool checkFileExists = true)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"File not found: {fileName}", nameof(fileName));
            }

            using (var img = System.Drawing.Image.FromFile(fileName))
            {
                return new Bitmap(img);
            }
        }
    }
}