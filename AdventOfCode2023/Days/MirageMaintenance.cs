using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    internal class MirageMaintenance
    {
        public static void Run()
        {
            Console.WriteLine("MirageMaintenance-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_9_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);

            int sumExtrapolatedValues = 0;
            foreach (string report in input)
            {
                List<int[]> extrapolationIterations = new List<int[]>();
                int[] history = Array.ConvertAll(report.Split(' ').ToArray(), int.Parse);

                int counter = 0;
                extrapolationIterations.Add(history);
                do
                {
                    extrapolationIterations.Add(new int[extrapolationIterations[counter].Length - 1]);

                    for (int i = 0; i < extrapolationIterations[counter].Length - 1; i++)
                    {
                        extrapolationIterations[counter + 1][i] = extrapolationIterations[counter][i + 1] -
                                                  extrapolationIterations[counter][i];
                    }

                    counter++;

                } while (!extrapolationIterations[counter].All(element => element.Equals(0)));

                foreach (int[] extrapolationIteration in extrapolationIterations)
                    Console.WriteLine(string.Join(" ", extrapolationIteration));

                int[] missingValues = new int[extrapolationIterations.Count];
                missingValues[^1] = 0;
                for (int i = extrapolationIterations.Count - 1; i > 0; i--)
                    missingValues[i - 1] = extrapolationIterations[i - 1].Last() + missingValues[i];

                for (int i = 0; i < extrapolationIterations.Count; i++)
                    Console.WriteLine($"Missing value for {string.Join(" ", extrapolationIterations[i])} should be {missingValues[i]}");

                sumExtrapolatedValues += missingValues[0];
                Console.WriteLine($"For history of {string.Join(" ", history)} the next value should be {missingValues[0]}.");
            }

            Console.WriteLine($"\nThe sum of the extrapolated values is {sumExtrapolatedValues}.");
        }
    }
}
