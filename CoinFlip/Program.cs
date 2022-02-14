using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinFlip
{
    public class Program
    {
        public bool[] coins = new bool[64];
        Random rand = new Random();
        long input;
        long output;

        long manualInput = 0;
        bool useManualInput = false;

        long ones = 0;
        long zeros = 0;

        void Reset()
        {
            ones = 0;
            zeros = 0;
            input = 0;
            output = 0;
            coins = new bool[64];
        }

        void Start(long iInput)
        {
            manualInput = iInput;
            string str = "";
            if (useManualInput)
            {
                str = Convert.ToString(manualInput, 2);

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == Convert.ToChar("1"))
                    {
                        coins[i] = true;
                    }
                    else
                    {
                        coins[i] = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < coins.Length; i++)
                {
                    double o = rand.NextDouble();
                    if (o > 0.5)
                    {
                        coins[i] = true;
                        str += "1";
                    }
                    else
                    {
                        coins[i] = false;
                        str += "0";
                    }
                }
            }
           
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == Convert.ToChar("0"))
                {
                    zeros++;
                }
                else
                {
                    ones++;
                }
            }

            input = Convert.ToInt64(str, 2);
            //Console.WriteLine("Input: " + input + ", str: " + str);
            //Console.WriteLine("0: " + zeros + ", 1: " + ones);

            bool[] row1Coins = row2(row1());
            string str1 = "";
            for (int i = 0; i < row1Coins.Length; i++)
            {
                str1 += Convert.ToInt64(row1Coins[i]).ToString();
            }
            output = Convert.ToInt64(str1, 2);
            //Console.WriteLine("Output: " + output);
            //Console.WriteLine("(" + input + ", " + output + ")");

            Reset();
        }

        bool[] row1()
        {
            bool[] row1Coins = coins;

            for (int i = 0; i < (coins.Length/2)-1; i++)
            {
                row1Coins[i] = coins[i] && coins[i + 1];
            }

            for (int i = coins.Length / 2; i < coins.Length-1; i++)
            {
                row1Coins[i] = coins[i] || coins[i + 1];
            }

            return row1Coins;
        }

        bool[] row2(bool[] extraInput)
        {
            bool[] row2Coins = coins;

            for (int i = 0; i < (coins.Length / 2) - 1; i++)
            {
                row2Coins[i] = coins[i] && extraInput[i + 1];
            }

            for (int i = extraInput.Length / 2; i < extraInput.Length - 1; i++)
            {
                row2Coins[i] = extraInput[i] || coins[i + 1];
            }

            return row2Coins;
        }
        static void Main(string[] args)
        {
            Program program = new Program();

            DateTime time = DateTime.Now;

            for (int i = 0; i < int.MaxValue; i++)
            {
                program.Start(i);
            }

            Console.WriteLine("\n Time: " + DateTime.Compare(DateTime.Now, time));

            Console.ReadLine();
        }
    }
}
