namespace AdventOfCode2023Solutions.Day07
{
    internal class CamelCards(bool IncludeJokerRule = false)
    {
        internal List<HandV2> Hands { get; set; } = [];

        internal void DealCards(string[] cardLines)
        {
            foreach (var datasetLine in cardLines)
            {
                var handAndBetPair = datasetLine.Split(" ");
                var handCards = handAndBetPair[0];
                var bet = long.Parse(handAndBetPair[1]);
                Hands.Add(new HandV2(handCards, bet, IncludeJokerRule));
            }
        }

        internal long DetermineRankAndCalculateWinnings()
        {
            Hands.Sort();
            long winningsSum = 0;

            for (int i = 0; i < Hands.Count; i++)
            {
                Hands[i].SetRankAndCalculateWinning(i+1);
                winningsSum += Hands[i].WinAmount;
            }
            return winningsSum;
        }
    }

    internal class HighStrengthFirstComparer : Comparer<HandV1>
    {
        public override int Compare(HandV1? x, HandV1? y)
        {
            if ((x?.Strength ?? 0) > (y?.Strength ?? 0))
                return 1;
            if ((x?.Strength ?? 0) < (y?.Strength ?? 0))
                return -1;
            return 0;
        }
    }
}
