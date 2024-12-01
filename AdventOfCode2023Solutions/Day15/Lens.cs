namespace AdventOfCode2023Solutions.Day15
{
    public class Lens(int focalLength, string label)
    {
        public int FocalLength { get; private set; } = focalLength;
        public string Label { get; private set; } = label;

        public void Replace(Lens inLens)
        {
            FocalLength = inLens.FocalLength;
            Label = inLens.Label;
        }
    }
}
