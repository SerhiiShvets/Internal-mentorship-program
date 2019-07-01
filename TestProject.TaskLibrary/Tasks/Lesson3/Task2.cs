using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson3
{
    public class Task2 : IRunnable
    {
        public void Run()
        {
            var employees = new[]
            {
                new {Name="Andras", Salary=420},
                new {Name="Bela", Salary=400},
                new {Name="Csaba", Salary=250},
                new {Name="David", Salary=300},
                new {Name="Endre", Salary=620},
                new {Name="Ferenc", Salary=350},
                new {Name="Gabor", Salary=410},
                new {Name="Hunor", Salary=500},
                new {Name="Imre", Salary=900},
                new {Name="Janos", Salary=600},
                new {Name="Karoly", Salary=700},
                new {Name="Laszlo", Salary=400},
                new {Name="Marton", Salary=500},
            };

            //1
            string oneWhoEarnsTheMost = (from employee in employees
                                         orderby employee.Salary descending
                                         select employee.Name).First();
            Console.WriteLine(oneWhoEarnsTheMost);
            //2

            var thoseWhoEarnLessThanAverage = from employee in employees
                                              where employee.Salary <
                                              (from em in employees select em.Salary).Average()
                                              select employee.Name;
            Console.WriteLine("The employees who earn less than average salary are: \n"
                + string.Join(" ", thoseWhoEarnLessThanAverage));
            //3
            var employeesSortedAscending = from employee in employees
                                           orderby employee.Salary
                                           select employee;
            Console.WriteLine("Employees sorted in an ascending order by salary:\n"
                + string.Join("\n", employeesSortedAscending));
            //4
            var distinctSalariesEmployees = employees.OrderBy(e => e.Salary)
                                                     .ThenBy(e => e.Name)
                                                     .GroupBy(e => e.Salary)
                                                     .Where(g => g.Count() > 1)
                                                     .SelectMany(g => g);

            Console.WriteLine("Employees who earn the same money are:\n"
       + string.Join("\n", distinctSalariesEmployees));

            //5
            var ytyt = employees.Select(x => new
            {
                Group = x.Salary > 200 && x.Salary < 400
                        ? "200-399"
                        : x.Salary < 600
                            ? "2"
                            : x.Salary < 800
                                ? "3"
                                : "skip",
                Name = x.Name
            }).GroupBy(x => x.Group).Where(x => x.Key != "skip");


            var groupedSalariesFrom200To399 = employees.Where(e => e.Salary >= 200 && e.Salary < 400)
                                                       .GroupBy(e => e.Name)
                                                       .SelectMany(g => g);
            var groupedSalariesFrom400To599 = employees.Where(e => e.Salary >= 400 && e.Salary < 600)
                                                       .GroupBy(e => e.Name)
                                                       .SelectMany(g => g);
            var groupedSalariesFrom600To799 = employees.Where(e => e.Salary >= 600 && e.Salary < 800)
                                                       .GroupBy(e => e.Name)
                                                       .SelectMany(g => g);
            var groupedSalariesFrom800To999 = employees.Where(e => e.Salary >= 800 && e.Salary < 1000)
                                                       .GroupBy(e => e.Name)
                                                       .SelectMany(g => g);

            Console.WriteLine("Employees grouped by salary are:\n"
                + string.Join("\n", groupedSalariesFrom200To399));
            Console.WriteLine(""
                + string.Join("\n", groupedSalariesFrom400To599));
            Console.WriteLine(""
               + string.Join("\n", groupedSalariesFrom600To799));
            Console.WriteLine(""
               + string.Join("\n", groupedSalariesFrom800To999));


            Console.WriteLine(""
               + string.Join("\n", ytyt));
        }
    }
}
