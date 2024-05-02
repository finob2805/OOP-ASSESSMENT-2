using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CMP1903_A1_2324
{
    public class Game
    {
        protected Statistics Stats = new Statistics();
        protected Die Die1 = new Die();
        protected int SevensPlayer1Score;
        protected int SevensPlayer2Score;
        protected int ThreesPlayer1Score = 0;
        protected int ThreesPlayer2Score = 0;
        protected int PlayerTurn = 1;
        private string? ComputerPlayer;
        public void GameMenu()
        {
            string PathName = @"C:\Users\finla\OneDrive\Documents\OOP Project\ConsoleApp1\Stats.txt";
            if (File.Exists(PathName) == false)
            {
                var CreateFile = File.Create(PathName);
                CreateFile.Close();
                for (int i = 0; i < 7; i++)
                {
                    File.AppendAllText(PathName, "0" + Environment.NewLine);
                }
            }
            while (true)
            {   
                Console.WriteLine("Do you wish to play against a partner (ENTER 1) or the computer (ENTER 2)?");
                string? Opponent = Console.ReadLine();
                if (Opponent == "1")
                {
                    ComputerPlayer = "NO";
                    break;
                }
                else if (Opponent == "2")
                {
                    ComputerPlayer = "YES";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please re-enter");
                }
            }
            while (true)
            {
                Console.WriteLine("Enter 1 for Sevens Out");
                Console.WriteLine("Enter 2 for Three or More");
                Console.WriteLine("Enter 3 for Statistics");
                Console.WriteLine("Enter 4 for Testing");
                Console.WriteLine("Enter 5 to Quit");
                int Choice;
                try
                {
                    Choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------");
                    if (Choice == 1)
                    {
                        SevensOut Sevens = new SevensOut();
                        Sevens.SevensOutGame(ComputerPlayer);
                    }
                    else if (Choice == 2)
                    {
                        ThreeOrMore Threes = new ThreeOrMore();
                        Threes.ThreeOrMoreGame(ComputerPlayer);
                    }

                    else if (Choice == 3)
                    {
                        Stats.StatisticsView();
                    }
                    else if (Choice == 4)
                    {
                        Testing GameTest = new Testing(); // creates testing object
                        GameTest.TestingGame(); // calls method
                    }
                    else if (Choice == 5)
                    {
                        Console.WriteLine("Goodbye");
                        break;
                    }
                    else if (Choice < 1 || Choice > 5)
                    {
                    Console.WriteLine("Invalid input. Please re-enter");
                    Console.WriteLine("--------------------------------");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please re-enter");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
    }
    public class SevensOut : Game
    {
        public int SevensOutGame(string ComputerPlayer)
        {
            while (true)
            {
                if (PlayerTurn == 1 || (PlayerTurn == 2 && ComputerPlayer == "NO"))
                {
                    Console.WriteLine("Enter any key to roll your dice");
                    Console.ReadLine();                 
                }
                else
                {
                    Console.WriteLine("Computer will roll the dice");
                }
                int RollOne = Die1.DiceRoll();
                int RollTwo = Die1.DiceRoll();
                Console.WriteLine("ROLL1 is " +RollOne);
                Console.WriteLine("ROLL2 is " + RollTwo);
                if (RollOne + RollTwo == 7)
                {
                    Console.WriteLine("ROLLED A 7!");
                    if (ComputerPlayer == "TEST")
                    {
                        return RollOne+RollTwo;
                    }
                    break;
                }
                if (PlayerTurn == 1)
                {
                    SevensPlayer1Score = SevensPlayer1Score + SevensScoreCalculator(RollOne, RollTwo);
                    Console.WriteLine("PLAYER 1's SCORE IS " + SevensPlayer1Score);
                    PlayerTurn++;
                    Console.WriteLine("--------------------SWITCHING PLAYER--------------------");

                }
                else
                {
                    SevensPlayer2Score = SevensPlayer2Score + SevensScoreCalculator(RollOne, RollTwo);
                    Console.WriteLine("PLAYER 2's SCORE IS " + SevensPlayer2Score);
                    PlayerTurn--;
                    Console.WriteLine("--------------------SWITCHING PLAYER--------------------");
                }
            }
            Console.WriteLine("PLAYER 1's SCORE IS " + SevensPlayer1Score);
            Console.WriteLine("PLAYER 2's SCORE IS " + SevensPlayer2Score);
            if (SevensPlayer1Score > SevensPlayer2Score)
            {
                Console.WriteLine("PLAYER 1 WINS");
                PlayerTurn = 1;
                Stats.StatisticsAppend(1, 0, 0, SevensPlayer1Score, 0, 1, 0);
                return 0;
            }
            else if (SevensPlayer2Score > SevensPlayer1Score)
            {
                Console.WriteLine("PLAYER 2 WINS");
                PlayerTurn = 1;
                if (ComputerPlayer == "YES")
                {
                    Stats.StatisticsAppend(0, 0, 1, SevensPlayer2Score, 0, 1, 0);
                }
                else
                {
                    Stats.StatisticsAppend(0, 1, 0, SevensPlayer2Score, 0, 1, 0);
                }
                return 0;
            }
            else
            {
                Console.WriteLine("DRAW");
                PlayerTurn = 1;
                Stats.StatisticsAppend(0, 0, 0, SevensPlayer1Score, 0, 1, 0);
                return 0;
            }
        }
        public int SevensScoreCalculator(int RollOne, int RollTwo)
        {
            int RoundTotal;
            if (RollOne == RollTwo)
            {
                RoundTotal = (RollOne + RollTwo)*2;
            }
            else
            {
                RoundTotal = RollOne + RollTwo;
            }      
            return RoundTotal;      
        }
    }
    public class ThreeOrMore : Game
    {
        public int ThreeOrMoreGame(string ComputerPlayer)
        {
            while (ThreesPlayer1Score < 20 && ThreesPlayer2Score < 20)
            {
                if (PlayerTurn == 1 || (PlayerTurn == 2 && ComputerPlayer == "NO"))
                {
                    Console.WriteLine("Enter any key to roll your dice");
                    Console.ReadLine();                 
                }
                else
                {
                    Console.WriteLine("Computer will roll their dice");
                    Thread.Sleep(1500);
                }
                List<int> ListOfRolls = ListCreator(0);
                int AreTheSame = OfAKindChecker(ListOfRolls);
                int MostFrequentNum = MostFrequent(ListOfRolls);
                if (AreTheSame == 2)
                {
                    Console.WriteLine("Enter 1 to re-roll the remaining dice that aren't the same");
                    Console.WriteLine("Enter 2 to re-roll all the dice");
                    if (PlayerTurn == 1 || (PlayerTurn == 2 && ComputerPlayer == "NO"))
                    {
                        while (true)
                        {
                            string? ReRollChoice = Console.ReadLine();
                            Console.WriteLine("--------------------------------");
                            if (ReRollChoice == "1")
                            {
                                ListOfRolls = ListCreator(MostFrequentNum);
                                AreTheSame = OfAKindChecker(ListOfRolls);
                                break;
                            }
                            else if (ReRollChoice == "2")
                            {
                                ListOfRolls = ListCreator(0);
                                AreTheSame = OfAKindChecker(ListOfRolls);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please re-enter");
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(3500);
                        Random RandomOneToTwo = new Random();
                        int ComputerChoice = RandomOneToTwo.Next(1, 3);
                        if (ComputerChoice == 1)
                        {
                            Console.WriteLine("Computer has chosen to randomise their remaining dice");
                            ListOfRolls = ListCreator(MostFrequentNum);
                            AreTheSame = OfAKindChecker(ListOfRolls);
                        }
                        else
                        {
                            Console.WriteLine("Computer has chosen to fully randomise their dice");
                            ListOfRolls = ListCreator(0);
                            AreTheSame = OfAKindChecker(ListOfRolls);
                        }
                    }
                }
                if (PlayerTurn == 1 && AreTheSame > 2)
                {
                    ThreesPlayer1Score = ScoreCalculator(AreTheSame, ThreesPlayer1Score);
                }
                else if (PlayerTurn == 2 && AreTheSame > 2)
                {
                    ThreesPlayer2Score = ScoreCalculator(AreTheSame, ThreesPlayer2Score);
                }
                if (ThreesPlayer1Score >= 20 || ThreesPlayer2Score >= 20)
                {
                    if (PlayerTurn == 1)
                    {
                        Console.WriteLine("Player 1's score is " + ThreesPlayer1Score);
                        if (ComputerPlayer == "TEST")
                        {
                            return ThreesPlayer1Score;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Player 2's score is " + ThreesPlayer2Score);
                        if (ComputerPlayer == "TEST")
                        {
                            return ThreesPlayer2Score;
                        }
                    }
                    Console.WriteLine("PLAYER " + PlayerTurn + " WINS");
                }
                if (PlayerTurn == 1 && ThreesPlayer1Score < 20 && ThreesPlayer2Score < 20)
                {
                    Console.WriteLine("Player 1's score is " + ThreesPlayer1Score);
                    Console.WriteLine("--------------------SWITCHING PLAYER--------------------");
                    Thread.Sleep(3000);
                    Console.WriteLine("PLAYER 2's TURN");
                    PlayerTurn++;
                }
                else if (PlayerTurn == 2 && ThreesPlayer1Score < 20 && ThreesPlayer2Score < 20)
                {
                    Console.WriteLine("Player 2's score is " + ThreesPlayer2Score);
                    Console.WriteLine("--------------------SWITCHING PLAYER--------------------");
                    Thread.Sleep(3000);
                    Console.WriteLine("PLAYER 1's TURN");
                    PlayerTurn--;
                }
            }
            if (PlayerTurn == 1)
            {
                Stats.StatisticsAppend(1, 0, 0, 0, ThreesPlayer1Score, 0, 1);
                return 0;
            }
            else
            {
                if (ComputerPlayer == "YES")
                {
                    Stats.StatisticsAppend(0, 0, 1, 0, ThreesPlayer2Score, 0, 1);
                }
                else
                {
                    Stats.StatisticsAppend(0, 1, 0, 0, ThreesPlayer2Score, 0, 1);
                }
                PlayerTurn--;
                return 0;
            }          
        }

        private int OfAKindChecker(List<int> ListOfRolls)
        {
            int Count = 1;
            int CountSaver = 1;
            for (int i = 0; i < ListOfRolls.Count-1; i++)
            {
                if (ListOfRolls[i] == ListOfRolls[i+1])
                {
                    Count++;
                }
                else
                {
                    if (Count > CountSaver)
                    {
                        CountSaver = Count;
                    }
                    Count = 1; 
                }
            }
            if (Count > CountSaver)
            {
                CountSaver = Count;
            }
            Console.WriteLine(CountSaver + " of a kind");
            return CountSaver;
        }

        private int MostFrequent(List<int> ListOfRolls)
        {
            int Count = 1;
            int CountSaver = 1;
            int MostFrequentNum = ListOfRolls[0];
            for (int i = 0; i < ListOfRolls.Count-1; i++)
            {
                if (ListOfRolls[i] == ListOfRolls[i+1])
                {
                    Count++;
                }
                else
                {
                    if (Count > CountSaver)
                    {
                        CountSaver = Count;
                        MostFrequentNum = ListOfRolls[i];
                        Count = 1;
                    }
                }
            }
            if (Count > CountSaver)
                {
                    CountSaver = Count;
                    MostFrequentNum = ListOfRolls[4];
                }
            return MostFrequentNum;
        }

        private List<int> ListCreator(int TwoOfAKind)
        {   
            if (TwoOfAKind == 0)
            {
                List<int> ListOfRolls = new List<int>{Die1.DiceRoll(), Die1.DiceRoll(), Die1.DiceRoll(), Die1.DiceRoll(), Die1.DiceRoll()};
                ListOfRolls.Sort();
                for (int i = 0; i < ListOfRolls.Count; i++)
                {
                    Console.WriteLine(ListOfRolls[i]);
                }
                return ListOfRolls;               
            }
            else
            {
                List<int> ListOfRolls = new List<int>{TwoOfAKind, TwoOfAKind, Die1.DiceRoll(), Die1.DiceRoll(), Die1.DiceRoll()};
                ListOfRolls.Sort();
                for (int i = 0; i < ListOfRolls.Count; i++)
                {
                    Console.WriteLine(ListOfRolls[i]);
                }
                return ListOfRolls; 
            }
        }

        public int ScoreCalculator(int AreTheSame, int Score)
        {

            if (AreTheSame == 3)
            {
                Score = Score + 3;
            }
            else if (AreTheSame == 4)
            {
                Score = Score + 6;
            }
            else
            {
                Score = Score + 12;
            }
            return Score;
        }
    }
}

            // int[] DiceRollList = {Die1.DiceRoll(), Die2.DiceRoll(), Die3.DiceRoll()}; // creates array for storing the number that comes from the rolls
            // int Sum = 0; // create sum variable
            // for (int Count = 0; Count <=2; Count++) // for loop goes through the array and adds up dice rolls
            // {
            //     Sum = Sum + DiceRollList[Count];
            //     Console.WriteLine("This die (" + (Count+1) + ") rolled " + DiceRollList[Count]); // outputs what the die rolled
            // }
            // Console.WriteLine("The total of the 3 rolls was " + Sum); // outputs sum of rolls
            // return DiceRollList; // returns the list