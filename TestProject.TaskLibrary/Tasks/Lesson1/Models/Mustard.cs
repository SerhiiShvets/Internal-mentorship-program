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
            Effect = 1;
            MainFood.Effect *= 0;
            return happiness + Effect;
        }

        public Mustard(IFood food) : base (food)
        {
            MainFood = (Food)food;
        }
    }
}
