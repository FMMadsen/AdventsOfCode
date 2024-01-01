using Common;

namespace AdventOfCode2023Solutions.Day07
{
    [Flags]
    public enum ECard
    {
        None = 0b_0000_0000,  // 0
        Two = 0b_0000_0001,  // 1
        Three = 0b_0000_0010,  // 2
        Four = 0b_0000_0100,  // 4
        Five = 0b_0000_1000,  // 8
        Six = 0b_0001_0000,  // 16
        Seven = 0b_0010_0000,  // 32
        Eight = 0b_0100_0000,  // 64
        Nine = 0b_1000_0000,  // 128
        Ten = 0b_0001_0000_0000,  // 256
        Knight = 0b_0010_0000_0000,  // 512
        Queen = 0b_0100_0000_0000,  // 1024
        King = 0b_1000_0000_0000,  // 2048
        Ace = 0b_0001_0000_0000_0000,  // 4096
    }

    [Flags]
    public enum EHand
    {
        None = 0b_0000_0000,
        HighCard = 0b_0000_0001, 
        OnePair = 0b_0000_0010,  
        TwoPair = 0b_0000_0100, 
        ThreeOfAKind = 0b_0000_1000,  
        FullHouse = 0b_0001_0000,  
        FourOfAKind = 0b_0010_0000,  
        FiveOfAKind = 0b_0100_0000,  
    }

    public class Card
    {
        public int Number
        {
            get
            {
                int number;

                switch (Value)
                {
                    case ECard.Two: number = 2; break;
                    case ECard.Three: number = 3; break;
                    case ECard.Four: number = 4; break;
                    case ECard.Five: number = 5; break;
                    case ECard.Six: number = 6; break;
                    case ECard.Seven: number = 7; break;
                    case ECard.Eight: number = 8; break;
                    case ECard.Nine: number = 9; break;
                    case ECard.Ten: number = 10; break;
                    case ECard.Knight: number = 11; break;
                    case ECard.Queen: number = 12; break;
                    case ECard.King: number = 13; break;
                    case ECard.Ace: number = 14; break;
                    default: number = 0; break;
                }

                return number;
            }
        }
        public string Label 
        { 
            get 
            {
                string label;

                switch (Value)
                {
                    case ECard.Two: label = "2"; break;
                    case ECard.Three: label = "3"; break;
                    case ECard.Four: label = "4"; break;
                    case ECard.Five: label = "5"; break;
                    case ECard.Six: label = "6"; break;
                    case ECard.Seven: label = "7"; break;
                    case ECard.Eight: label = "8"; break;
                    case ECard.Nine: label = "9"; break;
                    case ECard.Ten: label = "10"; break;
                    case ECard.Knight: label = "Knight"; break;
                    case ECard.Queen: label = "Queen"; break;
                    case ECard.King: label = "King"; break;
                    case ECard.Ace: label = "Ace"; break;
                    default: label = "No Card"; break;
                }

                return label;
            } 
        }
        public ECard Value { get; set; } = ECard.None;
    }

    public class Hand
    {
        private Card[] _RecievedCards;
        private Dictionary<ECard, List<Card>> _Cards = new Dictionary<ECard, List<Card>>();
        public Dictionary<ECard, List<Card>> Cards { get { return _Cards; } }

        private EHand _HandType = EHand.None;
        public EHand HandType
        {
            get
            {
                return _HandType;
            }
        }

        public Hand(Card[] cards)
        {
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
            _Cards.Add(ECard.Knight, new List<Card>());
            _Cards.Add(ECard.Queen, new List<Card>());
            _Cards.Add(ECard.King, new List<Card>());
            _Cards.Add(ECard.Ace, new List<Card>());

            foreach(Card card in cards)
            {
                _Cards[card.Value].Add(card);
            }

            DetermineType();
        }

        public void DetermineType()
        {
            Card threeOfAKind = null;
            List<Card> pair = new List<Card>();
            foreach(KeyValuePair<ECard, List<Card>> cardList in _Cards)
            {
                if (5 == cardList.Value.Count) { _HandType = EHand.FiveOfAKind; return; }
                if (4 == cardList.Value.Count) { _HandType = EHand.FourOfAKind; return; }
                if (3 == cardList.Value.Count) { threeOfAKind = cardList.Value[0]; }
                if (2 == cardList.Value.Count) { pair.Add(cardList.Value[0]); }
            }

            if (null != threeOfAKind)
            {
                _HandType = pair.Count == 1 ? EHand.FullHouse : EHand.ThreeOfAKind;
                return;
            }

            if (2 == pair.Count)
            {
                _HandType = EHand.TwoPair; 
                return;
            }

            if (1 == pair.Count)
            {
                _HandType = EHand.OnePair;
                return;
            }

            _HandType = EHand.HighCard;
        }
    }

    public class Solution(string[] DatasetLines) : IAOCSolution
    {
        

        public string PuzzleName => "Day 7: ";

        public string SolvePart1()
        {
            
            var a = new Card() { Value = ECard.Two };
            var b = new Card() { Value = ECard.Ten };
            var c = new Card() { Value = ECard.Ace };
            var d = new Card() { Value = ECard.None };
            var e = new Card();

            var ab = a.Number;
            var bb = b.Number;
            var cb = c.Number;
            var db = d.Number;
            var eb = e.Number;

            return "To be implemented";
        }

        public string SolvePart2()
        {
            return "To be implemented";
        }
    }
}
