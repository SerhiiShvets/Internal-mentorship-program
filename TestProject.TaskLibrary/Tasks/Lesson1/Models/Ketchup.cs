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
            Effect = MainFood.Effect * 2;
            return happiness + Effect;
        }

        public Ketchup(IFood food) : base(food)
        {
            MainFood = (Food)food;
        }
    }
}
