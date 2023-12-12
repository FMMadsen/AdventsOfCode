using Common;

namespace AdventOfCode2023Solutions.Day07
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 7: Camel Cards";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            CamelCards game = new();
            game.DealCards(datasetLines);
            var sumOfBets = game.DetermineRankAndCalculateWinnings();
            return sumOfBets.ToString();
        }

        public string SolvePart2()
        {
            CamelCards game = new(IncludeJokerRule: true);
            game.DealCards(datasetLines);
            var sumOfBets = game.DetermineRankAndCalculateWinnings();

            //PrintPart2(game);
            Console.WriteLine("Part 2: WRONG ANSWER!");

            return sumOfBets.ToString();
        }

        private static void PrintPart2(CamelCards game)
        {
            var noOfHands = game.Hands.Count();
            long sumWinnings = 0;
            for (int i = 0; i < noOfHands; i++)
            {
                var hand = game.Hands[i];
                sumWinnings += hand.WinAmount;
                if (hand.HandType != hand.HandTypeJokerized)
                    Console.WriteLine($"{hand.HandTypeJokerized,-11} {hand.HandString} - Win: {hand.Bet,-4} * {hand.Rank,-4} = {hand.WinAmount,-6} Σ {sumWinnings} <<== {hand.HandType.ToString()}=>{hand.HandStringJokerized}");
                else
                    Console.WriteLine($"{hand.HandType,-11} {hand.HandString} - Win: {hand.Bet,-4} * {hand.Rank,-4} = {hand.WinAmount,-6} Σ {sumWinnings}");
            }
        }
    }
}
