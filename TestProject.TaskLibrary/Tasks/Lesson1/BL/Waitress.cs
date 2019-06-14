using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Waitress
    {
        public Queue<Order> orders;
        public Kitchen Kitchen { get; }
        //?????Why do we take Kitchen as a parameter?
        public Waitress(Kitchen kitchen)
        {
            Kitchen = kitchen;
        }

        public void ServeOrders()
        {
            Console.WriteLine($"Processing {orders.Count} order(s)...");
        }
        public void TakeOrder(Client client, Order order)
        {
            orders.Enqueue(order);
            Console.WriteLine("Order registered, client: Client [name = " + 
                client.Name + "happiness = " + client.Happiness + 
                "], order: Order [food = " + order.FoodToOrder +", extra = "+ order.ExtrasForAdding);
        }
    }
}
