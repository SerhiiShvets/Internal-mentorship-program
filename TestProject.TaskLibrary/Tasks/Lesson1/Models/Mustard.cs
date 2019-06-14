using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    class Mustard : Extra
    {
        public override double CalculateHappiness(double happiness)
        {
            return happiness + Effect;
        }

        public Mustard(Food food) : base (food)
        {
            Effect = 1;
            food.Effect *= 0;
            MainFood = food;
        }
    }
}
