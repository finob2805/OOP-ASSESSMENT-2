using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Markup;

namespace CMP1903_A1_2324
{
    public class Statistics // creates Statistics class
    {
        private string PathName = @"C:\Users\finla\OneDrive\Documents\OOP Project\ConsoleApp1\Stats.txt";
        public void StatisticsView()
        {
            string[] Values = StatisticsRead();
            Console.WriteLine("-----------------STATS-----------------");
            Console.WriteLine("PLAYER 1 WINS: " + Values[0]);
            Console.WriteLine("PLAYER 2 WINS: " + Values[1]);
            Console.WriteLine("COMPUTER WINS: " + Values[2]);
            Console.WriteLine("SEVENS OUT HIGH SCORE: " + Values[3]);
            Console.WriteLine("THREE OR MORE HIGH SCORE: " + Values[4]);
            Console.WriteLine("TIMES PLAYED SEVENS OUT: " + Values[5]);
            Console.WriteLine("TIMES PLAYED THREE OR MORE: " + Values[6]);
            Console.WriteLine("-----------------STATS-----------------");
        }
        public void StatisticsAppend(int P1Wins, int P2Wins, int ComputerWins, int SevensScore, int ThreesScore, int SevensPlayed, int ThreesPlayed)
        {
            string[] Values = StatisticsRead();
            P1Wins = P1Wins + Convert.ToInt32(Values[0]);
            Values[0] = Convert.ToString(P1Wins);
            P2Wins = P2Wins + Convert.ToInt32(Values[1]);
            Values[1] = Convert.ToString(P2Wins);
            ComputerWins = ComputerWins + Convert.ToInt32(Values[2]);
            Values[2] = Convert.ToString(ComputerWins);
            if (Convert.ToInt32(Values[3]) < SevensScore)
            {
                Values[3] = Convert.ToString(SevensScore);
            }
            if (Convert.ToInt32(Values[4]) < ThreesScore)
            {
                Values[4] = Convert.ToString(ThreesScore);
            }
            Values[5] = Convert.ToString(Convert.ToInt32(Values[5]) + SevensPlayed);
            Values[6] = Convert.ToString(Convert.ToInt32(Values[6]) + ThreesPlayed);
            for (int i = 0; i < 7; i++)
                {
                    File.WriteAllLines(PathName, Values, Encoding.UTF8);
                }            
            
        }
        private string[] StatisticsRead()
        {
            string[] Values = File.ReadAllLines(PathName);
            return Values;
        }
    }
}
