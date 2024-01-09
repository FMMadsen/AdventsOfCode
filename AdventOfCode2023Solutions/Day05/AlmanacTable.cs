

namespace AdventOfCode2023Solutions.Day05
{
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

        public SeedModel? GetSeedFromSeed(UInt32 seedNumber)
        {
            UInt32 soil = GetDestination(SeedToSoil, seedNumber);
            UInt32 fertilizer = GetDestination(SoilToFertilizer, soil);
            UInt32 water = GetDestination(FertilizerToWater, fertilizer);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            if (!IsSeed(seedNumber)) { return null; }
            return new SeedModel() { Seed = seedNumber, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel? GetSeedFromSoil(UInt32 soilNumber)
        {
            UInt32 seed = GetSource(SeedToSoil, soilNumber);

            UInt32 fertilizer = GetDestination(SoilToFertilizer, soilNumber);
            UInt32 water = GetDestination(FertilizerToWater, fertilizer);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            if (!IsSeed(seed)) { return null; }
            return new SeedModel() { Seed = seed, Soil = soilNumber, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel? GetSeedFromFertilizer(UInt32 fertilizerNumber)
        {
            UInt32 soil = GetSource(SoilToFertilizer, fertilizerNumber);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 water = GetDestination(FertilizerToWater, fertilizerNumber);
            UInt32 light = GetDestination(WaterToLight, water);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            if (!IsSeed(seed)) { return null; }
            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizerNumber, Water = water, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel? GetSeedFromWater(UInt32 waterNumber)
        {
            UInt32 fertilizer = GetSource(FertilizerToWater, waterNumber);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 light = GetDestination(WaterToLight, waterNumber);
            UInt32 temperature = GetDestination(LightToTemperature, light);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            if (!IsSeed(seed)) { return null; }
            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = waterNumber, Light = light, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel? GetSeedFromLight(UInt32 lightNumber)
        {
            UInt32 water = GetSource(WaterToLight, lightNumber);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 temperature = GetDestination(LightToTemperature, lightNumber);
            UInt32 humidity = GetDestination(TemperatureToHumidity, temperature);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            if (!IsSeed(seed)) { return null; }
            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = lightNumber, Temperature = temperature, Humidity = humidity, Location = location };

        }
        public SeedModel? GetSeedFromTemperature(UInt32 temperatureNumber)
        {
            UInt32 light = GetSource(LightToTemperature, temperatureNumber);
            UInt32 water = GetSource(WaterToLight, light);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 humidity = GetDestination(TemperatureToHumidity, temperatureNumber);
            UInt32 location = GetDestination(HumidityToLocation, humidity);

            if (!IsSeed(seed)) { return null; }
            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperatureNumber, Humidity = humidity, Location = location };

        }
        public SeedModel? GetSeedFromHumidity(UInt32 humidityNumber)
        {
            UInt32 temperature = GetSource(TemperatureToHumidity, humidityNumber);
            UInt32 light = GetSource(LightToTemperature, temperature);
            UInt32 water = GetSource(WaterToLight, light);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            UInt32 location = GetDestination(HumidityToLocation, humidityNumber);

            if (!IsSeed(seed)) { return null; }
            return new SeedModel() { Seed = seed, Soil = soil, Fertilizer = fertilizer, Water = water, Light = light, Temperature = temperature, Humidity = humidityNumber, Location = location };

        }
        public SeedModel? GetSeedFromLocation(UInt32 locationNumber)
        {
            UInt32 humidity = GetSource(HumidityToLocation, locationNumber);
            UInt32 temperature = GetSource(TemperatureToHumidity, humidity);
            UInt32 light = GetSource(LightToTemperature, temperature);
            UInt32 water = GetSource(WaterToLight, light);
            UInt32 fertilizer = GetSource(FertilizerToWater, water);
            UInt32 soil = GetSource(SoilToFertilizer, fertilizer);
            UInt32 seed = GetSource(SeedToSoil, soil);

            if (!IsSeed(seed)) { return null; }
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
            foreach (AlmanacItem item in Seed)
            {
                if (item.Source <= seedNumber && seedNumber < item.Source + item.Range)
                {
                    return true;
                }
            }

            return false;
        }

    }

}
