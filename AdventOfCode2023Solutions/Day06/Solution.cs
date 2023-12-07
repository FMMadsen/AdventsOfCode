using Common;

namespace AdventOfCode2023Solutions.Day06
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 6: Wait For It";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var boatRaceList = new BoatRacesList();
            boatRaceList.LoadRaceListPart1(datasetLines);
            var winMargin = boatRaceList.CalculateWinMarginPart1();
            return winMargin.ToString();
        }

        public string SolvePart2()
        {
            var boatRaceList = new BoatRacesList();
            var winSolutions = boatRaceList.CalculateWinSolutionsForSingleRacePart2(datasetLines);
            return winSolutions.ToString();
        }
    }
}
