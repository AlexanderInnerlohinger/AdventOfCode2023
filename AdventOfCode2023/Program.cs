﻿using AdventOfCode2023.Days;

namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose function:\n");
            Console.WriteLine("(01) - Trebuchet?!");
            Console.WriteLine("(02) - Cube Conundrum");
            Console.WriteLine("(03) - Gear Ratios");
            Console.WriteLine("(04) - Scratchcards");
            Console.WriteLine("(05) - Fertilizer");
            Console.WriteLine("(06) - Toy Boats");
            Console.WriteLine("(07) - Camel Cards");
            Console.WriteLine("(08) - Haunted Wasteland");
            Console.WriteLine("(09) - Mirage Maintenance");
            Console.WriteLine("(10) - PipeMaze");
            Console.WriteLine("(11) - Cosmic Expansion\n");
            Console.WriteLine("(00) - Exit\n\n");
            Console.WriteLine("\n");

            ConsoleKeyInfo key1Pressed = Console.ReadKey();
            ConsoleKeyInfo key2Pressed = Console.ReadKey();

            string keyInputsString = key1Pressed.KeyChar + key2Pressed.KeyChar.ToString();
            int keyInputs = Convert.ToInt16(keyInputsString);

            switch (keyInputs)
            {
                case 0:
                    Console.Clear();
                    break;
                case 1:
                    Console.Clear();
                    Trebuchet.Run();
                    break;
                case 2:
                    Console.Clear();
                    CubeConundrum.Run();
                    break;
                case 3:
                    Console.Clear();
                    GearRatios.Run();
                    break;
                case 4:
                    Console.Clear();
                    Scratchcards.Run();
                    break;
                case 5:
                    Console.Clear();
                    Fertilizer.Run();
                    break;
                case 6:
                    Console.Clear();
                    ToyBoats.Run();
                    break;
                case 7:
                    Console.Clear();
                    CamelCards.Run();
                    break;
                case 8:
                    Console.Clear();
                    HauntedWasteland.Run();
                    break;
                case 9:
                    Console.Clear();
                    MirageMaintenance.Run();
                    break;
                case 10:
                    Console.Clear();
                    PipeMaze.Run();
                    break;
                case 11:
                    Console.Clear();
                    CosmicExpansion.Run();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Unknown function !\n\n");
                    Main(Array.Empty<string>());
                    break;
            }

            Console.Read();
        }
    }
}