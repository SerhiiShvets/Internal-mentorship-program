using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Kitchen
    {
        public IFood mainFood;
        public IFood extrasToAdd;

        private Dictionary<string, Func<IFood, IFood>> ExtrasCooker = new Dictionary<string, Func<IFood, IFood>>
        {
            { "MUSTARD", food => new Mustard(food) },
            { "KETCHUP", food => new Ketchup(food) }
        };

        private Dictionary<string, Func<IFood>> MainFoodCooker = new Dictionary<string, Func<IFood>>
        {
            { "HOTDOG", () => new HotDog() },
            { "CHIPS", () => new Chips() }
        };

        public IFood CreateMainFood(string food)
        {
            var result = MainFoodCooker[food]();

            return result;
        }

        public IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            var result = mainFood;
            foreach (var extra in extras)
            {
                result = ExtrasCooker[extra](result);
            }
            return result;
        }

        public IFood Cook(Order order)
        {
            Console.WriteLine($"Preparing food, order: Order[food={order.FoodToOrder}, extras=["+string.Join(" ", order.ExtrasForAdding)+"]]");
            mainFood = CreateMainFood(order.FoodToOrder);
            extrasToAdd = AddExtras(mainFood, order.ExtrasForAdding);
            Console.WriteLine($"Food prepared, food: "+string.Join(" ", extrasToAdd)+$"[{mainFood}[]]");
            return extrasToAdd;
        }
    }
}
