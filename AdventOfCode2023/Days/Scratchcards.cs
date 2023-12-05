using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    internal class Scratchcards
    {
        private static int points = 0;

        public static void Run()
        {
            Console.WriteLine("Scratchcard-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_4_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);


            for (int i = 0; i < input.Length; i++)
            {
                int currentCardPoints = 0;
                string[] card = input[i].Split(':');
                int cardNumber = Convert.ToInt32(Regex.Match(card[0], @"\d+").Value);

                string[] numbers = card[1].Split('|');

                // Create new array without null or empty values
                string[] winningNumbers = numbers[0].Split(' ');
                string[] winningNumbersWithoutNullOrEmpty =
                    winningNumbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                string[] ownNumbers = numbers[1].Split(' ');
                string[] ownNumbersWithoutNullOrEmpty =
                    ownNumbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                foreach (string ownNumber in ownNumbersWithoutNullOrEmpty)
                {
                    if (winningNumbersWithoutNullOrEmpty.Contains(ownNumber))
                    {
                        if (currentCardPoints.Equals(0))
                            currentCardPoints = 1;

                        else
                            currentCardPoints *= 2;
                    }
                }

                Console.WriteLine($"Card {cardNumber} has {currentCardPoints} points.");
                points += currentCardPoints;

            }

            Console.WriteLine($"The points are worth in total {points}.");

        }
    }
}
