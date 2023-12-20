using Common;
using System.Linq;

namespace AdventOfCode2023Solutions.Day05
{
    public class AlmanacItem
    {
        public long Destination;
        public long Source;
        public long Range;

        public AlmanacItem()
        {
            
        }

        public AlmanacItem(long[] dataset) 
        {
            Destination = dataset[0];
            Source = dataset[1];
            Range = dataset[2];
        }
    }

    public class SeedModel
    {
        public long Seed { get; set; }
        public long Soil { get; set; }
        public long Fertilizer { get; set; }
        public long Water { get; set; }
        public long Light { get; set; }
        public long Temperature { get; set; }
        public long Humidity { get; set; }
        public long Location { get; set; }

    }

    public class Almanac
    {
        public List<long> Seeds = new();
        public List<AlmanacItem> SeedToSoil = new();
        public List<AlmanacItem> SoilToFertilizer = new();
        public List<AlmanacItem> FertilizerToWater = new();
        public List<AlmanacItem> WaterToLight = new();
        public List<AlmanacItem> LightToTemperature = new();
        public List<AlmanacItem> TemperatureToHumidity = new();
        public List<AlmanacItem> HumidityToLocation = new();

        public Almanac(in string[] datasetLines) 
        {
            int lineNumber = 0;

            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            string[] seedLine = datasetLines[lineNumber].Split(':');
            if("seeds" != seedLine[0]) { throw new Exception("DatasetLines error in line "+lineNumber.ToString()+". Not seeds line."); }
            Seeds = seedLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToList();
            lineNumber++;

            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if ( !datasetLines[lineNumber].StartsWith("seed-to-soil") ) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not seed-to-soil map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                var t = datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray();
                SeedToSoil.Add( new AlmanacItem( t ));
            }

            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if (!datasetLines[lineNumber].StartsWith("soil-to-fertilizer")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not soil-to-fertilizer map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                SoilToFertilizer.Add( new AlmanacItem(datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray()) );
            }


            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if (!datasetLines[lineNumber].StartsWith("fertilizer-to-water")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not fertilizer-to-water map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                FertilizerToWater.Add(new AlmanacItem(datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if (!datasetLines[lineNumber].StartsWith("water-to-light")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not water-to-light map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                WaterToLight.Add(new AlmanacItem(datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if (!datasetLines[lineNumber].StartsWith("light-to-temperature")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not light-to-temperature map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                LightToTemperature.Add(new AlmanacItem(datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if (!datasetLines[lineNumber].StartsWith("temperature-to-humidity")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not temperature-to-humidity map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                TemperatureToHumidity.Add(new AlmanacItem(datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray()));
            }


            lineNumber = SkipEmpty(lineNumber, in datasetLines);
            if (!datasetLines[lineNumber].StartsWith("humidity-to-location")) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not humidity-to-location map."); }
            for (lineNumber++; lineNumber < datasetLines.Length; lineNumber++)
            {
                if ("" == datasetLines[lineNumber].Trim()) { break; }
                HumidityToLocation.Add(new AlmanacItem(datasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => long.Parse(l)).ToArray()));
            }

        }

        private int SkipEmpty(int currentLine, in string[] datasetLines)
        {
            while ("" == datasetLines[currentLine].Trim()) { currentLine++; }
            return currentLine;
        }
    }

    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: Almanac";

        private List<SeedModel> Seeds = new();

        public Solution(string[] datasetLines)
        {
            Almanac almanacTable = new Almanac(in datasetLines);
            long soil;
            long fertilizer;
            long water;
            long light;
            long temperature;
            long humidity;
            long location;

            foreach(long seed in almanacTable.Seeds)
            {
                soil = almanacTable.SeedToSoil.Where(a => a.Source <= seed && seed < a.Source + a.Range).Select(a => a.Destination + seed - a.Source).FirstOrDefault(seed);
                fertilizer = almanacTable.SoilToFertilizer.Where(a => a.Source <= soil && soil < a.Source + a.Range).Select(a => a.Destination + soil - a.Source).FirstOrDefault(soil);
                water = almanacTable.FertilizerToWater.Where(a => a.Source <= fertilizer && fertilizer < a.Source + a.Range).Select(a => a.Destination + fertilizer - a.Source).FirstOrDefault(fertilizer);
                light = almanacTable.WaterToLight.Where(a => a.Source <= water && water < a.Source + a.Range).Select(a => a.Destination + water - a.Source).FirstOrDefault(water);
                temperature = almanacTable.LightToTemperature.Where(a => a.Source <= light && light < a.Source + a.Range).Select(a => a.Destination + light - a.Source).FirstOrDefault(light);
                humidity = almanacTable.TemperatureToHumidity.Where(a => a.Source <= temperature && temperature < a.Source + a.Range).Select(a => a.Destination + temperature - a.Source).FirstOrDefault(temperature);
                location = almanacTable.HumidityToLocation.Where(a => a.Source <= humidity && humidity < a.Source + a.Range).Select(a => a.Destination + humidity - a.Source).FirstOrDefault(humidity);

                Seeds.Add(new SeedModel() { Seed=seed,Soil=soil,Fertilizer=fertilizer,Water=water,Light=light,Temperature=temperature,Humidity=humidity,Location=location});
            }
        }

        public string SolvePart1()
        {
            SeedModel? t = Seeds.OrderBy(s => s.Location).First();
            return t.Location.ToString() ?? "";
        }

        public string SolvePart2()
        {
            return "To be implemented";
        }

    }
}
