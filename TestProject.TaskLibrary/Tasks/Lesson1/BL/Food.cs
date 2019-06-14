using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public abstract class Food : IFood
    {
        public double Effect { get; set; }

        public abstract double CalculateHappiness(double happiness);

        public Food()
        {
            
        }
    }
}
