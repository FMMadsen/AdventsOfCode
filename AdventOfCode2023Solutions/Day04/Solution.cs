using Common;

namespace AdventOfCode2023Solutions.Day04
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 4: Scratchcards";

        public readonly long BaseScore = 1;
        public readonly long ScoreMultiplier = 2;

        public readonly List<Card> Cards = new();

        public string SolvePart1(string[] datasetLines)
        {
            long winnings = 0;

            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                string[] card = DatasetLines[lineNumber].Split(':','|');
                int[] winningNumbers = Array.ConvertAll(card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), Int32.Parse);
                int[] myNumbers = Array.ConvertAll(card[2].Split(' ', StringSplitOptions.RemoveEmptyEntries), Int32.Parse);

                int cardWinningTimes = 0;

                foreach (int myNumber in myNumbers)
                {
                    if (winningNumbers.Contains(myNumber))
                    {
                        cardWinningTimes++;
                    }
                }

                winnings += BaseScore * (long)Math.Pow(ScoreMultiplier, cardWinningTimes-1);
            }

            return winnings.ToString();
        }


        public string SolvePart2(string[] datasetLines)
        {
            for (int lineNumber = 0; lineNumber < DatasetLines.Length; lineNumber++)
            {
                string[] card = DatasetLines[lineNumber].Split(':', '|');
                Cards.Add(new Card() { 
                    Id = lineNumber,
                    Name = card[0].Trim(),
                    WinningNumbers = Array.ConvertAll(card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), Int32.Parse),
                    ScratchNumbers = Array.ConvertAll(card[2].Split(' ', StringSplitOptions.RemoveEmptyEntries), Int32.Parse)
                });
            }

            long totalCards = CountWinnings(Cards);

            return totalCards.ToString();
        }

        public long CountWinnings(List<Card> cards) 
        {
            long totalCards = 0;

            foreach (Card card in cards)
            {
                totalCards++;

                List<Card> cardsWon = new();

                foreach (int scratchNumber in card.ScratchNumbers)
                {
                    if (card.WinningNumbers.Contains(scratchNumber))
                    {
                        int newCardID = card.Id + 1 + cardsWon.Count;
                        if (newCardID < Cards.Count)
                        {
                            cardsWon.Add(Cards[newCardID]);
                        }
                        
                    }
                }

                totalCards += CountWinnings(cardsWon);
            }

            return totalCards;
        }
    }
}
