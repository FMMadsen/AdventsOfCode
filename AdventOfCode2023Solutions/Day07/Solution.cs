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
            game.OrderCardsAfterStrength();
            var sumOfBets = game.CalculateTotalWinningsPart1();
            return sumOfBets.ToString();
        }

        public string SolvePart2()
        {
            CamelCards game = new(IncludeJokerRule: true);
            game.DealCards(datasetLines);
            game.OrderCardsAfterStrength();
            var sumOfBets = game.CalculateTotalWinningsPart1();

            //foreach(var hand in game.Hands)
            //{
            //    if (hand.CardString.Contains("J"))
            //        Console.WriteLine($"Cards: {hand.CardString} Hand type: {hand.HandType.ToString()}");
            //}


            return sumOfBets.ToString();
        }
    }
}
