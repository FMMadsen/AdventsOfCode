namespace AdventOfCode2023Solutions.Day06
{
    internal class BoatRacesList
    {
        private IList<BoatRace> boatRaces = new List<BoatRace>();

        internal void LoadRaceListPart1(string[] raceList)
        {
            var timeArray = raceList[0][5..].Split(" ").Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => int.Parse(i)).ToArray();
            var distanceArray = raceList[1][9..].Split(" ").Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => int.Parse(i)).ToArray();

            for (int i = 0; i < timeArray.Length; i++)
            {
                boatRaces.Add(new BoatRace(timeArray[i], distanceArray[i]));
            }
        }

        internal long CalculateWinMarginPart1()
        {
            long winMargin = 1;

            foreach (var race in boatRaces)
            {
                winMargin *= race.CalculateNumberOfWinSolutions();
            }
            return winMargin;
        }

        internal long CalculateWinSolutionsForSingleRacePart2(string[] raceList)
        {
            var time = long.Parse(raceList[0][5..].Replace(" ", ""));
            var distance = long.Parse(raceList[1][9..].Replace(" ", ""));
            var singleBoatRace = new BoatRace(time, distance);
            return singleBoatRace.CalculateNumberOfWinSolutions();
        }
    }
}
