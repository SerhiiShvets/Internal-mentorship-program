using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public interface IEvent
    {
        event EventHandler<FoodReadyEventArgs> ev;
    }
}
