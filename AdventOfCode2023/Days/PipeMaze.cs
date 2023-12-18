using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    internal class PipeMaze
    {
        public enum Directions
        {
            North,
            East,
            South,
            West,
            Unknown
        }

        public enum PipeSegments
        {
            NorthSouth = '|',
            EastWest = '-',
            NorthEast = 'L',
            NorthWest = 'J',
            SouthWest = '7',
            SouthEast = 'F',
            Ground = '.',
            Start = 'S'
        }

        public static void Run()
        {
            Console.WriteLine("PipeMaze-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_10_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);

            Directions enteringDirection = Directions.West; // according to exactly this input
            Directions leavingDirection = Directions.Unknown;

            char currentPipeSegment = (char)PipeSegments.EastWest;  // according to exactly this input
            char nextPipeSegment = currentPipeSegment;
            int stepCounter = 1;


            char[,] grid = new char[input[0].Length, input.Length];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[j, i] = input[i][j];
                }
            }

            Point currentIndex = LinearSearch(grid, (char)PipeSegments.Start);
            currentIndex.X += 1;    // according to exactly this input

            while (true)
            {
                switch (enteringDirection)
                {
                    case Directions.West:

                        switch (nextPipeSegment)
                        {
                            case (char)PipeSegments.EastWest:
                                leavingDirection = Directions.East;
                                currentIndex.X += 1;
                                break;
                            case (char)PipeSegments.NorthWest:
                                leavingDirection = Directions.North;
                                currentIndex.Y -= 1;
                                break;
                            case (char)PipeSegments.SouthWest:
                                leavingDirection = Directions.South;
                                currentIndex.Y += 1;
                                break;
                        }
                        break;

                    case Directions.North:

                        switch (nextPipeSegment)
                        {
                            case (char)PipeSegments.NorthSouth:
                                leavingDirection = Directions.South;
                                currentIndex.Y += 1;
                                break;
                            case (char)PipeSegments.NorthWest:
                                leavingDirection = Directions.West;
                                currentIndex.X -= 1;
                                break;
                            case (char)PipeSegments.NorthEast:
                                leavingDirection = Directions.East;
                                currentIndex.X += 1;
                                break;
                        }
                        break;


                    case Directions.East:

                        switch (nextPipeSegment)
                        {
                            case (char)PipeSegments.EastWest:
                                leavingDirection = Directions.West;
                                currentIndex.X -= 1;
                                break;
                            case (char)PipeSegments.SouthEast:
                                leavingDirection = Directions.South;
                                currentIndex.Y += 1;
                                break;
                            case (char)PipeSegments.NorthEast:
                                leavingDirection = Directions.North;
                                currentIndex.Y -= 1;
                                break;
                        }
                        break;

                    case Directions.South:

                        switch (nextPipeSegment)
                        {
                            case (char)PipeSegments.NorthSouth:
                                leavingDirection = Directions.North;
                                currentIndex.Y -= 1;
                                break;
                            case (char)PipeSegments.SouthEast:
                                leavingDirection = Directions.East;
                                currentIndex.X += 1;
                                break;
                            case (char)PipeSegments.SouthWest:
                                leavingDirection = Directions.West;
                                currentIndex.X -= 1;
                                break;
                        }
                        break;

                }


                enteringDirection = GetComplimentaryDirection(leavingDirection);
                nextPipeSegment = grid[currentIndex.X, currentIndex.Y];
                stepCounter++;

                Console.WriteLine($"Next entering direction: {enteringDirection}");
                Console.WriteLine($"Next pipe segment: {nextPipeSegment}");
                Console.WriteLine($"Current step counter: {stepCounter}");

                if (nextPipeSegment == (char)PipeSegments.Start)
                {
                    break;
                }
            }

            Console.WriteLine($"\nFound back to start after {stepCounter} steps.");
            Console.WriteLine($"Thus the longest distance is {stepCounter / 2}");
        }

        private static Directions GetComplimentaryDirection(Directions leavingDirection)
        {
            switch (leavingDirection)
            {
                case Directions.East:
                    return Directions.West;
                case Directions.West:
                    return Directions.East;
                case Directions.North:
                    return Directions.South;
                case Directions.South:
                    return Directions.North;
                default:
                    return Directions.Unknown;
            }
        }

        static Point LinearSearch(char[,] grid, char target)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == target)
                    {
                        return new Point(i,j);
                    }
                }
            }
            return new Point(-1,-1);
        }
    }
}
