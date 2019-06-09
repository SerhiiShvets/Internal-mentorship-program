using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson2
{
    public class TimeSheet
    {
        public string[] timeSlots = { "9:00", "9:15", "9:30", "9:45", "10:00", "10:15", "10:30", "10:45", "11:00" };
        public IEnumerator<string> GetEnumerator()
        {
            return new TimeSheetEnumerator(timeSlots);
        }
    }
}
