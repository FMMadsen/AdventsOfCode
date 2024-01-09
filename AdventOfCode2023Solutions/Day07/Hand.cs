

namespace AdventOfCode2023Solutions.Day07
{
    public class Hand
    {
        private Card[] _RecievedCards;
        private Dictionary<ECard, List<Card>> _Cards = new Dictionary<ECard, List<Card>>();
        public Card[] Cards { get { return _RecievedCards; } }

        private EHand _HandType = EHand.None;
        public EHand HandType
        {
            get
            {
                return _HandType;
            }
        }

        public int Bid = 0;
        public string Strength = "";

        public Hand(Card[] cards)
        {
            _RecievedCards = cards;

            // TODO
            // _Cards change to List<Card>[]
            // access by _Cards[ECard.None]

            _Cards.Add(ECard.None, new List<Card>());
            _Cards.Add(ECard.Two, new List<Card>());
            _Cards.Add(ECard.Three, new List<Card>());
            _Cards.Add(ECard.Four, new List<Card>());
            _Cards.Add(ECard.Five, new List<Card>());
            _Cards.Add(ECard.Six, new List<Card>());
            _Cards.Add(ECard.Seven, new List<Card>());
            _Cards.Add(ECard.Eight, new List<Card>());
            _Cards.Add(ECard.Nine, new List<Card>());
            _Cards.Add(ECard.Ten, new List<Card>());
            _Cards.Add(ECard.Joker, new List<Card>());
            _Cards.Add(ECard.Queen, new List<Card>());
            _Cards.Add(ECard.King, new List<Card>());
            _Cards.Add(ECard.Ace, new List<Card>());

            foreach (Card card in cards)
            {
                _Cards[card.Value].Add(card);
            }

            _HandType = DetermineType(_Cards);
            Strength = DetermineStrength();
        }

        public static EHand DetermineType(Dictionary<ECard, List<Card>> cards)
        {
            Card? threeOfAKind = null;
            List<Card> pair = new List<Card>();
            bool jokersUsed = false;
            foreach (KeyValuePair<ECard, List<Card>> cardList in cards)
            {
                if (5 == cardList.Value.Count || 5 == cardList.Value.Count + cards[ECard.Joker].Count) { return EHand.FiveOfAKind; }
                if (cardList.Key == ECard.Joker || 0 == cardList.Value.Count) { continue; }
                if (4 == cardList.Value.Count || 4 == cardList.Value.Count + cards[ECard.Joker].Count) { return EHand.FourOfAKind; }
                if (3 == cardList.Value.Count) { threeOfAKind = cardList.Value[0]; }
                if (null == threeOfAKind && 0 < cards[ECard.Joker].Count && 3 == cardList.Value.Count + cards[ECard.Joker].Count)
                {
                    if (jokersUsed) { pair.RemoveAt(0); }
                    threeOfAKind = cardList.Value[0];
                    jokersUsed = true;
                }
                if (2 == cardList.Value.Count && (null == threeOfAKind || cardList.Key != threeOfAKind.Value)) { pair.Add(cardList.Value[0]); }
                if (!jokersUsed && 0 < cards[ECard.Joker].Count && 2 == cardList.Value.Count + cards[ECard.Joker].Count)
                {
                    pair.Add(cardList.Value[0]);
                    jokersUsed = true;
                }
            }

            if (null != threeOfAKind)
            {
                return pair.Count == 1 ? EHand.FullHouse : EHand.ThreeOfAKind;
            }

            if (2 == pair.Count)
            {
                return EHand.TwoPair;
            }

            if (1 == pair.Count)
            {
                return EHand.OnePair;
            }

            return EHand.HighCard;
        }

        public string DetermineStrength()
        {
            var rank = new char[6];
            rank[0] = (char)HandType;
            rank[1] = (char)Cards[0].Value;
            rank[2] = (char)Cards[1].Value;
            rank[3] = (char)Cards[2].Value;
            rank[4] = (char)Cards[3].Value;
            rank[5] = (char)Cards[4].Value;

            return new string(rank);
        }
    }
}
