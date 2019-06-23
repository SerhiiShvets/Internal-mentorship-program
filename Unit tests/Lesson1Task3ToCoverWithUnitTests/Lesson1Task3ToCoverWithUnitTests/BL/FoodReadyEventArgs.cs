using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class FoodReadyEventArgs : EventArgs
    {
        public string Message { get; }
        public string Food { get; }
        IEnumerable<string> Extras { get; }
        public IFood thisFood;

        public FoodReadyEventArgs(IFood food)
        {
            thisFood = food;
            Message = food.ToString();
        }
    }
}
