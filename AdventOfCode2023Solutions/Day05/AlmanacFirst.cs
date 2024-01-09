

namespace AdventOfCode2023Solutions.Day05
{
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

}
