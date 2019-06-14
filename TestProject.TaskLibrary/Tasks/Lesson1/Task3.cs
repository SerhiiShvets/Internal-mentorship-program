using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;
using TestProject.TaskLibrary.Tasks.Lesson1.BL;

namespace TestProject.TaskLibrary.Tasks.Lesson1
{
    class Task3 : IRunnable
    {
        
        public void Run()
        {
            Client client1 = new Client(100, "Peter");

            Client client2 = new Client(200, "Berci");

            Kitchen kitchen = new Kitchen();
            Waitress waitressRobot = new Waitress(kitchen);

            Order order1 = new Order("CHIPS");
            //???For some reason this implementation of IEnumerable doesnt work
            //order1.ExtrasForAdding = new IEnumerable<string> { "MUSTARD" };
            order1.ExtrasForAdding = new List<string> { "MUSTARD" };
            //to do
            // implement event
            Order order2 = new Order("HOTDOG");
            order2.ExtrasForAdding = new List<string> { "KETCHUP" };


            waitressRobot.TakeOrder(client1, order1);
            waitressRobot.TakeOrder(client2, order2);
            waitressRobot.ServeOrders();

            kitchen.Cook(waitressRobot.orders.Dequeue());
            

        }
    }
}
