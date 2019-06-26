using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Client : IEatable
    {
        public double Happiness { get; set; }
        public string Name { get; }
        public Order Order { get; set; }

        public void Subscribe(Order order)
        {
            order.FoodReady += HandleEventFoodReady;
        }

        public Client(double happiness, string name)
        {
            Happiness = happiness;
            Name = name;
        }

        public void Eat(IFood food)
        {
            Console.WriteLine($"Client: Starting to eat food, client: Client [name={Name}, happiness={Happiness}], food: {food.ToString()}]");
            Console.WriteLine("Client: Csam csam nyam nyam");
            Happiness = food.CalculateHappiness(Happiness);
            Console.WriteLine($"Client: Food eaten, client: Client name={Name}, happiness={Happiness}");
        }

        void HandleEventFoodReady(object sender, FoodReadyEventArgs e)
        {
            Eat(e.thisFood);
        }
    }
}
