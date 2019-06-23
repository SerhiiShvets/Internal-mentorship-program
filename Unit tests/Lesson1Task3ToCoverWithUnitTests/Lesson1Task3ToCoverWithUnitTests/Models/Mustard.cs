using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Mustard : Extra
    {
        public override double CalculateHappiness(double happiness)
        {
            MainFood.CalculateHappiness(happiness);
            Effect = 1;
            MainFood.Effect *= 0;
            return happiness + Effect;
        }

        public Mustard(IFood food) : base(food)
        {
            MainFood = (Food)food;
        }
        public override string ToString()
        {
            return $"food={MainFood.ToString()}, extras=[Mustard]";
        }
    }
}
