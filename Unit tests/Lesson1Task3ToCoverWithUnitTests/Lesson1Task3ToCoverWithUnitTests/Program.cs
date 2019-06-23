using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client1 = new Client(100, "Peter");
            Client client2 = new Client(200, "Berci");

            Kitchen kitchen = new Kitchen();
            Waitress waitressRobot = new Waitress(kitchen);

            Order order1 = new Order("CHIPS", new List<string> { "MUSTARD" });
            Order order2 = new Order("HOTDOG", new List<string> { "KETCHUP" });

            client1.Subscribe(order1);
            client2.Subscribe(order2);

            waitressRobot.TakeOrder(client1, order1);
            waitressRobot.TakeOrder(client2, order2);
            waitressRobot.ServeOrders();

            kitchen.Cook(waitressRobot.orders.Dequeue());
            kitchen.Cook(waitressRobot.orders.Dequeue());

            Console.ReadKey();
        }
    }
}
