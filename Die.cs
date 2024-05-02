using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    public class Die // creates Die class
    {
        private int CurrentDieValue;  
        public int DiceRoll() // randomises a roll between 1 and 6 and then returns said roll in CurrentDieValue
        {
            Random RandomOneToSix = new Random();
            CurrentDieValue = RandomOneToSix.Next(1, 7); //must be 7 as that means all numbers from 1 up to 7-1
            return CurrentDieValue;
        }
    }
}
