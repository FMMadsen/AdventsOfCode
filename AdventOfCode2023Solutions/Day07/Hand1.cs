

namespace AdventOfCode2023Solutions.Day07
{
    public class Hand1
    {
        private Card1[] _RecievedCards;
        private Dictionary<ECard1, List<Card1>> _Cards = new Dictionary<ECard1, List<Card1>>();
        public Card1[] Cards { get { return _RecievedCards; } }

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

        public Hand1(Card1[] cards)
        {
            _RecievedCards = cards;

            // TODO
            // _Cards change to List<Card>[]
            // access by _Cards[ECard.None]

            _Cards.Add(ECard1.None, new List<Card1>());
            _Cards.Add(ECard1.Two, new List<Card1>());
            _Cards.Add(ECard1.Three, new List<Card1>());
            _Cards.Add(ECard1.Four, new List<Card1>());
            _Cards.Add(ECard1.Five, new List<Card1>());
            _Cards.Add(ECard1.Six, new List<Card1>());
            _Cards.Add(ECard1.Seven, new List<Card1>());
            _Cards.Add(ECard1.Eight, new List<Card1>());
            _Cards.Add(ECard1.Nine, new List<Card1>());
            _Cards.Add(ECard1.Ten, new List<Card1>());
            _Cards.Add(ECard1.Jack, new List<Card1>());
            _Cards.Add(ECard1.Queen, new List<Card1>());
            _Cards.Add(ECard1.King, new List<Card1>());
            _Cards.Add(ECard1.Ace, new List<Card1>());

            foreach (Card1 card in cards)
            {
                _Cards[card.Value].Add(card);
            }

            _HandType = DetermineType(_Cards);
            Strength = DetermineStrength();
        }

        public static EHand DetermineType(Dictionary<ECard1, List<Card1>> cards)
        {
            Card1? threeOfAKind = null;
            List<Card1> pair = new List<Card1>();
            
            foreach (KeyValuePair<ECard1, List<Card1>> cardList in cards)
            {
                if (0 == cardList.Value.Count) { continue; }
                if (5 == cardList.Value.Count) { return EHand.FiveOfAKind; }
                if (4 == cardList.Value.Count) { return EHand.FourOfAKind; }
                if (3 == cardList.Value.Count) { threeOfAKind = cardList.Value[0]; }
                if (2 == cardList.Value.Count) { pair.Add(cardList.Value[0]); }
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
