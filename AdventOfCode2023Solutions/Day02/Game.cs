namespace AdventOfCode2023Solutions.Day02
{
    internal class Game
    {
        private readonly int gameStringLength = "Game ".Length;
        private const string Red = "red";
        private const string Green = "green";
        private const string Blue = "blue";

        internal int GameNumber { get; private set; }
        internal int RedCubesRevealed { get; private set; }
        internal int GreenCubesRevealed { get; private set; }
        internal int BlueCubesRevealed { get; private set; }

        internal Game(string inputLine)
        {
            var mainGameLineSections = inputLine.Split(":");

            var numberString = mainGameLineSections[0].Substring(gameStringLength);
            GameNumber = int.Parse(numberString);

            var revealBatches = mainGameLineSections[1].Split(";");

            foreach (var revealedBatch in revealBatches)
            {
                GrabCubes(revealedBatch);
            }
        }

        internal int CalculatePowerOfMinimumCubes()
        {
            return RedCubesRevealed * GreenCubesRevealed * BlueCubesRevealed;
        }

        private void GrabCubes(string cubeBatchesGroupedByColorsString)
        {
            cubeBatchesGroupedByColorsString = cubeBatchesGroupedByColorsString.Trim().ToLower();
            var cubeBatchesGroupedByColors = cubeBatchesGroupedByColorsString.Split(",");

            int red = 0;
            int green = 0;
            int blue = 0;

            foreach (var cubeBatchForOneColour in cubeBatchesGroupedByColors)
            {
                var cubeBatch = cubeBatchForOneColour.Trim().ToLower();

                var index = -1;

                index = cubeBatch.IndexOf(Red);
                if (index != -1)
                {
                    int numberOf = int.Parse(cubeBatch.Substring(0, index - 1));
                    red += numberOf;
                    continue;
                }

                index = cubeBatch.IndexOf(Green);
                if (index != -1)
                {
                    int numberOf = int.Parse(cubeBatch.Substring(0, index - 1));
                    green += numberOf;
                    continue;
                }

                index = cubeBatch.IndexOf(Blue);
                if (index != -1)
                {
                    int numberOf = int.Parse(cubeBatch.Substring(0, index - 1));
                    blue += numberOf;
                    continue;
                }
            }

            if (red > RedCubesRevealed)
                RedCubesRevealed = red;

            if (green > GreenCubesRevealed)
                GreenCubesRevealed = green;

            if (blue > BlueCubesRevealed)
                BlueCubesRevealed = blue;
        }
    }
}
