using System;
using System.Collections;
using System.Collections.Generic;
using Lesson1Task3ToCoverWithUnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Lesson1Task3Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Waitress_TakeOrder()
        {
            //arrange
            IWaitressMethods mockWaitressMethods = MockRepository.GenerateMock<IWaitressMethods>();
            Kitchen kitchen = new Kitchen();
            Waitress waitress = new Waitress(kitchen);
            waitress.orders = new Queue<Order>();
            Client client = new Client(120, "Andrew");
            Order order = new Order("HOTDOG", new List<string> { "KETCHUP"});

            mockWaitressMethods.Expect(x => x.TakeOrder(client, order));

            //act
            mockWaitressMethods.Replay();

            //assert
            Assert.IsTrue(waitress.orders.Count == 1);
            Assert.IsTrue(waitress.orders.Dequeue() == order);
            mockWaitressMethods.VerifyAllExpectations();
        }
    }
}
