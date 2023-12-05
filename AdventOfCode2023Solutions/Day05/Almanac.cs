namespace AdventOfCode2023Solutions.Day05
{
    internal class Almanac
    {
        private readonly IList<long> seedList = new List<long>();
        private readonly AlmanacMap seedToSoilMap = new AlmanacMap();
        private readonly AlmanacMap soilToFertilizerMap = new AlmanacMap();
        private readonly AlmanacMap fertilizerToWaterMap = new AlmanacMap();
        private readonly AlmanacMap waterToLightMap = new AlmanacMap();
        private readonly AlmanacMap lightToTemperatureMap = new AlmanacMap();
        private readonly AlmanacMap temperatureToHumidityMap = new AlmanacMap();
        private readonly AlmanacMap humidityToLocationMap = new AlmanacMap();

        internal IEnumerable<PlantInstruction> GetPlantInstructionsPart1()
        {
            return seedList.Select(i => GetPlantInstruction(i));
        }

        internal IEnumerable<PlantInstruction> GetPlantInstructionsPart2()
        {
            var seedArray = seedList.ToArray();

            var plantInstructions = new List<PlantInstruction>();

            for (int i = 0; i < seedArray.Length; i += 2)
            {
                var seedBegin = seedArray[i];
                var seedRange = seedArray[i + 1];

                for (long j = seedBegin; j < seedBegin + seedRange; j++)
                {
                    plantInstructions.Add(GetPlantInstruction(j));
                }

            }

            return plantInstructions;
        }

        internal PlantInstruction GetPlantInstruction(long seedNo)
        {
            var plantInst = new PlantInstruction();
            plantInst.PlantSeed = seedNo;
            plantInst.PlantLocation = GetPlantLocation(seedNo);
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

        private void LoadSeeds(string[] inputLines, long index)
        {
            var seedStringLine = inputLines[index];

            if (seedStringLine.Contains("seeds:"))
            {
                var seedStrings = seedStringLine.Substring(6).Trim().Split(' ');
                foreach (var seedString in seedStrings)
                {
                    seedList.Add(long.Parse(seedString));
                }
            }
        }

        private void InitializeMap(AlmanacMap map, string[] inputLines, int startIndex)
        {
            var index1 = startIndex + 1;
            var index2 = startIndex + 1;
            for (int i = index1; i < inputLines.Length; i++)
            {
                var isBlankLine = string.IsNullOrWhiteSpace(inputLines[i]);
                var isLastLine = i == inputLines.Length - 1;

                if (isBlankLine)
                {
                    index2 = i;
                    break;
                }
                else if (isLastLine)
                {
                    index2 = i + 1;
                }
            }
            var mapLines = inputLines.Take(new Range(index1, index2));
            map.InitializeMap(mapLines);
        }

        private long GetPlantLocation(long plantSeed)
        {
            var soil = seedToSoilMap.Map(plantSeed);
            var fertilizer = soilToFertilizerMap.Map(soil);
            var water = fertilizerToWaterMap.Map(fertilizer);
            var light = waterToLightMap.Map(water);
            var temp = lightToTemperatureMap.Map(light);
            var humid = temperatureToHumidityMap.Map(temp);
            var location = humidityToLocationMap.Map(humid);
            return location;
        }
    }
}
