using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    class HotDog : Food
    {

        public override double CalculateHappiness(double happiness)
        {
            Effect = 2;
            return happiness + Effect;
        }

        public HotDog() 
        {

        }
    }
}
