using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public class Order
    {
        public IEnumerable<string> Extras { get; }
        public string Food { get; }

        public void NotifyReady(IFood food)
        {

        }
        public Order(string food, IEnumerable<string> extras)
        {
            Extras = extras;
            Food = food;
        }

    }
}
