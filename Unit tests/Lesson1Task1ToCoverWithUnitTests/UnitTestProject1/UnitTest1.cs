using System;
using Lesson1Task1ToCoverWithUnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConvertToRoman_146_CXLVIreturned()
        {
            //arrange
            int input = 146;
            string expected = "CXLVI";
            //act
            ConverterToRoman converter = new ConverterToRoman();
            string actual = converter.ConvertToRoman(input);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToRoman_1257_MCCLVIIreturned()
        {
            //arrange
            int input = 1257;
            string expected = "MCCLVII";
            //act
            ConverterToRoman converter = new ConverterToRoman();
            string actual = converter.ConvertToRoman(input);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(13, "XIII")]
        [DataRow(685, "DCLXXXV")]
        [DataRow(2346, "MMCCCXLVI")]
        [DataRow(1000, "M")]
        public void ConvertToRomanTest(int input, string expected)
        {
            //act
            ConverterToRoman converter = new ConverterToRoman();
            string actual = converter.ConvertToRoman(input);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
