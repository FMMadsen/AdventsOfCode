

namespace AdventOfCode2023Solutions.Day05
{
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
}
