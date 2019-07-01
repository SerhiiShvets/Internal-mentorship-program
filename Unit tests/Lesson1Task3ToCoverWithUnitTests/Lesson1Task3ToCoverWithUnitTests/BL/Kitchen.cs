using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Kitchen
    {
        private ILogger logger;
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

        public IFood Cook(Order order, ILogger logger)
        {
            IFood food;
            logger.Write($"Kitchen: Preparing food, order: Order[food={order.FoodToOrder}, extras=[" + string.Join(" ", order.ExtrasForAdding) + "]]");
            food = CreateMainFood(order.FoodToOrder);
            food = AddExtras(food, order.ExtrasForAdding);
            logger.Write($"Kitchen: Food prepared, food: " + food.ToString());
            order.NotifyReady(food);
            
            return food;
        }
    }
}
