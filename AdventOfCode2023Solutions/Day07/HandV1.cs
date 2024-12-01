namespace AdventOfCode2023Solutions.Day07
{
    internal class HandV1
    {
        internal string CardString { get; set; }
        internal char[] CardArray { get; set; }
        internal HandTypes HandType { get; set; }
        internal long Bet { get; set; }
        internal long Strength { get; set; }
        internal bool UseJokerRule { get; private set; }

        public HandV1(string cardString, long bet, bool includeJokerRule = false)
        {
            CardString = cardString;
            CardArray = cardString.ToArray();
            Bet = bet;
            UseJokerRule = includeJokerRule;
            DetermineHandType(cardString);
            DetermineHandStrength();

        }

        private void DetermineHandStrength()
        {
            AddStrengthBasedOnHandType();
            AddStrengthBasedOnIndividialCards();
        }

        private void AddStrengthBasedOnHandType()
        {
            switch (HandType)
            {
                case HandTypes.HighCard:
                    Strength += 0;
                    break;
                case HandTypes.OnePair:
                    Strength += 2000000;
                    break;
                case HandTypes.TwoPair:
                    Strength += 4000000;
                    break;
                case HandTypes.ThreeOfAKind:
                    Strength += 6000000;
                    break;
                case HandTypes.FullHouse:
                    Strength += 8000000;
                    break;
                case HandTypes.FourOfAKind:
                    Strength += 10000000;
                    break;
                case HandTypes.FiveOfAKind:
                    Strength += 12000000;
                    break;
                default:
                    break;
            }
        }

        private void AddStrengthBasedOnIndividialCards()
        {
            Strength += GetCardValue(CardArray[4]);
            Strength += GetCardValue(CardArray[3]) * 15;
            Strength += GetCardValue(CardArray[2]) * 200;
            Strength += GetCardValue(CardArray[1]) * 5000;
            Strength += GetCardValue(CardArray[0]) * 100000;
        }

        private long GetCardValue(char card)
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
                    return long.Parse(card.ToString()) - 2;
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
                    return long.Parse(card.ToString()) - 1;
            }
        }

        private void DetermineHandType(string cardString)
        {
            var cardDict = CountCards();
            var numberOfKindOfCards = cardDict.Count;

            switch (numberOfKindOfCards)
            {
                case 1:
                    HandType = HandTypes.FiveOfAKind;
                    break;
                case 2:
                    var contains4SimilarCards = cardDict.ContainsValue(4);
                    HandType = contains4SimilarCards ? HandTypes.FourOfAKind : HandTypes.FullHouse;
                    break;
                case 3:
                    var contains3SimilarCards = cardDict.ContainsValue(3);
                    HandType = contains3SimilarCards ? HandTypes.ThreeOfAKind : HandTypes.TwoPair;
                    break;
                case 4:
                    HandType = HandTypes.OnePair;
                    break;
                default:
                    HandType = HandTypes.HighCard;
                    break;
            }
        }

        private Dictionary<char, int> CountCards()
        {
            var cardDict = new Dictionary<char, int>();

            foreach (char card in CardArray)
            {
                if (cardDict.ContainsKey(card))
                    cardDict[card]++;
                else
                    cardDict.Add(card, 1);
            }

            if (UseJokerRule)
            {
                if (cardDict.TryGetValue('J', out int numberOfJokers))
                {
                    switch (numberOfJokers)
                    {
                        case 1:
                            //If there are 1 Joker in the hand, then turn it into whatever card there are most of
                            var card4OfKind1 = cardDict.FirstOrDefault(c => c.Value == 4);
                            var card3OfKind1 = cardDict.FirstOrDefault(c => c.Value == 3);
                            var cardPair1 = cardDict.FirstOrDefault(c => c.Value == 2);
                            var hasCard4OfKind1 = card4OfKind1.Value > 0;
                            var hasCard3OfKind1 = card3OfKind1.Value > 0;
                            var hasCardPair1 = cardPair1.Value > 0;
                            if (hasCard4OfKind1)
                            {
                                cardDict[card4OfKind1.Key]++;
                                cardDict.Remove('J');
                            }
                            else if (hasCard3OfKind1)
                            {
                                cardDict[card3OfKind1.Key]++;
                                cardDict.Remove('J');
                            }
                            else if (hasCardPair1)
                            {
                                cardDict[cardPair1.Key]++;
                                cardDict.Remove('J');
                            }
                            else
                            {
                                var highCard = cardDict.FirstOrDefault(c => c.Key != 'J');
                                cardDict[highCard.Key]++;
                                cardDict.Remove('J');
                            }
                            break;
                        case 2:
                            //If any other card appears twice or more, then both jokers should be turned to those
                            var card3OfKind2 = cardDict.FirstOrDefault(c => c.Value == 3);
                            var cardPair2 = cardDict.FirstOrDefault(c => c.Value == 2 && c.Key != 'J');
                            var has3OfKind2 = card3OfKind2.Value > 0;
                            var hasPair2 = cardPair2.Value > 0;
                            if (has3OfKind2)
                            {
                                cardDict[card3OfKind2.Key] += 2;
                                cardDict.Remove('J');
                            }
                            else if (hasPair2)
                            {
                                cardDict[cardPair2.Key] += 2;
                                cardDict.Remove('J');
                            }
                            break;
                        case 3:
                            //Do nothing, 3 jokers can't be turned into anything better
                            break;
                        case 4:
                            //Do nothing, 4 jokers can't be turned into anything better
                            break;
                    }
                }
            }

            return cardDict;
        }
    }
}
