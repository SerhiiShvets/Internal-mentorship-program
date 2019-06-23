using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
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
        public override string ToString()
        {
            return "Chips";
        }
    }
}
