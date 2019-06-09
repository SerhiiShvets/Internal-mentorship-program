using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class FoodReadyEventArgs
    {
        public string Message { get; }
        public FoodReadyEventArgs(Order order)
        {
            Message = order.Food+"and"+order.Extras+"are ready";

        }
    }
}
