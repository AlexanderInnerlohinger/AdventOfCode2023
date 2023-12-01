using System.Text.RegularExpressions;

namespace AdventOfCode2023
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
                string spelledWithLettersZero = Regex.Replace(calibrationDocumentLine, "(zero)", "0");
                string spelledWithLettersOne = Regex.Replace(spelledWithLettersZero, "(one)", "1");
                string spelledWithLettersTwo = Regex.Replace(spelledWithLettersOne, "(two)", "2");
                string spelledWithLettersThree = Regex.Replace(spelledWithLettersTwo, "(three)", "3");
                string spelledWithLettersFour = Regex.Replace(spelledWithLettersThree, "(four)", "4");
                string spelledWithLettersFive = Regex.Replace(spelledWithLettersFour, "(five)", "5");
                string spelledWithLettersSix = Regex.Replace(spelledWithLettersFive, "(six)", "6");
                string spelledWithLettersSeven = Regex.Replace(spelledWithLettersSix, "(seven)", "7");
                string spelledWithLettersEight = Regex.Replace(spelledWithLettersSeven, "(eight)", "8");
                string spelledWithLettersNine = Regex.Replace(spelledWithLettersEight, "(nine)", "9");

                string calibrationValues = Regex.Replace(spelledWithLettersNine, @"[\D-]", string.Empty);

                if (calibrationValues.Length == 1)
                    calibrationValues = calibrationValues.Insert(1, calibrationValues);

                string firstDigit = calibrationValues.Substring(0, 1);
                string lastDigit = calibrationValues.Substring(calibrationValues.Length - 1, 1);
                string twoDigitNumber = firstDigit + lastDigit;

                calibrationValue += Convert.ToInt32(twoDigitNumber);
            }

            Console.WriteLine("Calibration value: " + calibrationValue);
        }
    }
}
