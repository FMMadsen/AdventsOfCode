using Common;
using System.Linq;

namespace AdventOfCode2023Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: Almanac";


        public string SolvePart1(string[] DatasetLines)
        {
            List<SeedModel> seeds = new();
            int lineNumber = 0;
            AlmanacFirst almanacTable = new AlmanacFirst();

            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            string[] seedLine = DatasetLines[lineNumber].Split(':');
            if ("seeds" != seedLine[0]) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not seeds line."); }
            almanacTable.Seeds = seedLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s) ).ToList();
            lineNumber++;

            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("seed-to-soil")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not seed-to-soil map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.SeedToSoil.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }

            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("soil-to-fertilizer")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not soil-to-fertilizer map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.SoilToFertilizer.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("fertilizer-to-water")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not fertilizer-to-water map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.FertilizerToWater.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("water-to-light")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not water-to-light map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.WaterToLight.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("light-to-temperature")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not light-to-temperature map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.LightToTemperature.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("temperature-to-humidity")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not temperature-to-humidity map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.TemperatureToHumidity.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            if (!DatasetLines[lineNumber].StartsWith("humidity-to-location")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not humidity-to-location map."); }
            for (lineNumber++; lineNumber < DatasetLines.Length; lineNumber++)
            {
                if ("" == DatasetLines[lineNumber].Trim()) { break; }
                almanacTable.HumidityToLocation.Add(new AlmanacItem(DatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }

            UInt32 soil;
            UInt32 fertilizer;
            UInt32 water;
            UInt32 light;
            UInt32 temperature;
            UInt32 humidity;
            UInt32 location;

            foreach (UInt32 seed in almanacTable.Seeds)
            {
                soil = almanacTable.SeedToSoil.Where(a => a.Source <= seed && seed < a.Source + a.Range).Select(a => a.Destination + seed - a.Source).FirstOrDefault(seed);
                fertilizer = almanacTable.SoilToFertilizer.Where(a => a.Source <= soil && soil < a.Source + a.Range).Select(a => a.Destination + soil - a.Source).FirstOrDefault(soil);
                water = almanacTable.FertilizerToWater.Where(a => a.Source <= fertilizer && fertilizer < a.Source + a.Range).Select(a => a.Destination + fertilizer - a.Source).FirstOrDefault(fertilizer);
                light = almanacTable.WaterToLight.Where(a => a.Source <= water && water < a.Source + a.Range).Select(a => a.Destination + water - a.Source).FirstOrDefault(water);
                temperature = almanacTable.LightToTemperature.Where(a => a.Source <= light && light < a.Source + a.Range).Select(a => a.Destination + light - a.Source).FirstOrDefault(light);
                humidity = almanacTable.TemperatureToHumidity.Where(a => a.Source <= temperature && temperature < a.Source + a.Range).Select(a => a.Destination + temperature - a.Source).FirstOrDefault(temperature);
                location = almanacTable.HumidityToLocation.Where(a => a.Source <= humidity && humidity < a.Source + a.Range).Select(a => a.Destination + humidity - a.Source).FirstOrDefault(humidity);

                seeds.Add(new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location });
            }

            SeedModel? t = seeds.OrderBy(s => s.Location).First();
            almanacTable.Clear();
            seeds.Clear();
            return t.Location.ToString() ?? "";
        }

        public string SolvePart2(string[] datasetLines)
        {
            AlmanacTable alamanac = new AlmanacTable(datasetLines);

            SeedModel? seed = null;
            for (uint iLocation = 0; iLocation < uint.MaxValue; iLocation++)
            {
                seed = alamanac.GetSeedFromLocation(iLocation);
                if(null != seed) { break; }
            }

            if (null == seed) { throw new Exception("No seed with a location found"); }

            return seed.Location.ToString();
        }

       
        private int SkipEmpty(int currentLine, string[] datasetLines)
        {
            while ("" == datasetLines[currentLine].Trim()) { currentLine++; }
            return currentLine;
        }
    }
}
