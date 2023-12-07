namespace AdventOfCode2023Solutions.Day07
{
    internal class Hand
    {
        internal string CardString { get; set; }
        internal char[] CardArray { get; set; }
        internal HandTypes HandType { get; set; }
        internal long Bet { get; set; }

        public Hand(string cardString, long bet)
        {
            CardString = cardString;
            CardArray = cardString.ToArray();
            HandType = DetermineHandType(cardString);
            Bet = bet;
        }

        private HandTypes DetermineHandType(string cardString)
        {
            var cardDict = new Dictionary<char, int>();

            foreach (char card in CardArray)
            {
                if (cardDict.ContainsKey(card))
                    cardDict[card]++;
                else
                    cardDict.Add(card, 1);
            }

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
                    handType= HandTypes.OnePair;
                    break;
                default:
                    handType = HandTypes.HighCard;
                    break;
            }

            return handType;
        }
    }
}
