namespace AdventOfCode2023Solutions.Day07
{
    internal class CamelCards(bool IncludeJokerRule = false)
    {
        internal List<Hand> Hands { get; set; } = new List<Hand>();

        internal void DealCards(string[] cardLines)
        {
            foreach (var datasetLine in cardLines)
            {
                var handAndBetPair = datasetLine.Split(" ");
                var handCards = handAndBetPair[0];
                var bet = long.Parse(handAndBetPair[1]);
                Hands.Add(new Hand(handCards, bet, IncludeJokerRule));
            }
        }

        internal void OrderCardsAfterStrength()
        {
            Hands.Sort(new HighStrengthFirstComparer());
        }

        internal long CalculateTotalWinningsPart1()
        {
            long rank;
            long winningsSum = 0;
            for (int i = 0; i < Hands.Count(); i++)
            {
                rank = i + 1;
                winningsSum += Hands[i].Bet * rank;
            }
            return winningsSum;
        }
    }

    internal class HighStrengthFirstComparer : Comparer<Hand>
    {
        public override int Compare(Hand? x, Hand? y)
        {
            if ((x?.Strength ?? 0) > (y?.Strength ?? 0))
                return 1;
            if ((x?.Strength ?? 0) < (y?.Strength ?? 0))
                return -1;
            return 0;
        }
    }
}
