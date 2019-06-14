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

        //I am not sure this is a good way to implement AddExtras(), Cook() and CreateMainFood()
        public IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            if(extras.ToString() == "MUSTARD")
            {
                return new Mustard((Food)mainFood);
            }
            if(extras.ToString() == "KETCHUP")
            {
                return new Ketchup((Food)mainFood);
            }
            return null;
        }
        public IFood Cook(Order order)
        {
            Console.WriteLine($"Preparing food, order: Order[food={order.FoodToOrder}, extras=[{order.ExtrasForAdding}]]");
            mainFood = CreateMainFood(order.FoodToOrder);
            extrasToAdd = AddExtras(mainFood, order.ExtrasForAdding);
            Console.WriteLine($"Food prepared, food: {extrasToAdd}[{mainFood}[]]");
            return extrasToAdd;
        }
        public IFood CreateMainFood(string food)
        {
            if(food == "HOTDOG")
            {
                return new HotDog();
            }
            else
            {
                return new Chips();
            }
        }

    }
}
