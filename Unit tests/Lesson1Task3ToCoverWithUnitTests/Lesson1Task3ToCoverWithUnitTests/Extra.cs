using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public abstract class Extra : IFood 
    {
        public double Effect { get; set; }
        public Food MainFood { get; set; }


        public abstract double CalculateHappiness(double happiness);

        public Extra(IFood food)
        {
            MainFood = (Food)food;
        }
    }
}
