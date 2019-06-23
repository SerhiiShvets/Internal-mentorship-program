using System;
using System.Collections;
using System.Collections.Generic;
using Lesson1Task3ToCoverWithUnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lesson1Task3Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Waitress_TakeOrder()
        {
            //arrange
            Kitchen kitchen = new Kitchen();
            Waitress waitress = new Waitress(kitchen);
            Client client = new Client(120, "Andrew");
            Order order = new Order("HOTDOG", new List<string> { "KETCHUP"});

            //act
            //assert
        }
    }
}
