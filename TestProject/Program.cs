using System;
using System.Threading.Tasks;
using TestProject.Common.Core.Interfaces;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //List of .NetLab Tasks
            var tasks = new IRunnable[]
            {
                new TaskLibrary.Tasks.Lesson1.Task1(),
                //new TaskLibrary.Tasks.Lesson1.Task2(),
                //new TaskLibrary.Tasks.Lesson2.Task1(),             
            };

            foreach (var task in tasks)
            {
                task.Run();
            }
            Console.ReadLine();
        }
    }
}
