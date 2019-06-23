using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Ketchup : Extra
    {
        public override double CalculateHappiness(double happiness)
        {
            MainFood.CalculateHappiness(happiness);
            Effect = MainFood.Effect * 2;
            return happiness + Effect;
        }

        public Ketchup(IFood food) : base(food)
        {
            MainFood = (Food)food;
        }
        public override string ToString()
        {
            return $"food={MainFood.ToString()}, extras=[Ketchup]";
        }
    }
}
