using Common;

namespace AdventOfCode2023Solutions.Day04
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 4: Scratchcards";

        List<Card> Cards = new();

        public string SolvePart1(string[] datasetLines)
        {
            long baseScore = 1;
            long scoreMultiplier = 2;
            long winnings = 0;

            for (int lineNumber = 0; lineNumber < datasetLines.Length; lineNumber++)
            {
                string[] card = datasetLines[lineNumber].Split(':','|');
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

                winnings += baseScore * (long)Math.Pow(scoreMultiplier, cardWinningTimes-1);
            }

            return winnings.ToString();
        }

        private int[] InitializeCardCounters(List<ScratchCard> scratchCards)
        {
            var initialCardCounter = new int[scratchCards.Count() + 1];
            for (int i = 0; i < initialCardCounter.Length; i++)
            {
                initialCardCounter[i] = 1;
            }
            initialCardCounter[0] = 0;
            return initialCardCounter;
        }

        public string SolvePart2(string[] datasetLines)
        {
            for (int lineNumber = 0; lineNumber < datasetLines.Length; lineNumber++)
            {
                string[] card = datasetLines[lineNumber].Split(':', '|');
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
