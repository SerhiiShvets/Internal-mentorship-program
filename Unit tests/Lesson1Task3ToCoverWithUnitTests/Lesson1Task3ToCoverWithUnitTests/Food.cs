using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
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
