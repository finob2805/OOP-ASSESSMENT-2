using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Testing
    {
        SevensOut TestGame = new SevensOut();
        ThreeOrMore TestGame2 = new ThreeOrMore();
        public void TestingGame()
        {
            Console.WriteLine("----------TESTING----------"); //signifies that this run is a test run to verify the numbers we expect are correct
            Console.WriteLine("----------TESTING----------");
            Console.WriteLine("----------TESTING----------");
            Thread.Sleep(2000);
            int SevenSumCheck = TestGame.SevensOutGame("TEST"); // calls method from Game class
            Debug.Assert(SevenSumCheck == 7, "The sum of the dice never reached 7 so an error occured");
            Random InARowRand = new Random();
            Random ScoreRand = new Random();
            int InARow = InARowRand.Next(3, 6);
            int RandomScore = ScoreRand.Next(0, 18); // goes up to 17 which is the max value that will always next = 20 no matter what score is added
            int Score = TestGame2.ScoreCalculator(InARow, RandomScore);
            if (InARow == 3)
            {
                Debug.Assert(Score == RandomScore+3, "The sum of the dice didn't reach its expected value and so there was an error");
            }
            else if (InARow == 4)
            {
                Debug.Assert(Score == RandomScore+6, "The sum of the dice didn't reach its expected value and so there was an error");
            }
            else if (InARow == 5)
            {
                Debug.Assert(Score == RandomScore+12, "The sum of the dice didn't reach its expected value and so there was an error");
            }
            int TwentyOrOverCheck = TestGame2.ThreeOrMoreGame("TEST");
            Debug.Assert(TwentyOrOverCheck >= 20, "The score didn't reach 20 but the value was returned so there was an error");
        }
    }
}