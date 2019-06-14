using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class FoodReadyEventArgs
    {
        public string Message { get; }
        public string Food { get; }
        IEnumerable<string> Extras { get; }
        public FoodReadyEventArgs(string message, string food, IEnumerable<string> extras)
        {
            Food = food;
            Extras = extras;
            Message = message;

        }
    }
}
