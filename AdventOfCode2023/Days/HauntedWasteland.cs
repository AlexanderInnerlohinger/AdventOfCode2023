using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    internal class HauntedWasteland
    {
        public static void Run()
        {
            Console.WriteLine("HauntedWasteland-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_8_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);

            List<Node> nodes = new List<Node>();
            string instuctionList = input[0];

            for (int i = 0; i < input.Length-2; i++)
                nodes.Add(new Node(input[i + 2]));


            int startIndex = nodes.FindIndex(x => x.Origin.Equals("AAA"));
            Node currentNode = nodes[startIndex];
            string nextDestination;
            int steps = 0;

            do
            {
                foreach (char destination in instuctionList)
                {
                    steps++;

                    switch (destination)
                    {
                        case 'R':
                            nextDestination = currentNode.DestinationRight;
                            break;
                        case 'L':
                            nextDestination = currentNode.DestinationLeft;
                            break;
                        default:
                            throw new ArgumentException($"Destination \"{destination}\" not defined as right or left.");
                    }

                    Console.WriteLine($"Went from {currentNode.Origin} = ({currentNode.DestinationLeft}, {currentNode.DestinationRight}) with instruction {destination} to {nextDestination}.");

                    int nextIndex = nodes.FindIndex(x => x.Origin.Equals(nextDestination));
                    currentNode = nodes[nextIndex];
                }
            } while (!currentNode.Origin.Equals("ZZZ"));

            Console.WriteLine($"There were {steps} steps necessary to reach the last destination.");
        }
    }

    internal class Node
    {
        public string Origin { get; set; }
        public string DestinationLeft { get; set; }
        public string DestinationRight { get; set; }

        public Node(string nodeLine)
        {
            string[] splitLine = nodeLine.Split('=');
            Origin = splitLine[0].Trim();

            string[] coordinates = splitLine[1].Substring(2, splitLine[1].Length - 3).Split(',');
            DestinationLeft = coordinates[0].Trim();
            DestinationRight = coordinates[1].Trim();
        }
    }
}
