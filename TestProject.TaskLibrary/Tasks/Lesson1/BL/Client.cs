using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Client
    {
        public double Happiness { get; set; }
        public string Name { get; }

        public Client(double happiness, string name)
        {
            Happiness = happiness;
            Name = name;
        }

        public void Eat(IFood food)
        {
            //to do
            //
            food.CalculateHappiness(Happiness);
        }
    }
}
