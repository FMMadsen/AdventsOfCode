using Common;

namespace AdventOfCode2023Solutions.Day07
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 7: Camel Cards";

        public string SolvePart1(string[] datasetLines)
        {
            Hand1[] hands = BuildHands1();
            hands = RankHands1(hands);

            long winnings = 0;

            for (int i = 0; i < hands.Length; i++)
            {
                winnings += hands[i].Bid * (i + 1);
            }

            return winnings.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            Hand[] hands = BuildHands();
            hands = RankHands(hands);

            long winnings = 0;

            for (int i = 0; i < hands.Length; i++)
            {
                winnings += hands[i].Bid * (i + 1);
            }

            return winnings.ToString();
        }

        public Hand1[] BuildHands1()
        {
            Hand1[] hands = new Hand1[DatasetLines.Length];

            for (int i = 0; i < DatasetLines.Length; i++)
            {
                string[] splitLine = DatasetLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Card1[] cards = splitLine[0].ToCharArray().Select(a => new Card1(Card1.FromLabels[a])).ToArray();

                hands[i] = new Hand1(cards);
                hands[i].Bid = int.Parse(splitLine[1]);
            }

            return hands;
        }
        public Hand1[] RankHands1(Hand1[] hands)
        {

            Hand1[] handsT = hands.OrderBy(h => h.Strength, StringComparer.InvariantCulture).ToArray();

            return handsT;

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
            
            Hand[] handsT = hands.OrderBy(h => h.Strength, StringComparer.InvariantCulture).ToArray();

            return handsT;

        }


    }
}
