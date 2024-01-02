namespace AdventOfCode2023Solutions.Day05
{
    using System.Diagnostics;

    internal class Almanac
    {
        private readonly IList<long> seedInputList = new List<long>();
        private readonly List<SeedRange> seedRanges = new List<SeedRange>();
        private readonly AlmanacMap seedToSoilMap = new AlmanacMap();
        private readonly AlmanacMap soilToFertilizerMap = new AlmanacMap();
        private readonly AlmanacMap fertilizerToWaterMap = new AlmanacMap();
        private readonly AlmanacMap waterToLightMap = new AlmanacMap();
        private readonly AlmanacMap lightToTemperatureMap = new AlmanacMap();
        private readonly AlmanacMap temperatureToHumidityMap = new AlmanacMap();
        private readonly AlmanacMap humidityToLocationMap = new AlmanacMap();
        private readonly AlmanacMap newSeed2LocationMap = new AlmanacMap();
        internal long LowestLocationNumber { get; private set; } = long.MaxValue;

        internal void Load(string[] inputLines)
        {
            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i].Contains("seeds"))
                    LoadSeedNumbers(inputLines, i);

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

        private void LoadSeedNumbers(string[] inputLines, long index)
        {
            var seedStringLine = inputLines[index];

            if (seedStringLine.Contains("seeds:"))
            {
                var seedStrings = seedStringLine.Substring(6).Trim().Split(' ');
                foreach (var seedString in seedStrings)
                {
                    seedInputList.Add(long.Parse(seedString));
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

        internal void CreateSeedToLocationMap()
        {
            humidityToLocationMap.ExpandMapToIncludeZeroToMax();
            temperatureToHumidityMap.ExpandMapToIncludeZeroToMax();
            lightToTemperatureMap.ExpandMapToIncludeZeroToMax();
            waterToLightMap.ExpandMapToIncludeZeroToMax();
            fertilizerToWaterMap.ExpandMapToIncludeZeroToMax();
            soilToFertilizerMap.ExpandMapToIncludeZeroToMax();
            seedToSoilMap.ExpandMapToIncludeZeroToMax();

            humidityToLocationMap.ExpandMapToFillInGapInRuleRanges();
            temperatureToHumidityMap.ExpandMapToFillInGapInRuleRanges();
            lightToTemperatureMap.ExpandMapToFillInGapInRuleRanges();
            waterToLightMap.ExpandMapToFillInGapInRuleRanges();
            fertilizerToWaterMap.ExpandMapToFillInGapInRuleRanges();
            soilToFertilizerMap.ExpandMapToFillInGapInRuleRanges();
            seedToSoilMap.ExpandMapToFillInGapInRuleRanges();

            temperatureToHumidityMap.ExpandMapToAlignRangesOnRightHandSideMap(humidityToLocationMap.MappingRules);
            lightToTemperatureMap.ExpandMapToAlignRangesOnRightHandSideMap(temperatureToHumidityMap.MappingRules);
            waterToLightMap.ExpandMapToAlignRangesOnRightHandSideMap(lightToTemperatureMap.MappingRules);
            fertilizerToWaterMap.ExpandMapToAlignRangesOnRightHandSideMap(waterToLightMap.MappingRules);
            soilToFertilizerMap.ExpandMapToAlignRangesOnRightHandSideMap(fertilizerToWaterMap.MappingRules);
            seedToSoilMap.ExpandMapToAlignRangesOnRightHandSideMap(soilToFertilizerMap.MappingRules);

            var seedToLocationRules = new List<MappingRule>();
            foreach (MappingRule rule in seedToSoilMap.MappingRules)
            {
                var sourceRangeFrom = rule.SourceRangeFrom;
                var sourceRangeTo = rule.SourceRangeTo;
                var targetRangeFrom = GetPlantLocationFromSeedNoWholeProcess(sourceRangeFrom);
                var targetRangeTo = GetPlantLocationFromSeedNoWholeProcess(sourceRangeTo);
                if ((sourceRangeTo - sourceRangeFrom) != (targetRangeTo - targetRangeFrom))
                    throw new Exception("Modifyer not the same in from and to");

                var newRule = new MappingRule(sourceRangeFrom, sourceRangeTo, targetRangeFrom, targetRangeTo);
                seedToLocationRules.Add(newRule);
            }
            newSeed2LocationMap.InitializeMap(seedToLocationRules);
            newSeed2LocationMap.SortMapByTargetRange();
        }

        internal void GetLowestLocationNumberPart1Version1()
        {
            LowestLocationNumber = seedInputList.Select(i => GetPlantLocationFromSeedNoWholeProcess(i)).Min();
        }

        internal void GetLowestLocationNumberPart1Version2()
        {
            LowestLocationNumber = seedInputList.Select(i => GetPlantLocationFromSeedNoShortcut(i)).Min();
        }

        internal void GetLowestLocationNumberPart1Version3()
        {
            for (int i = 0; i < newSeed2LocationMap.MappingRules.Count; i++)
            {
                var rangeFrom = newSeed2LocationMap.MappingRules[i].SourceRangeFrom;
                var rangeTo = newSeed2LocationMap.MappingRules[i].SourceRangeTo;

                var seedsInRange = seedInputList.Where(s => s >= rangeFrom && s <= rangeTo);
                if (seedsInRange != null && seedsInRange.Any())
                {
                    var seed = seedsInRange.Min();
                    LowestLocationNumber = newSeed2LocationMap.Map(seed);
                    return;
                }
            }
            throw new Exception("No seeds where in any of the mapping rule ranges");
        }

        internal void Part2CreateSeedRanges()
        {
            for (int i = 0; i < seedInputList.Count; i += 2)
                seedRanges.Add(new SeedRange(seedInputList[i], seedInputList[i + 1]));
        }

        internal void GetLowestLocationNumberPart2Version1()
        {
            Stopwatch swMidtimer = new();
            Stopwatch swTotaltimer = new();
            swMidtimer.Start();
            swTotaltimer.Start();
            long countTarget = CountNumberOfSeedsPart2();
            long progresCounter = 0;

            foreach (var seedRange in seedRanges)
            {
                for (long seedNo = seedRange.From; seedNo <= seedRange.To; seedNo++)
                {
                    var plantLocation = GetPlantLocationFromSeedNoWholeProcess(seedNo);
                    if (plantLocation < LowestLocationNumber)
                        LowestLocationNumber = plantLocation;

                    progresCounter++;
                    if (progresCounter % 1000000 == 0)
                    {
                        PrintProgress(progresCounter, countTarget, swMidtimer, swTotaltimer);
                        swMidtimer.Restart();
                    }
                }
            }

            swMidtimer.Stop();
            swTotaltimer.Stop();
            PrintProgress(progresCounter, countTarget, swMidtimer, swTotaltimer);
        }

        internal void GetLowestLocationNumberPart2Version2()
        {
            Stopwatch swMidtimer = new();
            Stopwatch swTotaltimer = new();
            swMidtimer.Start();
            swTotaltimer.Start();
            long countTarget = CountNumberOfSeedsPart2();
            long progresCounter = 0;

            foreach (var seedRange in seedRanges)
            {
                for (long seedNo = seedRange.From; seedNo <= seedRange.To; seedNo++)
                {
                    var plantLocation = GetPlantLocationFromSeedNoShortcut(seedNo);
                    if (plantLocation < LowestLocationNumber)
                        LowestLocationNumber = plantLocation;

                    progresCounter++;
                    if (progresCounter % 1000000 == 0)
                    {
                        PrintProgress(progresCounter, countTarget, swMidtimer, swTotaltimer);
                        swMidtimer.Restart();
                    }
                }
            }
            swMidtimer.Stop();
            swTotaltimer.Stop();
            PrintProgress(progresCounter, countTarget, swMidtimer, swTotaltimer);
        }


        private static void PrintProgress(long progressCounter, long countTarget, Stopwatch swMidtimer, Stopwatch swTotaltimer)
        {
            Console.WriteLine($"{(long)(swTotaltimer.ElapsedMilliseconds / 1000 / 60)}min : Progress:{Math.Round(((double)progressCounter / (double)countTarget * 100), 2)}% ({progressCounter:#,##0}/{countTarget:#,##0}) - parttimer: {(long)swMidtimer.ElapsedMilliseconds / 1000}sec");
        }

        internal long CountNumberOfSeedsPart2()
        {
            var seedArray = seedInputList.ToArray();

            long sum = 0;

            for (int i = 0; i < seedArray.Length; i += 2)
            {
                var seedRange = seedArray[i + 1];
                sum += seedRange;
            }

            return sum;
        }

        internal void GetLowestLocationNumberPart2Version3()
        {
            //Go through all the map rules from seed to location, ordered after higest priority ranges first (means lowest location number)
            for (int i = 0; i < newSeed2LocationMap.MappingRules.Count; i++)
            {
                var mapRangeFrom = newSeed2LocationMap.MappingRules[i].SourceRangeFrom;
                var mapRangeTo = newSeed2LocationMap.MappingRules[i].SourceRangeTo;

                var overlappingSeedRanges = seedRanges.Where(s => (s.From >= mapRangeFrom && s.From <= mapRangeTo) || (s.To >= mapRangeFrom && s.From <= mapRangeTo));
                if (overlappingSeedRanges != null && overlappingSeedRanges.Any())
                {
                    var lowestOverlappingSeedRangeFrom = overlappingSeedRanges.Min(s => s.From);
                    if (lowestOverlappingSeedRangeFrom <= mapRangeFrom)
                    {
                        LowestLocationNumber = newSeed2LocationMap.Map(mapRangeFrom);
                        return;
                    }
                    else
                    {
                        LowestLocationNumber = newSeed2LocationMap.Map(lowestOverlappingSeedRangeFrom);
                        return;
                    }
                }
            }
            throw new Exception("No seeds where in any of the mapping rule ranges");
        }

        private long GetPlantLocationFromSeedNoWholeProcess(long plantSeed)
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

        private long GetPlantLocationFromSeedNoShortcut(long plantSeed)
        {
            return newSeed2LocationMap.Map(plantSeed);
        }
    }
}
