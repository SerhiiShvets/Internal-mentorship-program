﻿using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
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
