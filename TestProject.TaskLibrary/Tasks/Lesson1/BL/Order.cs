using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Order 
    {
        public IEnumerable<string> ExtrasForAdding { get; set; }

        public string FoodToOrder { get; set; }



        public delegate void FoodReady(object sender, FoodReadyEventArgs e);

        public event FoodReady FoodCooked;

        public void NotifyReady(IFood food)
        {
            if (FoodCooked != null)
            {
                FoodCooked(this, new FoodReadyEventArgs
                    ($"Notifying observers of Order[food={FoodToOrder}, extras={ExtrasForAdding}]", FoodToOrder, ExtrasForAdding));
            }
        }
        public Order(string food/*, IEnumerable<string> extras*/)
        {
            //Extras = extras;
            FoodToOrder = food;
        }

    }
}
