using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    internal class GearRatios
    {
        private static int m_sumPartNumbers = 0;
        private static Regex m_specialChar = new Regex(@"[-+*/!$%&=?#~@€^'<>|,;:_]");

        public static void Run()
        {

            Console.WriteLine("GearRatio-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_3_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);
            string[,] engineSchematic = GetSchematic(input);

            for (int i = 0; i < input.Length; i++)
            {
                // Get all available numbers in one line of the engine schematic
                MatchCollection numberCollection = Regex.Matches(input[i], @"(\d+)+");

                foreach (Match match in numberCollection)
                {
                    int index = match.Index;
                    int length = match.Length;
                    int number = Convert.ToInt32(match.Value);

                    // Check all possible directions for the current number
                    // Use try-catch for out-of-bounds exception from array check
                    CheckForSpecialCharacters( engineSchematic, index, length, i, number);
                }
            }

            Console.WriteLine($"The sum of the valid part numbers is {m_sumPartNumbers}.");
        }

        private static void CheckForSpecialCharacters(string[,] engineSchematic, int index, int length, int i,
            int number)
        {
            // Right side
            try
            {
                if (m_specialChar.IsMatch(engineSchematic[index + length, i]))
                {
                    Console.WriteLine($"Valid part found for number {number} at the right side.");
                    m_sumPartNumbers += number;
                    return;
                }
            }
            catch
            {
                // ignored
            }

            //Bottom right corner
            try
            {
                if (m_specialChar.IsMatch(engineSchematic[index + length, i + 1]))
                {
                    Console.WriteLine($"Valid part found for number {number} at the bottom right corner.");
                    m_sumPartNumbers += number;
                    return;
                }
            }
            catch
            {
                // ignored
            }

            // Bottom side
            try
            {
                for (int j = 0; j < length; j++)
                {
                    if (m_specialChar.IsMatch(engineSchematic[index + j, i + 1].ToCharArray()))
                    {
                        Console.WriteLine($"Valid part found for number {number} at the bottom side.");
                        m_sumPartNumbers += number;
                        return;
                    }
                }
            }
            catch
            {
                // ignored
            }

            //Bottom left corner
            try
            {
                if (m_specialChar.IsMatch(engineSchematic[index - 1, i + 1]))
                {
                    Console.WriteLine($"Valid part found for number {number} at the bottom left corner.");
                    m_sumPartNumbers += number;
                    return;
                }
            }
            catch
            {
                // ignored
            }


            // Left side
            try
            {
                if (m_specialChar.IsMatch(engineSchematic[index - 1, i]))
                {
                    Console.WriteLine($"Valid part found for number {number} at the left side.");
                    m_sumPartNumbers += number;
                    return;
                }
            }
            catch
            {
                // ignored
            }

            //Top left corner
            try
            {
                if (m_specialChar.IsMatch(engineSchematic[index - 1, i - 1]))
                {
                    Console.WriteLine($"Valid part found for number {number} at the top right corner.");
                    m_sumPartNumbers += number;
                    return;
                }
            }
            catch
            {
                // ignored
            }

            // Top side
            try
            {
                for (int j = 0; j < length; j++)
                {
                    if (m_specialChar.IsMatch(engineSchematic[index + j, i - 1].ToCharArray()))
                    {
                        Console.WriteLine($"Valid part found for number {number} at the top side.");
                        m_sumPartNumbers += number;
                        return;
                    }
                }
            }
            catch
            {
                // ignored
            }

            //Top right corner
            try
            {
                if (m_specialChar.IsMatch(engineSchematic[index + length, i - 1]))
                {
                    Console.WriteLine($"Valid part found for number {number} at the top right corner.");
                    m_sumPartNumbers += number;
                    return;
                }
            }
            catch
            {
                // ignored
            }

        }

        private static string[,] GetSchematic(string[] input)
        {
            var schematic = new string[input[0].Length, input.Length];

            for (int i = 0; i < schematic.GetLength(0); i++)
            {
                for (int j = 0; j < schematic.GetLength(1); j++)
                {
                    schematic[i, j] = input[j][i].ToString();
                }
            }

            return schematic;
        }
    }
}
