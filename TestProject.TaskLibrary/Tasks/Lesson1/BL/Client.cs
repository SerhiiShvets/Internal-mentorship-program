﻿using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Client
    {
        public double Happiness { get; set; }
        public string Name { get; }

        public void Subscribe(Order order)
        {
            order.FoodReady += HandleCustomEvent;
        }

        public Client(double happiness, string name)
        {
            Happiness = happiness;
            Name = name;
        }

        public void Eat(IFood food)
        {
            Console.WriteLine($"Starting to eat food, client: Client [name={Name}, happiness={Happiness}], food: {food.ToString()}]");
            Console.WriteLine("Csam csam nyam nyam");
            Happiness = food.CalculateHappiness(Happiness);
            Console.WriteLine($"Food eaten, client: Client name={Name}, happiness={Happiness}");
        }

        void HandleCustomEvent (object sender, FoodReadyEventArgs e)
        {
            Eat(e.thisFood);
        }
    }
}
