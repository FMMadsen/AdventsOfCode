

namespace AdventOfCode2023Solutions.Day08
{
    public class Directions
    {
        private int[] _DirectionString;
        private int _DirectionIndex = 0;

        public int DirectionIndex { get { return _DirectionIndex; } set { _DirectionIndex = value; } }

        public int DirectionIterate
        {
            get
            {
                int index = _DirectionIndex;
                _DirectionIndex++;
                if (_DirectionString.Length <= _DirectionIndex) { _DirectionIndex = 0; }
                return _DirectionString[index];
            }
        }

        public Directions(int[] directions)
        {
            _DirectionString = directions;
        }

        public Directions(string directions)
        {
            _DirectionString = directions.ToCharArray().Select(a => a == 'L' ? 0 : a == 'R' ? 1 : -1).ToArray();
        }

    }
}
