using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    class Ketchup : Extra
    {

        public override double CalculateHappiness(double happiness)
        {
            return happiness + Effect;
        }

        public Ketchup(Food food) : base(food)
        {
            Effect = food.Effect * 2;
            MainFood = food;
        }
    }
}
