using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class CubeConundrum
    {
        public static void Run()
        {
            int redCubesMax = 12;
            int greenCubesMax = 13;
            int blueCubesMax = 14;
            int sumIDPossibleGames = 0;

            Console.WriteLine("CubeConundrum-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_2_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);

            foreach (string s in input)
            {
                bool gamePossible = true;

                string[] splittedInput = s.Split(':');
                int gameID = Convert.ToInt32(Regex.Match(splittedInput[0], @"\d+").Value);

                string[] gameList = splittedInput[1].Split(';');
                foreach (string singleGame in gameList)
                {
                    string[] cubeConfiguration = singleGame.Split(',');

                    for (int i = 0; i < cubeConfiguration.Length; i++)
                    {
                        int amountCubes = Convert.ToInt32(Regex.Match(cubeConfiguration[i], @"\d+").Value);
                        string color = Regex.Match(cubeConfiguration[i], @"[a-zA-Z]+").Value;



                        switch (color)
                        {
                            case "red":
                                if (amountCubes > redCubesMax) 
                                    gamePossible = false;

                                break;

                            case "green":
                                if (amountCubes > greenCubesMax)
                                    gamePossible = false;

                                break;

                            case "blue":
                                if (amountCubes > blueCubesMax)
                                    gamePossible = false;

                                break;

                            default:
                                Console.WriteLine("Unknown color !\n\n");
                                break;
                        }
                    }
                }

                if (gamePossible)
                {
                    Console.WriteLine("Game " + gameID + " is possible !\n\n");
                    sumIDPossibleGames += gameID;
                }
                else
                    Console.WriteLine("Game " + gameID + " is not possible !\n\n");
            }

            Console.WriteLine($"The sum of the possible game-IDs is {sumIDPossibleGames}.");
        }
    }
}
