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

        public uint SeedLength { get { return Seed.Select(a => a.Source + a.Range).OrderDescending().FirstOrDefault((uint)0); } }
        public uint LocationLength 
        { 
            get 
            {
                List<uint> list = new List<uint>();
                list.Add(SeedLength);
                list.Add(SeedToSoil.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0) );
                list.Add(SoilToFertilizer.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0));
                list.Add(FertilizerToWater.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0));
                list.Add(WaterToLight.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0));
                list.Add(LightToTemperature.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0));
                list.Add(TemperatureToHumidity.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0));
                list.Add(HumidityToLocation.Select(a => a.Destination + a.Range).OrderDescending().FirstOrDefault((uint)0));
                return list.OrderDescending().First();
            } 
        }

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

        public SeedModel GetSeed(UInt32 seedNumber)
        {
            /*
            UInt32 soil = SeedToSoil.Where(a => a.Source <= seedNumber && seedNumber < a.Source + a.Range).Select(a => a.Destination + seedNumber - a.Source).FirstOrDefault(seedNumber);
            UInt32 fertilizer = SoilToFertilizer.Where(a => a.Source <= soil && soil < a.Source + a.Range).Select(a => a.Destination + soil - a.Source).FirstOrDefault(soil);
            UInt32 water = FertilizerToWater.Where(a => a.Source <= fertilizer && fertilizer < a.Source + a.Range).Select(a => a.Destination + fertilizer - a.Source).FirstOrDefault(fertilizer);
            UInt32 light = WaterToLight.Where(a => a.Source <= water && water < a.Source + a.Range).Select(a => a.Destination + water - a.Source).FirstOrDefault(water);
            UInt32 temperature = LightToTemperature.Where(a => a.Source <= light && light < a.Source + a.Range).Select(a => a.Destination + light - a.Source).FirstOrDefault(light);
            UInt32 humidity = TemperatureToHumidity.Where(a => a.Source <= temperature && temperature < a.Source + a.Range).Select(a => a.Destination + temperature - a.Source).FirstOrDefault(temperature);
            UInt32 location = HumidityToLocation.Where(a => a.Source <= humidity && humidity < a.Source + a.Range).Select(a => a.Destination + humidity - a.Source).FirstOrDefault(humidity);
            */

            UInt32 soil = GetDestination(SeedToSoil, seedNumber);
            UInt32 fertilizer = GetDestination(SoilToFertilizer, soil);
            UInt32 water = GetDestination(FertilizerToWater, fertilizer);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            return new SeedModel() { Seed = seedNumber, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };
              
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

        public SeedModel GetClosestLocationSeed()
        {
            uint seedMax = SeedLength;
            //uint locationMax = LocationLength;
            if (UInt32.MaxValue < SeedLength) { throw new Exception("Too many seeds"); }

            SeedModel closestSeed = GetSeed(Seed[0].Source);
            SeedModel tempSeed = new SeedModel( closestSeed );

            
            for (uint seedNumber = 0; seedNumber < seedMax; seedNumber++)
            {
                
                if (0 == seedNumber % 10000000) 
                {
                    Console.Write("\rReached seed: " + (seedNumber * 0.000001).ToString() + " mil.  "); 
                }
                if (!Array.Exists(Seed, a => a.Source <= seedNumber && seedNumber < a.Source + a.Range)) { continue; }

                //tempSeed = GetSeed(seedNumber);

                tempSeed.Seed = seedNumber;
                tempSeed.Soil = GetDestination(SeedToSoil, tempSeed.Seed);
                tempSeed.Fertilizer = GetDestination(SoilToFertilizer, tempSeed.Soil);
                tempSeed.Water = GetDestination(FertilizerToWater, tempSeed.Fertilizer);
                tempSeed.Light = GetDestination(WaterToLight, tempSeed.Water);
                tempSeed.Temperature = GetDestination(LightToTemperature, tempSeed.Light);
                tempSeed.Humidity = GetDestination(TemperatureToHumidity, tempSeed.Temperature);
                tempSeed.Location = GetDestination(HumidityToLocation, tempSeed.Humidity);


                if (tempSeed.Location < closestSeed.Location)
                {
                    closestSeed = new SeedModel( tempSeed );
                }
            }
            
            /*
            for (uint locationNumber = 0; locationNumber < locationMax; locationNumber++)
            {
                if (!Array.Exists(Seed, a => a.Source <= seedNumber && seedNumber < a.Source + a.Range)) { continue; }
            }
            */
            return closestSeed;
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

    }

    public class AlmanacOld
    {
        public List<SeedModel> Seeds = new();
        public List<AlmanacItem> SeedToSoil = new();
        public List<AlmanacItem> SoilToFertilizer = new();
        public List<AlmanacItem> FertilizerToWater = new();
        public List<AlmanacItem> WaterToLight = new();
        public List<AlmanacItem> LightToTemperature = new();
        public List<AlmanacItem> TemperatureToHumidity = new();
        public List<AlmanacItem> HumidityToLocation = new();

        public AlmanacOld() 
        {
            

        }

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

            return alamanac.GetClosestLocationSeed().Location.ToString();
        }

        private string Part2Old()
        {
            int lineNumber = 0;
            AlmanacOld almanacTable = new AlmanacOld();

            lineNumber = SkipEmpty(lineNumber, DatasetLines);
            string[] seedLine = DatasetLines[lineNumber].Split(':');
            if ("seeds" != seedLine[0]) { throw new Exception("DatasetLines error in line " + lineNumber.ToString() + ". Not seeds line."); }
            UInt32[] seedRange = seedLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => uint.Parse(s)).ToArray();

            for (int i = 0; i < seedRange.Length; i++)
            {
                UInt32 max = (UInt32)seedRange[i] + seedRange[i + 1];
                for (UInt32 min = seedRange[i]; min < max; min++)
                {
                    almanacTable.Seeds.Add(new SeedModel() { Seed = min });
                }
                i++;
            }
            seedRange = new UInt32[0];
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


            int seedsAmount = almanacTable.Seeds.Count;

            for (int seedsIndex = 0; seedsIndex < seedsAmount; seedsIndex++)
            {
                almanacTable.Seeds[seedsIndex].Soil = almanacTable.SeedToSoil.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Seed && almanacTable.Seeds[seedsIndex].Seed < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Seed - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Seed);
                almanacTable.Seeds[seedsIndex].Fertilizer = almanacTable.SoilToFertilizer.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Soil && almanacTable.Seeds[seedsIndex].Soil < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Soil - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Soil);
                almanacTable.Seeds[seedsIndex].Water = almanacTable.FertilizerToWater.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Fertilizer && almanacTable.Seeds[seedsIndex].Fertilizer < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Fertilizer - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Fertilizer);
                almanacTable.Seeds[seedsIndex].Light = almanacTable.WaterToLight.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Water && almanacTable.Seeds[seedsIndex].Water < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Water - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Water);
                almanacTable.Seeds[seedsIndex].Temperature = almanacTable.LightToTemperature.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Light && almanacTable.Seeds[seedsIndex].Light < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Light - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Light);
                almanacTable.Seeds[seedsIndex].Humidity = almanacTable.TemperatureToHumidity.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Temperature && almanacTable.Seeds[seedsIndex].Temperature < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Temperature - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Temperature);
                almanacTable.Seeds[seedsIndex].Location = almanacTable.HumidityToLocation.Where(a => a.Source <= almanacTable.Seeds[seedsIndex].Humidity && almanacTable.Seeds[seedsIndex].Humidity < a.Source + a.Range).Select(a => a.Destination + almanacTable.Seeds[seedsIndex].Humidity - a.Source).FirstOrDefault(almanacTable.Seeds[seedsIndex].Humidity);

            }

            SeedModel? t = almanacTable.Seeds.OrderBy(s => s.Location).First();
            almanacTable.Clear();
            return t.Location.ToString() ?? "";
        }

        private int SkipEmpty(int currentLine, string[] datasetLines)
        {
            while ("" == datasetLines[currentLine].Trim()) { currentLine++; }
            return currentLine;
        }
    }
}
