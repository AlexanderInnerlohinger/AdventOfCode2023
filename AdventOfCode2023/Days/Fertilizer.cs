using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    internal class Fertilizer
    {
        #region Fields

        private static List<string> seedToSoilMaps = new();
        private static List<string> soilToFertilzerMaps = new();
        private static List<string> fertilizerToWaterMaps = new();
        private static List<string> waterToLightMaps = new();
        private static List<string> lightToTemperatureMaps = new();
        private static List<string> temperatureToHumidityMaps = new();
        private static List<string> humidityToLocationMaps = new();

        private static Dictionary<int, int> seedToSoilDictionary = new();
        private static Dictionary<int, int> soilToFertilizerDictionary = new();
        private static Dictionary<int, int> fertilizerToWaterDictionary = new();
        private static Dictionary<int, int> waterToLightDictionary = new();
        private static Dictionary<int, int> lightToTemperatureDictionary = new();
        private static Dictionary<int, int> temperatureToHumidityDictionary = new();
        private static Dictionary<int, int> humidityToLocationDictionary = new();

        private static int lowestLocationNumber = int.MaxValue;

        #endregion

        public static void Run()
        {
            Console.WriteLine("Fertilizer-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_5_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);


            string[] seeds = input[0].Split(':')[1].Split(' ');
            uint[] seedsWithoutNullOrEmpty = Array.ConvertAll(seeds.Where(x => !string.IsNullOrEmpty(x)).ToArray(), uint.Parse);


            int seedToSoilIndex = Array.FindIndex(input, SeedToSoilPredicate);
            int soilToFertilizerIndex = Array.FindIndex(input, SoilToFertilizerPredicate);
            int fertilizerToWater = Array.FindIndex(input, FertilizerToWaterPredicate);
            int waterToLight = Array.FindIndex(input, WaterToLightPredicate);
            int lightToTemperature = Array.FindIndex(input, LightToTemperaturePredicate);
            int temperatureToHumidity = Array.FindIndex(input, TemperatureToHumidityPredicate);
            int humidityToLocation = Array.FindIndex(input, HumidityToLocationPredicate);

            for (int i = seedToSoilIndex + 1; i < soilToFertilizerIndex - 1; i++)
                seedToSoilMaps.Add(input[i]);

            for (int i = soilToFertilizerIndex + 1; i < fertilizerToWater - 1; i++)
                soilToFertilzerMaps.Add(input[i]);

            for (int i = fertilizerToWater + 1; i < waterToLight - 1; i++)
                fertilizerToWaterMaps.Add(input[i]);

            for (int i = waterToLight + 1; i < lightToTemperature - 1; i++)
                waterToLightMaps.Add(input[i]);

            for (int i = lightToTemperature + 1; i < temperatureToHumidity - 1; i++)
                lightToTemperatureMaps.Add(input[i]);

            for (int i = temperatureToHumidity + 1; i < humidityToLocation - 1; i++)
                temperatureToHumidityMaps.Add(input[i]);

            for (int i = humidityToLocation + 1; i < input.Length; i++)
                humidityToLocationMaps.Add(input[i]);

            // This method is way too resource inefficient because a map for the whole input is generated
            //GenerateSeedToSoilMap();
            //GenerateSoilToFertilizerMap();
            //GenerateFertilizerToWaterMap();
            //GenerateWaterToLightMap();
            //GenerateLightToTemperatureMap();
            //GenerateTemperatureToHumidityMap();
            //GenerateHumidityToLocation();


            foreach (uint seed in seedsWithoutNullOrEmpty)
            {
                List<uint> availableSources = new List<uint>();
                uint relevantSource = 0;

                for (int i = 0; i < seedToSoilMaps.Count; i++)
                {
                    uint[] map = Array.ConvertAll(seedToSoilMaps[i].Split(' '), uint.Parse);
                    availableSources.Add(map[1]);
                }
                availableSources = availableSources.Order().ToList();


                foreach (uint availableSource in availableSources)
                {
                    if (seed > availableSource && relevantSource < availableSource)
                        relevantSource = availableSource;
                }


            }














            foreach (int seed in seedsWithoutNullOrEmpty)
            {
                int soil = ApplySoilMap(seed);
                Console.WriteLine($"Seed {seed} mapped to soil {soil}.");

                int fertilizer = ApplyFertilizerMap(soil);
                Console.WriteLine($"Soil {soil} mapped to fertilizer {fertilizer}.");

                int water = ApplyWaterMap(fertilizer);
                Console.WriteLine($"Fertilizer {fertilizer} mapped to water {water}.");

                int light = ApplyLightMap(water);
                Console.WriteLine($"Water {water} mapped to light {light}.");

                int temperature = ApplyTemperatureMap(light);
                Console.WriteLine($"Light {light} mapped to temperature {temperature}.");

                int humidity = ApplyHumidityMap(temperature);
                Console.WriteLine($"Temperature {temperature} mapped to humidity {humidity}.");

                int location = ApplyLocationMap(humidity);
                Console.WriteLine($"Humidity {humidity} mapped to location {location}.");

                Console.WriteLine();

                if (location < lowestLocationNumber)
                    lowestLocationNumber = location;
            }

            Console.WriteLine($"The lowest location seed is {lowestLocationNumber}.");
        }

        #region Generate maps

        private static void GenerateHumidityToLocation()
        {
            foreach (string humidityToLocationMap in humidityToLocationMaps)
            {
                int[] map = Array.ConvertAll(humidityToLocationMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    humidityToLocationDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            humidityToLocationDictionary = humidityToLocationDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private static void GenerateTemperatureToHumidityMap()
        {
            foreach (string temperatureToHumidityMap in temperatureToHumidityMaps)
            {
                int[] map = Array.ConvertAll(temperatureToHumidityMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    temperatureToHumidityDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            temperatureToHumidityDictionary = temperatureToHumidityDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private static void GenerateLightToTemperatureMap()
        {
            foreach (string lightToTemperatureMap in lightToTemperatureMaps)
            {
                int[] map = Array.ConvertAll(lightToTemperatureMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    lightToTemperatureDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            lightToTemperatureDictionary = lightToTemperatureDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private static void GenerateWaterToLightMap()
        {
            foreach (string waterToLightMap in waterToLightMaps)
            {
                int[] map = Array.ConvertAll(waterToLightMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    waterToLightDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            waterToLightDictionary = waterToLightDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private static void GenerateFertilizerToWaterMap()
        {
            foreach (string fertilizerToWaterMap in fertilizerToWaterMaps)
            {
                int[] map = Array.ConvertAll(fertilizerToWaterMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    fertilizerToWaterDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            fertilizerToWaterDictionary = fertilizerToWaterDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private static void GenerateSoilToFertilizerMap()
        {
            foreach (string soilToFertilzerMap in soilToFertilzerMaps)
            {
                int[] map = Array.ConvertAll(soilToFertilzerMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    soilToFertilizerDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            soilToFertilizerDictionary = soilToFertilizerDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        private static void GenerateSeedToSoilMap()
        {
            foreach (string seedToSoilMap in seedToSoilMaps)
            {
                int[] map = Array.ConvertAll(seedToSoilMap.Split(' '), int.Parse);

                int destinationRangeStart = map[0];
                int sourceRangeStart = map[1];
                int rangeLength = map[2];

                for (int i = 0; i < rangeLength; i++)
                    seedToSoilDictionary.Add(sourceRangeStart + i, destinationRangeStart + i);
            }

            seedToSoilDictionary = seedToSoilDictionary.OrderBy(obj => obj.Key)
                .ToDictionary(obj => obj.Key, obj => obj.Value);
        }

        #endregion

        #region Apply maps

        private static int ApplySoilMap(int seed)
        {
            if (seedToSoilDictionary.TryGetValue(seed, out var mappedSoil))
                return mappedSoil;

            return seed;
        }

        private static int ApplyLocationMap(int humidity)
        {
            if (humidityToLocationDictionary.TryGetValue(humidity, out var mappedLocation))
                return mappedLocation;

            return humidity;
        }

        private static int ApplyHumidityMap(int temperature)
        {
            if (temperatureToHumidityDictionary.TryGetValue(temperature, out var mappedHumidity))
                return mappedHumidity;

            return temperature;
        }

        private static int ApplyTemperatureMap(int light)
        {
            if (lightToTemperatureDictionary.TryGetValue(light, out var mappedTemperature))
                return mappedTemperature;

            return light;
        }

        private static int ApplyLightMap(int water)
        {
            if (waterToLightDictionary.TryGetValue(water, out var mappedLight))
                return mappedLight;

            return water;
        }

        private static int ApplyWaterMap(int fertilizer)
        {
            if (fertilizerToWaterDictionary.TryGetValue(fertilizer, out var mappedWater))
                return mappedWater;

            return fertilizer;
        }

        private static int ApplyFertilizerMap(int soil)
        {
            if (soilToFertilizerDictionary.TryGetValue(soil, out var mappedFertilizer))
                return mappedFertilizer;

            return soil;
        }

        #endregion

        #region Predicates

        private static bool HumidityToLocationPredicate(string obj)
        {
            if (obj == "humidity-to-location map:")
                return true;

            return false;
        }

        private static bool TemperatureToHumidityPredicate(string obj)
        {
            if (obj == "temperature-to-humidity map:")
                return true;

            return false;
        }

        private static bool LightToTemperaturePredicate(string obj)
        {
            if (obj == "light-to-temperature map:")
                return true;

            return false;
        }

        private static bool WaterToLightPredicate(string obj)
        {
            if (obj == "water-to-light map:")
                return true;

            return false;
        }

        private static bool FertilizerToWaterPredicate(string obj)
        {
            if (obj == "fertilizer-to-water map:")
                return true;

            return false;
        }

        private static bool SoilToFertilizerPredicate(string obj)
        {
            if (obj == "soil-to-fertilizer map:")
                return true;

            return false;
        }

        private static bool SeedToSoilPredicate(string obj)
        {
            if (obj == "seed-to-soil map:")
                return true;

            return false;
        }

        #endregion
    }
}
