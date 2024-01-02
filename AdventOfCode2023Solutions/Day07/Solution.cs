using Common;

namespace AdventOfCode2023Solutions.Day07
{
    public enum ECard
    {
        None = 96,
        Joker = 97,
        Two = 98, 
        Three = 99, 
        Four = 100, 
        Five = 101, 
        Six = 102, 
        Seven = 103, 
        Eight = 104,
        Nine = 105,
        Ten = 106,  
        Queen = 107, 
        King = 108,
        Ace = 109
    }

    public enum EHand
    {
        None = 96,
        HighCard = 97, 
        OnePair = 98,  
        TwoPair = 99, 
        ThreeOfAKind = 100,  
        FullHouse = 101,  
        FourOfAKind = 102,  
        FiveOfAKind = 103 
    }

    public class Card
    {
        
        public static readonly Dictionary<ECard, int> Numbers = new Dictionary<ECard, int>() {
            { ECard.None,0},
            { ECard.Two,2 },
            { ECard.Three,3},
            { ECard.Four,4},
            { ECard.Five,5},
            { ECard.Six,6},
            { ECard.Seven,7},
            { ECard.Eight,8},
            { ECard.Nine,9},
            { ECard.Ten,10},
            { ECard.Joker,1},
            { ECard.Queen,12},
            { ECard.King,13},
            { ECard.Ace,14}
        };
        public static readonly Dictionary<int, ECard> FromNumbers = new Dictionary<int, ECard>() {
            {0, ECard.None},
            {2, ECard.Two},
            {3, ECard.Three},
            {4, ECard.Four},
            {5, ECard.Five},
            {6, ECard.Six},
            {7, ECard.Seven},
            {8, ECard.Eight},
            {9, ECard.Nine},
            {10, ECard.Ten},
            {1, ECard.Joker},
            {12, ECard.Queen},
            {13, ECard.King},
            {14, ECard.Ace}
        };
        public static readonly Dictionary<ECard, char> Labels = new Dictionary<ECard, char>() {
            { ECard.None,' '},
            { ECard.Two, '2'},
            { ECard.Three,'3'},
            { ECard.Four,'4'},
            { ECard.Five,'5'},
            { ECard.Six,'6'},
            { ECard.Seven,'7'},
            { ECard.Eight,'8'},
            { ECard.Nine,'9'},
            { ECard.Ten,'T'},
            { ECard.Joker,'J'},
            { ECard.Queen,'Q'},
            { ECard.King,'K'},
            { ECard.Ace,'A'}
        };
        public static readonly Dictionary<char,ECard> FromLabels = new Dictionary<char,ECard>() {
            {' ', ECard.None},
            {'2', ECard.Two},
            {'3', ECard.Three},
            {'4', ECard.Four},
            {'5', ECard.Five},
            {'6', ECard.Six},
            {'7', ECard.Seven},
            {'8', ECard.Eight},
            {'9', ECard.Nine},
            {'T', ECard.Ten},
            {'J', ECard.Joker},
            {'Q', ECard.Queen},
            {'K', ECard.King},
            {'A', ECard.Ace}
        };

        public int Number
        {
            get
            {
                return Numbers[Value];
            }
        }
        public char Label 
        { 
            get 
            {
                return Labels[Value];
            } 
        }
        public ECard Value { get; set; } = ECard.None;

        public Card(ECard value)
        {
            Value = value;
        }
    }

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
            //_RecievedCards = cards.OrderByDescending(a => a.Number).ToArray();
            _RecievedCards = cards;

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

            foreach(Card card in cards)
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
            foreach(KeyValuePair<ECard, List<Card>> cardList in cards)
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

    public class Solution(string[] DatasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 7: ";

        public string SolvePart1()
        {
            return "Outdated";
        }

        public string SolvePart2()
        {
            Hand[] hands = BuildHands();
            hands = RankHands(hands);

            long winnings = 0;

            for (int i = 0; i < hands.Length; i++)
            {
                winnings += hands[i].Bid * (i + 1);
                string[] write = new string[7];
                write[0] = (i + 1).ToString();
                write[1] = hands[i].Strength;
                write[2] = hands[i].Cards[0].Value.ToString();
                write[3] = hands[i].Cards[1].Value.ToString(); 
                write[4] = hands[i].Cards[2].Value.ToString();
                write[5] = hands[i].Cards[3].Value.ToString();
                write[6] = hands[i].Cards[4].Value.ToString();
                Console.WriteLine(String.Join(" ", write));
            }

            return winnings.ToString();
        }

        public Hand[] BuildHands() 
        {
            Hand[] hands = new Hand[DatasetLines.Length];

            for(int i = 0; i < DatasetLines.Length; i++)
            {
                string[] splitLine = DatasetLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Card[] cards = splitLine[0].ToCharArray().Select(a => new Card(Card.FromLabels[a])).ToArray();

                hands[i] = new Hand(cards);
                hands[i].Bid = int.Parse( splitLine[1] );
            }

            return hands;
        }
        public Hand[] RankHands(Hand[] hands)
        {
            
            Hand[] hands2 = hands.OrderBy(h => h.Strength, StringComparer.InvariantCulture).ToArray();

            return hands2;

        }

    }
}
