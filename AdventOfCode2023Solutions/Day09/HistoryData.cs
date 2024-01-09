

namespace AdventOfCode2023Solutions.Day09
{
    public class HistoryData
    {
        private List<int[]> _History = new List<int[]>();
        public int Predicted
        {
            get
            {
                return _History[0][_History[0].Length - 1];
            }
        }

        public int Predated
        {
            get
            {
                return _History[0][0];
            }
        }

        public List<int[]> History { get { return _History; } }

        public HistoryData(int[] history)
        {
            _History.Add(history);
        }

    }
}
