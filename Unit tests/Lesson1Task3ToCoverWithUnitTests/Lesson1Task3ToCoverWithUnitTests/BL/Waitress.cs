using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Waitress : IWaitressMethods
    {
        public Queue<Order> orders;
        public Kitchen Kitchen { get; }

        public Waitress(Kitchen kitchen)
        {
            Kitchen = kitchen;
            orders = new Queue<Order>();
        }

        public void ServeOrders()
        {
            Console.WriteLine($"WaitressRobot: Processing {orders.Count} order(s)...");
            while (orders.Count > 0)
            {
                Kitchen.Cook(orders.Dequeue());
            }
            Console.WriteLine("WaitressRobot: Orders processed.");
        }
        public void TakeOrder(Client client, Order order)
        {
            orders.Enqueue(order);
            string output = "WaitressRobot: Order registered, client: Client [name = " +
                client.Name + " happiness = " + client.Happiness +
                "], order: Order [food = " + order.FoodToOrder + ", extra = " + string.Join(" ", order.ExtrasForAdding);
            Console.WriteLine(output);
        }
    }
}
