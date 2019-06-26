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

            client1.Order = new Order("CHIPS", new List<string> { "MUSTARD" });
            client2.Order = new Order("HOTDOG", new List<string> { "KETCHUP" });

            client1.Subscribe(client1.Order);
            client2.Subscribe(client2.Order);

            Dictionary<int, Client> dictOfClients = new Dictionary<int, Client>();
            dictOfClients.Add(1, client1);
            dictOfClients.Add(2, client2);

            Kitchen kitchen = new Kitchen();
            Waitress waitressRobot = new Waitress(kitchen);

            //Order order1 = new Order("CHIPS", new List<string> { "MUSTARD" });
            //Order order2 = new Order("HOTDOG", new List<string> { "KETCHUP" });
            foreach(Client client in dictOfClients.Values)
            {
                waitressRobot.TakeOrder(client, client.Order);
            }
           

            //waitressRobot.TakeOrder(client1, client1.Order);
            //waitressRobot.TakeOrder(client2, client2.Order);
            while (waitressRobot.orders.Count > 0)
            {
                waitressRobot.ServeOrders();
            }
            //kitchen.Cook(waitressRobot.orders.Dequeue());
            //kitchen.Cook(waitressRobot.orders.Dequeue());

            Console.ReadKey();
        }
    }
}
