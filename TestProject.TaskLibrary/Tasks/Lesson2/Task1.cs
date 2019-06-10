using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson2
{
    public class Task1 : IRunnable
    {
        public void Run()
        {
            int indexOfChosenSlot;
            var freeSlotsToOffer = new List<string>();
            var timeSheet = new TimeSheet();
            //This is a type casting to invoke all the methods from TimeSheetEnumerator. I am not sure it is a good idea
            TimeSheetEnumerator enumeratorForTimeSlots = (TimeSheetEnumerator)timeSheet.GetEnumerator();

            Console.WriteLine("Choose and input a timeslot");

            foreach(string s in timeSheet.timeSlots)
            {
                Console.Write(s + " ");
            }

            Console.WriteLine("");
            string chosenSlot = Console.ReadLine();

            Console.WriteLine("Input a quantity of timeslots");
            int quantityOfSlotsToShow = Convert.ToInt32(Console.ReadLine());
            indexOfChosenSlot =  enumeratorForTimeSlots.GetIndexOfChosenTimeSlot(chosenSlot);
            enumeratorForTimeSlots.SetStartPositionAndQuantityOfSlotsToShow(indexOfChosenSlot, quantityOfSlotsToShow);

            //This is a way to collect free slots

            while (enumeratorForTimeSlots.MoveNext())
            {
                freeSlotsToOffer.Add(enumeratorForTimeSlots.Current);
            }

            Console.Write(chosenSlot + " ");
            foreach (var slot in enumeratorForTimeSlots.freeSlots)
            {
                Console.Write(slot + " ");
            }
            
            Console.ReadKey();
        }

        
    }
}
