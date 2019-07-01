using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Waitress
    {
        public Queue<Order> orders;
        public Kitchen Kitchen { get; }

        private ILogger _logger;

        public Waitress(Kitchen kitchen, ILogger logger)
        {
            Kitchen = kitchen;
            orders = new Queue<Order>();
            _logger = logger;
        }

        public void ServeOrders()
        {
            _logger.Write($"WaitressRobot: Processing {orders.Count} order(s)...");
            while (orders.Count > 0)
            {
                Kitchen.Cook(orders.Dequeue(), _logger);
            }
            _logger.Write("WaitressRobot: Orders processed.");
        }
        public void TakeOrder(Client client, Order order)
        {
            orders.Enqueue(order);
            string output = "WaitressRobot: Order registered, client: Client [name = " +
                client.Name + " happiness = " + client.Happiness +
                "], order: Order [food = " + order.FoodToOrder + ", extra = " + string.Join(" ", order.ExtrasForAdding);
           _logger.Write(output);
        }
    }
}
