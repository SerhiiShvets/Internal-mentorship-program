using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class HotDog : Food
    {
        public override double CalculateHappiness(double happiness)
        {
            Effect = 2;
            return happiness + Effect;
        }

        public HotDog()
        {

        }
        public override string ToString()
        {
            return "Hotdog";
        }
    }
}
