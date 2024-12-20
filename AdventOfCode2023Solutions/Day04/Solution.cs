﻿using Common;

namespace AdventOfCode2023Solutions.Day04
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 4: Scratchcards";

        public string SolvePart1(string[] datasetLines)
        {
            var scratchCards = LoadInitialScratchCards(datasetLines);
            int sum = 0;
            scratchCards.ForEach(i => sum += i.CalculatePointsScored());
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var scratchCards = LoadInitialScratchCards(datasetLines);
            var cardCounters = InitializeCardCounters(scratchCards);

            foreach (var scratchCard in scratchCards)
            {
                if (scratchCard.NumberOfMatches > 0)
                {
                    for (int i = 0; i < cardCounters[scratchCard.CardNumber]; i++)
                    {
                        AddExtraCardCopies(scratchCard.CardNumber, scratchCard.NumberOfMatches, cardCounters);
                    }
                }
            }

            return cardCounters.Sum().ToString();
        }

        private List<ScratchCard> LoadInitialScratchCards(string[] datasetLines)
        {
            List<ScratchCard> scratchCards = new();
            foreach (var datasetLine in datasetLines)
            {
                scratchCards.Add(new ScratchCard(datasetLine));
            }
            return scratchCards;
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

        private void AddExtraCardCopies(int currentNumber, int numberOfExtraCards, int[] cardCounters)
        {
            for (int cardNumber = currentNumber + 1; cardNumber < currentNumber + numberOfExtraCards + 1; cardNumber++)
            {
                cardCounters[cardNumber]++;
            }
        }
    }
}
