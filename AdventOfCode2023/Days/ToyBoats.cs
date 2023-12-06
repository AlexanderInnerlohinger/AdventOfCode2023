using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    internal class ToyBoats
    {
        public static void Run()
        {
            Console.WriteLine("ToyBoats-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_6_input_modified.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);


            string[] times = input[0].Split(':')[1].Split(' ');
            ulong[] timeArray = Array.ConvertAll(times.Where(x => !string.IsNullOrEmpty(x)).ToArray(), ulong.Parse);

            string[] distances = input[1].Split(':')[1].Split(' ');
            ulong[] distanceArray = Array.ConvertAll(distances.Where(x => !string.IsNullOrEmpty(x)).ToArray(), ulong.Parse);

            var waysToBeat = 1;
            for (ulong i = 0; i < (ulong)timeArray.Length; i++)
            {
                ulong recordDistance = distanceArray[i];
                List<ulong> brokenRecors = new List<ulong>();

                Console.WriteLine($"Commencing race for time entry {timeArray[i]} with record distance {recordDistance}:");

                for (ulong j = 0; j <= timeArray[i]; j++)
                {
                    ulong velocity = j;
                    ulong raceTime = timeArray[i] - velocity;

                    ulong distance = velocity * raceTime;

                    if (distance > recordDistance)
                        brokenRecors.Add(distance);

                    Console.WriteLine($"Distance reached was {distance} with velocity {velocity} and remaining racetime {raceTime}.\n");
                }

                waysToBeat *= brokenRecors.Count;
            }

            Console.WriteLine($"Product of all ways to beat the record is {waysToBeat}.");

        }
    }
}
