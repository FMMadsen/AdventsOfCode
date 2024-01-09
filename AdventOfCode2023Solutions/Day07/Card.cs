

namespace AdventOfCode2023Solutions.Day07
{
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
        public static readonly Dictionary<char, ECard> FromLabels = new Dictionary<char, ECard>() {
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
}
