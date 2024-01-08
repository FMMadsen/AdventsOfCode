using Common;
using System.Linq;

namespace AdventOfCode2023Solutions.Day05
{
    public class AlmanacItem
    {
        public UInt32 Destination;
        public UInt32 Source;
        public UInt32 Range;

        public AlmanacItem()
        {
            
        }

        public AlmanacItem(UInt32[] dataset) 
        {
            Destination = dataset[0];
            Source = dataset[1];
            Range = dataset[2];
        }
    }

    public class SeedModel
    {
        public UInt32 Seed { get; set; }
        public UInt32 Soil { get; set; }
        public UInt32 Fertilizer { get; set; }
        public UInt32 Water { get; set; }
        public UInt32 Light { get; set; }
        public UInt32 Temperature { get; set; }
        public UInt32 Humidity { get; set; }
        public UInt32 Location { get; set; }

        public SeedModel() { }

        public SeedModel(SeedModel model)
        {
            Seed = model.Seed;
            Soil = model.Soil;
            Fertilizer = model.Fertilizer;
            Water = model.Water;
            Light = model.Light;
            Temperature = model.Temperature;
            Humidity = model.Humidity;
            Location = model.Location;
        }
    }

    public class AlmanacList
    {
        public AlmanacItem[] Items { get; set; } = new AlmanacItem[0];
        public uint NextIndex = 0;
    }

    public class AlmanacFirst
    {
        public List<long> Seeds = new();
        public List<AlmanacItem> SeedToSoil = new();
        public List<AlmanacItem> SoilToFertilizer = new();
        public List<AlmanacItem> FertilizerToWater = new();
        public List<AlmanacItem> WaterToLight = new();
        public List<AlmanacItem> LightToTemperature = new();
        public List<AlmanacItem> TemperatureToHumidity = new();
        public List<AlmanacItem> HumidityToLocation = new();


        public void Clear()
        {
            Seeds.Clear();
            SeedToSoil.Clear();
            SoilToFertilizer.Clear();
            FertilizerToWater.Clear();
            WaterToLight.Clear();
            LightToTemperature.Clear();
            TemperatureToHumidity.Clear();
            HumidityToLocation.Clear();
        }

    }

    public class AlmanacTable
    {
        public AlmanacItem[] Seed = new AlmanacItem[0];
        public AlmanacItem[] SeedToSoil = new AlmanacItem[0];
        public AlmanacItem[] SoilToFertilizer = new AlmanacItem[0];
        public AlmanacItem[] FertilizerToWater = new AlmanacItem[0];
        public AlmanacItem[] WaterToLight = new AlmanacItem[0];
        public AlmanacItem[] LightToTemperature = new AlmanacItem[0];
        public AlmanacItem[] TemperatureToHumidity = new AlmanacItem[0];
        public AlmanacItem[] HumidityToLocation = new AlmanacItem[0];

        public AlmanacTable(string[] almanacDatasetLines)
        {
            int lineNumber = 0;
            List<AlmanacItem> tempList = new List<AlmanacItem>();

            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            string[] seedLine = almanacDatasetLines[lineNumber].Split(':');
            if ("seeds" != seedLine[0]) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not seeds line."); }
            UInt32[] seedRange = seedLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => uint.Parse(s)).ToArray();

            for (int i = 0; i < seedRange.Length; i++)
            {
                tempList.Add(new AlmanacItem() { Source = seedRange[i], Range = seedRange[i + 1], Destination = 0 });
                i++;
            }
            Seed = tempList.OrderBy(a => a.Source).ToArray();
            seedRange = new UInt32[0];
            lineNumber++;

            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("seed-to-soil")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not seed-to-soil map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            SeedToSoil = tempList.OrderBy(a => a.Source).ToArray();

            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("soil-to-fertilizer")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not soil-to-fertilizer map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            SoilToFertilizer = tempList.OrderBy(a => a.Source).ToArray();


            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("fertilizer-to-water")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not fertilizer-to-water map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            FertilizerToWater = tempList.OrderBy(a => a.Source).ToArray();


            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("water-to-light")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not water-to-light map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            WaterToLight = tempList.OrderBy(a => a.Source).ToArray();


            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("light-to-temperature")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not light-to-temperature map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            LightToTemperature = tempList.OrderBy(a => a.Source).ToArray();


            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("temperature-to-humidity")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not temperature-to-humidity map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            TemperatureToHumidity = tempList.OrderBy(a => a.Source).ToArray();


