using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1.BL
{
    public abstract class Extra : IFood
    {
        public abstract double CalculateHappiness(double happiness);

        public Extra()
        {

        }
    }
}
