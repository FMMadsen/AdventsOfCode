using Common;

namespace AdventOfCode2023Solutions.Day06
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 6: Wait For It";

        public string SolvePart1(string[] datasetLines)
        {
            var boatRaceList = new BoatRacesList();
            boatRaceList.LoadRaceListPart1(datasetLines);
            var winMargin = boatRaceList.CalculateWinMarginPart1();
            return winMargin.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var boatRaceList = new BoatRacesList();
            var winSolutions = boatRaceList.CalculateWinSolutionsForSingleRacePart2(datasetLines);
            return winSolutions.ToString();
        }
    }
}
