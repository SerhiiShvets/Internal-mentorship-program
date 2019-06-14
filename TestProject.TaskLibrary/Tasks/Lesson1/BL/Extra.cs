using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public abstract class Extra : IFood
    {
        public double Effect { get; set; }
        public Food MainFood { get; set; }

        public abstract double CalculateHappiness(double happiness);

        public Extra(IFood food)
        {
            MainFood = food as Food;
        }
    }
}
