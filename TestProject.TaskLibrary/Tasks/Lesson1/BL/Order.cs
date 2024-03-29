﻿using System;
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


        //While using EventHandler<FoodReadyEventArgs> event, as required,  we dont need to use this delegate
        //public delegate void EventHandler<FoodReadyEventArgs> (object sender, FoodReadyEventArgs e);

        public event EventHandler<FoodReadyEventArgs> FoodReady;

        public virtual void OnFoodReady(FoodReadyEventArgs e)
        {
            EventHandler<FoodReadyEventArgs> handler = FoodReady;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void NotifyReady(IFood food)
        {
            Console.WriteLine($"Food prepared, food: " + food.ToString());
            Console.WriteLine($"Notifying observers of Order: [food={FoodToOrder}, extras=" + string.Join(" ", ExtrasForAdding)+"]");
            OnFoodReady(new FoodReadyEventArgs(food));
        }
        public Order(string food, IEnumerable<string> extras)
        {
            ExtrasForAdding = extras;
            FoodToOrder = food;
        }

    }
}
