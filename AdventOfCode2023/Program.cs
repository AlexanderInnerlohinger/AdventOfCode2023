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
            Console.WriteLine("(04) - Scratchcards\n");
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