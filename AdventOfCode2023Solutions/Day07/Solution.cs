using Common;

namespace AdventOfCode2023Solutions.Day07
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 7: Camel Cards";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var hands = new List<Hand>();
            foreach (var datasetLine in DatasetLines)
            {
                var handAndBetPair = datasetLine.Split(" ");
                var handCards = handAndBetPair[0];
                var bet = long.Parse(handAndBetPair[1]);
                hands.Add(new Hand(handCards, bet));
            }
            var sumOfBets = hands.Sum(h => h.Bet);
            return sumOfBets.ToString();
        }

        public string SolvePart2()
        {
            return "To be implemented";
        }
    }
}
