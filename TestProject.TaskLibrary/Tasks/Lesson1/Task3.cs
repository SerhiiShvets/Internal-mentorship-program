﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;
using TestProject.TaskLibrary.Tasks.Lesson1.BL;

namespace TestProject.TaskLibrary.Tasks.Lesson1
{
    public class Task3 : IRunnable
    {
        
        public void Run()
        {
            Client client1 = new Client(100, "Peter");

            Client client2 = new Client(200, "Berci");

            Kitchen kitchen = new Kitchen();
            Waitress waitressRobot = new Waitress(kitchen);

            Order order1 = new Order("CHIPS", new List<string> { "MUSTARD" });

            order1.ExtrasForAdding = new List<string> { "MUSTARD" };
            //to do
            // implement event
            Order order2 = new Order("HOTDOG", new List<string> {"KETCHUP"});

            client1.Subscribe(order1);
            client2.Subscribe(order2);

            waitressRobot.TakeOrder(client1, order1);
            waitressRobot.TakeOrder(client2, order2);
            waitressRobot.ServeOrders();

            kitchen.Cook(waitressRobot.orders.Dequeue());
            kitchen.Cook(waitressRobot.orders.Dequeue());

            //order1.FoodReady += order1.NotifyReady();


        }
    }
}
