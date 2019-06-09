using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TaskLibrary.Tasks.Lesson2
{
    public class TimeSheetEnumerator : IEnumerator<string>
    {
        int quantity;
        int position;
        int positionToGoBack;
        int counter = 0;
        int counterToCheckWhetherItIsOddOrEven = 0;
        string[] timeSlots;
        public List<string> freeSlots = new List<string>();

        public TimeSheetEnumerator(string[] availableTimeSlots)
        {
            timeSlots = availableTimeSlots;
        }

        public int GetIndexOfChosenTimeSlot(string choice)
        {
            for (int i = 0; i < timeSlots.Length; i++)
            {
                if (choice == timeSlots[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void  SetStartPositionAndQuantityOfSlotsToShow(int position, int quantity)
        {
            this.position = position;
            this.quantity = quantity;
            this.positionToGoBack = position;
        }

        public string Current
        {
            get
            {
                if (position == -1 || position >= timeSlots.Length)
                    throw new InvalidOperationException();
                return timeSlots[position];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
             //           || counterToCheckWhetherItIsOddOrEven <= quantity
        public bool MoveNext()
        {
            if (counter < quantity-1)
            {
                if (counterToCheckWhetherItIsOddOrEven % 2 != 0 && position < timeSlots.Length - 2)
                {
                    position++;
                    counter++;
                    counterToCheckWhetherItIsOddOrEven++;
                    Console.WriteLine("position is " + position);
                    Console.WriteLine("counter is " + counterToCheckWhetherItIsOddOrEven);
                    Console.WriteLine(timeSlots[position]);
                    freeSlots.Add(timeSlots[position]);
                    return true;
                }
                else if (positionToGoBack >= 1 && counterToCheckWhetherItIsOddOrEven % 2 == 0)
                {
                    positionToGoBack--;
                    counter++;
                    counterToCheckWhetherItIsOddOrEven++;
                    Console.WriteLine("positionToGoBack i " + positionToGoBack);
                    Console.WriteLine("counter is " + counterToCheckWhetherItIsOddOrEven);
                    Console.WriteLine(timeSlots[positionToGoBack]);
                    freeSlots.Add(timeSlots[position]);
                    return true;
                }
                else
                {
                    counterToCheckWhetherItIsOddOrEven++;
                    return true;
                }
                //return false;
            }
            return false;
        }

        public void Reset()
        {
            position = -1;

        }
    }
}
