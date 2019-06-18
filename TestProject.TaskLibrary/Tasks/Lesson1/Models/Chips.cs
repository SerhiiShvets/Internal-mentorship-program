using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Chips : Food
    {

        public override double CalculateHappiness(double happiness)
        {
            Effect = happiness * 0.05;
            return happiness + Effect;
        }
        public Chips()
        {

        }
    }
}
