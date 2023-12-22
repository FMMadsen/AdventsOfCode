namespace AdventOfCode2023Solutions.Day11
{
    public class GalaxyRoute(Galaxy galaxyA, Galaxy galaxyB)
    {
        public Galaxy GalaxyA { get; set; } = galaxyA;
        public Galaxy GalaxyB { get; set; } = galaxyB;

        public long CalculateDistance()
        {
            var distance = Math.Abs(GalaxyA.expX - GalaxyB.expX) + Math.Abs(GalaxyA.expY - GalaxyB.expY);
            return distance;
        }
    }
}
