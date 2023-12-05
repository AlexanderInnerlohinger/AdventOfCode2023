using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    internal class Trebuchet
    {
        public static void Run()
        {
            Console.WriteLine("Trebuchet?!-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_1_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);

            int calibrationValue = 0;
            foreach (string calibrationDocumentLine in input)
            {
                string[] calibrationValuesLine = new string[calibrationDocumentLine.Length];

                MatchCollection matchesLetters = Regex.Matches(calibrationDocumentLine, @"(one|two|three|four|five|six|seven|eight|nine|zero)");
                MatchCollection matchesDigits = Regex.Matches(calibrationDocumentLine, @"1|2|3|4|5|6|7|8|9|0");

                // Fill array with letters and digits
                foreach (Match matchesLetter in matchesLetters)
                    calibrationValuesLine[matchesLetter.Index] = matchesLetter.Value;

                foreach (Match matchesDigit in matchesDigits)
                    calibrationValuesLine[matchesDigit.Index] = matchesDigit.Value;

                // Create new array without null values
                string[] calibrationValuesWithoutNull =
                    calibrationValuesLine.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                // Convert each number written as string in an integer
                for (int i = 0; i < calibrationValuesWithoutNull.Length; i++)
                    calibrationValuesWithoutNull[i] = ConvertToNumber(calibrationValuesWithoutNull[i]);

                // Finally take the first and last number and combine them to a two digit number
                string firstDigit = calibrationValuesWithoutNull[0];
                string lastDigit = calibrationValuesWithoutNull[^1];
                string twoDigitNumber = firstDigit + lastDigit;

                calibrationValue += Convert.ToInt32(twoDigitNumber);
            }

            Console.WriteLine("Calibration value: " + calibrationValue);
        }

        private static string ConvertToNumber(string v)
        {
            switch (v)
            {
                case "zero":
                    return "0";
                case "one":
                    return "1";
                case "two":
                    return "2";
                case "three":
                    return "3";
                case "four":
                    return "4";
                case "five":
                    return "5";
                case "six":
                    return "6";
                case "seven":
                    return "7";
                case "eight":
                    return "8";
                case "nine":
                    return "9";
                default:
                    // String must already be a number
                    return v;
            }
        }
    }
}
