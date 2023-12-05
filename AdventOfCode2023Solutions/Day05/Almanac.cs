using System.Security.Cryptography;

namespace AdventOfCode2023Solutions.Day05
{
    internal class Almanac
    {
        private IList<int> seed = new List<int>();
        private readonly AlmanacMap seedToSoilMap = new AlmanacMap();
        private readonly AlmanacMap soilToFertilizerMap = new AlmanacMap();
        private readonly AlmanacMap fertilizerToWaterMap = new AlmanacMap();
        private readonly AlmanacMap waterToLightMap = new AlmanacMap();
        private readonly AlmanacMap lightToTemperatureMap = new AlmanacMap();
        private readonly AlmanacMap temperatureToHumidityMap = new AlmanacMap();
        private readonly AlmanacMap humidityToLocationMap = new AlmanacMap();

        internal IEnumerable<PlantInstruction> GetPlantInstructions()
        {
            return seed.Select(i => GetPlantInstruction(i));
        }

        internal PlantInstruction GetPlantInstruction(int seedNo)
        {
            var plantInst = new PlantInstruction();
            plantInst.PlantSeed = seedNo;
            plantInst.PlantLocation = RandomNumberGenerator.GetInt32(1, 99);
            return plantInst;
        }

        internal void Load(string[] inputLines)
        {
            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i].Contains("seeds"))
                    LoadSeeds(inputLines, i);

                else if (inputLines[i].Contains("seed-to-soil"))
                    InitializeMap(seedToSoilMap, inputLines, i);

                else if (inputLines[i].Contains("soil-to-fertilizer"))
                    InitializeMap(soilToFertilizerMap, inputLines, i);

                else if (inputLines[i].Contains("fertilizer-to-water"))
                    InitializeMap(fertilizerToWaterMap, inputLines, i);

                else if (inputLines[i].Contains("water-to-light"))
                    InitializeMap(waterToLightMap, inputLines, i);

                else if (inputLines[i].Contains("light-to-temperature"))
                    InitializeMap(lightToTemperatureMap, inputLines, i);

                else if (inputLines[i].Contains("temperature-to-humidity"))
                    InitializeMap(temperatureToHumidityMap, inputLines, i);

                else if (inputLines[i].Contains("humidity-to-location"))
                    InitializeMap(humidityToLocationMap, inputLines, i);
            }
        }

        private void LoadSeeds(string[] inputLines, int index)
        {
            var seedStringLine = inputLines[index];

            if (seedStringLine.Contains("seeds:"))
            {
                var seedStrings = seedStringLine.Substring(6).Trim().Split(' ');
                foreach (var seedString in seedStrings)
                {
                    seed.Add(int.Parse(seedString));
                }
            }
        }

        private void InitializeMap(AlmanacMap map, string[] inputLines, int startIndex)
        {
            int index1 = startIndex + 1;
            int index2 = startIndex + 1;
            for (int i = index1; i < inputLines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(inputLines[i]))
                {
                    index2 = i - 1;
                    break;
                }
            }
            map.InitializeMap(inputLines.Take(new Range(index1, index2)));
        }

        //private void LoadSeedToSoilMap(string[] inputLines, int index)
        //{
        //    map.InitializeMap(inputLines.Take(new Range(index, index + 2)));
        //}

        //private void LoadSoilToFertilizerMap(string[] inputLines, int index)
        //{
        //    soilToFertilizerMap.InitializeMap(inputLines.Take(new Range(index, index + 2)));
        //}

        //private void LoadFertilizerToWaterMap(string[] inputLines, int index)
        //{
        //    fertilizerToWaterMap.InitializeMap(inputLines.Take(new Range(index, index + 2)));
        //}

        //private void LoadWaterToLightMap(string[] inputLines, int index)
        //{
        //    waterToLightMap
        //}

        //private bool LoadLightToTemperatureMap(string[] inputLines, int index)
        //{
        //    return false;
        //}

        //private void LoadTemperatureToHumidityMap(string[] inputLines, int index)
        //{
        //    return false;
        //}

        //private void LoadHumidityToLocationMap(string[] inputLines, int index)
        //{
        //    return false;
        //}
    }
}
