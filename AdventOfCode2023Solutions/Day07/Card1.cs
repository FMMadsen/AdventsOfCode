

namespace AdventOfCode2023Solutions.Day07
{
    public class Card1
    {

        public static readonly Dictionary<ECard1, int> Numbers = new Dictionary<ECard1, int>() {
            { ECard1.None,0},
            { ECard1.Two,2 },
            { ECard1.Three,3},
            { ECard1.Four,4},
            { ECard1.Five,5},
            { ECard1.Six,6},
            { ECard1.Seven,7},
            { ECard1.Eight,8},
            { ECard1.Nine,9},
            { ECard1.Ten,10},
            { ECard1.Jack,11},
            { ECard1.Queen,12},
            { ECard1.King,13},
            { ECard1.Ace,14}
        };
        public static readonly Dictionary<int, ECard1> FromNumbers = new Dictionary<int, ECard1>() {
            {0, ECard1.None},
            {2, ECard1.Two},
            {3, ECard1.Three},
            {4, ECard1.Four},
            {5, ECard1.Five},
            {6, ECard1.Six},
            {7, ECard1.Seven},
            {8, ECard1.Eight},
            {9, ECard1.Nine},
            {10, ECard1.Ten},
            {1, ECard1.Jack},
            {12, ECard1.Queen},
            {13, ECard1.King},
            {14, ECard1.Ace}
        };
        public static readonly Dictionary<ECard1, char> Labels = new Dictionary<ECard1, char>() {
            { ECard1.None,' '},
            { ECard1.Two, '2'},
            { ECard1.Three,'3'},
            { ECard1.Four,'4'},
            { ECard1.Five,'5'},
            { ECard1.Six,'6'},
            { ECard1.Seven,'7'},
            { ECard1.Eight,'8'},
            { ECard1.Nine,'9'},
            { ECard1.Ten,'T'},
            { ECard1.Jack,'J'},
            { ECard1.Queen,'Q'},
            { ECard1.King,'K'},
            { ECard1.Ace,'A'}
        };
        public static readonly Dictionary<char, ECard1> FromLabels = new Dictionary<char, ECard1>() {
            {' ', ECard1.None},
            {'2', ECard1.Two},
            {'3', ECard1.Three},
            {'4', ECard1.Four},
            {'5', ECard1.Five},
            {'6', ECard1.Six},
            {'7', ECard1.Seven},
            {'8', ECard1.Eight},
            {'9', ECard1.Nine},
            {'T', ECard1.Ten},
            {'J', ECard1.Jack},
            {'Q', ECard1.Queen},
            {'K', ECard1.King},
            {'A', ECard1.Ace}
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
        public ECard1 Value { get; set; } = ECard1.None;

        public Card1(ECard1 value)
        {
            Value = value;
        }
    }
}