            lineNumber = SkipEmpty(lineNumber, almanacDatasetLines);
            if (!almanacDatasetLines[lineNumber].StartsWith("humidity-to-location")) { throw new Exception("datasetLines error in line " + lineNumber.ToString() + ". Not humidity-to-location map."); }
            tempList = new List<AlmanacItem>();
            for (lineNumber++; lineNumber < almanacDatasetLines.Length; lineNumber++)
            {
                if ("" == almanacDatasetLines[lineNumber].Trim()) { break; }
                tempList.Add(new AlmanacItem(almanacDatasetLines[lineNumber].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => UInt32.Parse(l)).ToArray()));
            }
            HumidityToLocation = tempList.OrderBy(a => a.Source).ToArray();
        }

        public SeedModel GetSeedFromSeed(UInt32 seedNumber)
        {
            UInt32 soil = GetDestination(SeedToSoil, seedNumber);
            UInt32 fertilizer = GetDestination(SoilToFertilizer, soil);
            UInt32 water = GetDestination(FertilizerToWater, fertilizer);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seedNumber, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };
              
        }
        public SeedModel GetSeedFromSoil(UInt32 soilNumber)
        {
            UInt32 seed = GetSource(SeedToSoil, soilNumber);

            UInt32 fertilizer = GetDestination(SoilToFertilizer, soilNumber);
            UInt32 water = GetDestination(FertilizerToWater, fertilizer);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seed, Soil = soilNumber, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel GetSeedFromFertilizer(UInt32 fertilizerNumber)
        {
            UInt32 soil = GetSource(SoilToFertilizer, fertilizerNumber);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 water = GetDestination(FertilizerToWater, fertilizerNumber);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizerNumber, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel GetSeedFromWater(UInt32 waterNumber)
        {
            UInt32 fertilizer = GetSource(FertilizerToWater, waterNumber);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);
            
            UInt32 light = GetDestination(WaterToLight, waterNumber);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = waterNumber, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel GetSeedFromLight(UInt32 lightNumber)
        {
            UInt32 water = GetSource(WaterToLight, lightNumber);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 temperature = GetDestination(LightToTemperature, lightNumber);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = lightNumber, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel GetSeedFromTemperature(UInt32 temperatureNumber)
        {
            UInt32 light = GetSource(LightToTemperature, temperatureNumber);
            UInt32 water = GetSource(WaterToLight, light);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 humidity = GetDestination(TemperatureToHumidity, temperatureNumber);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperatureNumber, Humidity = humidity, Location = location };

        }
        public SeedModel GetSeedFromHumidity(UInt32 humidityNumber)
        {
            UInt32 temperature = GetSource(TemperatureToHumidity, humidityNumber);
            UInt32 light = GetSource(LightToTemperature, temperature);
            UInt32 water = GetSource(WaterToLight, light);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 location = GetDestination(HumidityToLocation, humidityNumber);

            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidityNumber, Location = location };

        }
        public SeedModel GetSeedFromLocation(UInt32 locationNumber)
        {
            UInt32 humidity = GetSource(HumidityToLocation, locationNumber);
            UInt32 temperature = GetSource(TemperatureToHumidity, humidity);
            UInt32 light = GetSource(LightToTemperature, temperature);
            UInt32 water = GetSource(WaterToLight, light);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);
            
            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = locationNumber };

        }

        private uint GetDestination(AlmanacItem[] items, UInt32 value)
        {
            uint destination = value;

            foreach (AlmanacItem item in items)
            {
                if (item.Source <= value && value < item.Source + item.Range) 
                {
                    destination = item.Destination + value - item.Source;
                    break;
                }
            }

            return destination;
        }

        private uint GetSource(AlmanacItem[] items, UInt32 value)
        {
            uint source = value;

            foreach (AlmanacItem item in items)
            {
                if (item.Destination <= value && value < item.Destination + item.Range)
                {
                    source = item.Source + value - item.Destination;
                    break;
                }
            }

            return source;
        }

        public void Clear()
        {
            Array.Clear(Seed);
            Array.Clear(SeedToSoil);
            Array.Clear(SoilToFertilizer);
            Array.Clear(FertilizerToWater);
            Array.Clear(WaterToLight);
            Array.Clear(LightToTemperature);
            Array.Clear(TemperatureToHumidity);
            Array.Clear(HumidityToLocation);
        }

        private int SkipEmpty(int currentLine, string[] datasetLines)
        {
            while ("" == datasetLines[currentLine].Trim()) { currentLine++; }
            return currentLine;
        }

        public bool IsSeed(UInt32 seedNumber)
        {
            foreach(AlmanacItem item in Seed)
            {
                if (item.Source <= seedNumber && seedNumber < item.Source + item.Range)
                {
                    return true;
                }
            }
            
            return false;
        }

    }

    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: Almanac";

        private String[] DatasetLines;

        public Solution(string[] datasetLines)
        {
            DatasetLines = datasetLines;
            
            
        }

        public string SolvePart1()
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

        public string SolvePart2()
        {
            AlmanacTable alamanac = new AlmanacTable(DatasetLines);

            SeedModel? seed = null;
            for (uint iLocation = 0; iLocation < uint.MaxValue; iLocation++)
            {
                seed = alamanac.GetSeedFromLocation(iLocation);
                if (alamanac.IsSeed(seed.Seed)) { break; }
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
