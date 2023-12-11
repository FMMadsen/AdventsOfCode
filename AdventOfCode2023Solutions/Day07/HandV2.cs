namespace AdventOfCode2023Solutions.Day07
{
    internal class HandV2 : IComparable<HandV2>
    {
        public string HandString { get; private set; }
        public long Bet { get; private set; }
        public HandTypes HandType { get; private set; }
        public HandTypes HandTypeJokerized { get; private set; }
        public string HandStringJokerized { get; private set; }
        public bool IncludeJokerRule { get; private set; }
        public long Rank { get; private set; }
        public long WinAmount { get; private set; }

        public HandV2(string handString, long bet, bool includeJokerRule = false)
        {
            HandString = handString;
            Bet = bet;
            IncludeJokerRule = includeJokerRule;
            Rank = 0;

            var cardsGroupedAndCounted = GroupAndCountCards(handString);
            HandType = DetermineHandType(cardsGroupedAndCounted);

            JokerMagic(handString, HandType, out string handStringJokerized, out HandTypes handTypeJokerized);
            HandStringJokerized = handStringJokerized;
            HandTypeJokerized = handTypeJokerized;
        }

        public void SetRankAndCalculateWinning(long rank)
        {
            Rank = rank;
            WinAmount = rank * Bet;
        }

        public static HandTypes DetermineHandType(Dictionary<char, int> cardDict)
        {
            var numberOfKindOfCards = cardDict.Count;

            HandTypes handType;
            switch (numberOfKindOfCards)
            {
                case 1:
                    handType = HandTypes.FiveOfAKind;
                    break;
                case 2:
                    var contains4SimilarCards = cardDict.ContainsValue(4);
                    handType = contains4SimilarCards ? HandTypes.FourOfAKind : HandTypes.FullHouse;
                    break;
                case 3:
                    var contains3SimilarCards = cardDict.ContainsValue(3);
                    handType = contains3SimilarCards ? HandTypes.ThreeOfAKind : HandTypes.TwoPair;
                    break;
                case 4:
                    handType = HandTypes.OnePair;
                    break;
                default:
                    handType = HandTypes.HighCard;
                    break;
            }
            return handType;
        }

        public static Dictionary<char, int> GroupAndCountCards(string handString)
        {
            var cardDict = new Dictionary<char, int>();

            foreach (char card in handString)
            {
                if (cardDict.ContainsKey(card))
                    cardDict[card]++;
                else
                    cardDict.Add(card, 1);
            }
            return cardDict;

        }

        public static bool JokerMagic(string handString, HandTypes handType, out string handStringJokerized, out HandTypes handTypeJokerized)
        {
            handTypeJokerized = handType;

            var JokerCount = handString.Count(c => c == 'J');

            if (JokerCount == 0 || JokerCount == 3 || JokerCount == 4 || JokerCount == 5)
                handTypeJokerized = handType;

            if (JokerCount == 2 && handType == HandTypes.OnePair)
                handTypeJokerized = HandTypes.ThreeOfAKind;

            if (JokerCount == 2 && handType == HandTypes.TwoPair)
                handTypeJokerized = HandTypes.FourOfAKind;

            if (JokerCount == 2 && handType == HandTypes.FullHouse)
                handTypeJokerized = HandTypes.FiveOfAKind;

            if (JokerCount == 1)
            {
                if (handType == HandTypes.HighCard)
                    handTypeJokerized = HandTypes.OnePair;

                else if (handType == HandTypes.OnePair)
                    handTypeJokerized = HandTypes.ThreeOfAKind;

                else if (handType == HandTypes.TwoPair)
                    handTypeJokerized = HandTypes.FullHouse;

                else if (handType == HandTypes.ThreeOfAKind)
                    handTypeJokerized = HandTypes.FourOfAKind;

                else if (handType == HandTypes.FourOfAKind)
                    handTypeJokerized = HandTypes.FiveOfAKind;
            }

            if (handType != handTypeJokerized)
            {
                var mostFrequentCard = handString.Replace("J", "").GroupBy(c => c).MaxBy(g => g.Count()).Key;
                handStringJokerized = handString.Replace('J', mostFrequentCard);
                return true;
            }
            else
            {
                handStringJokerized = handString;
                return false;
            }
        }

        public int CompareTo(HandV2? other)
        {
            //Sorting priority 1
            if (other == null) return 1;

            //Sorting priority 2
            if (IncludeJokerRule)
            {
                if (this.HandTypeJokerized != other.HandTypeJokerized)
                    return this.HandTypeJokerized.CompareTo(other.HandTypeJokerized);
            }
            else
            {
                if (this.HandType != other.HandType)
                    return this.HandType.CompareTo(other.HandType);
            }

            //Sorting priority 3
            var cards = this.HandString.ToArray();
            var otherCards = other.HandString.ToArray();

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != otherCards[i])
                {
                    var cardValue = GetCardValue(cards[i], this.IncludeJokerRule);
                    var otherCardValue = GetCardValue(otherCards[i], other.IncludeJokerRule);
                    if (cardValue != otherCardValue)
                    {
                        var compareResult = cardValue.CompareTo(otherCardValue);
                        return compareResult;
                    }
                }
            }

            return 0;
        }

        private static int GetCardValue(char card, bool UseJokerRule)
        {
            if (!UseJokerRule)
            {
                if (card == 'A')
                    return 12;
                else if (card == 'K')
                    return 11;
                else if (card == 'Q')
                    return 10;
                else if (card == 'J')
                    return 9;
                else if (card == 'T')
                    return 8;
                else
                    return int.Parse(card.ToString()) - 2;
            }
            else
            {
                if (card == 'A')
                    return 12;
                else if (card == 'K')
                    return 11;
                else if (card == 'Q')
                    return 10;
                else if (card == 'J')
                    return 0;
                else if (card == 'T')
                    return 9;
                else
                    return int.Parse(card.ToString()) - 1;
            }
        }
    }
}
