namespace AdventOfCode2023Solutions.Day15
{
    public class Box(int boxNo)
    {
        public int BoxNo { get; private set; } = boxNo;

        public List<Lens> Lenses { get; private set; } = new List<Lens>();

        public void AddLens(Lens inLens)
        {
            var existingLensWithSameLabel = Lenses.FirstOrDefault(x => x.Label == inLens.Label);
            if (existingLensWithSameLabel != null)
                existingLensWithSameLabel.Replace(inLens);
            else
                Lenses.Add(inLens);
        }

        public void RemoveLens(string label)
        {
            var existingLensWithSameLabel = Lenses.FirstOrDefault(x => x.Label == label);
            if (existingLensWithSameLabel != null)
                Lenses.Remove(existingLensWithSameLabel);
        }

        public int SummarizeLensFocusPower()
        {
            int sum = 0, lensNumber = 0;
            Lenses.ForEach(x => { sum += (BoxNo + 1) * (++lensNumber) * x.FocalLength; });
            return sum;
        }
    }
}
