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

        public bool MoveNext()
        {
            if (counter < quantity-1)
            {
                if (counterToCheckWhetherItIsOddOrEven % 2 != 0 && position < timeSlots.Length - 1)
                {
                    if (position < timeSlots.Length - 1)
                    {
                        position++;
                    }
                    counter++;
                    counterToCheckWhetherItIsOddOrEven++;                    
                    freeSlots.Add(timeSlots[position]);
                    return true;
                }
                if (counterToCheckWhetherItIsOddOrEven % 2 == 0 && positionToGoBack >= 1)
                {
                    positionToGoBack--;
                    counter++;
                    counterToCheckWhetherItIsOddOrEven++;
                    freeSlots.Add(timeSlots[positionToGoBack]);
                    return true;
                }
                else
                {
                    counterToCheckWhetherItIsOddOrEven++;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            position = -1;

        }
    }
}
