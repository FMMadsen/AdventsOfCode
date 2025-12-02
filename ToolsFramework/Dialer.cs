namespace ToolsFramework
{
    public class Dialer
    {
        public Dialer(int lowNumber, int highNumber, int? initial = null)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(lowNumber, highNumber);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lowNumber, highNumber);
            Pointer = initial ?? lowNumber;
            Scale = highNumber - lowNumber + 1;
            HighNumber = highNumber;
            LowNumber = lowNumber;
        }

        public int Pointer { get; private set; }
        public int Scale { get; init; }
        public int LowNumber { get; init; }
        public int HighNumber { get; init; }
        public int LowNumberHits { get; private set; }

        public void Rotate(int clicks, Direction direction)
        {
            int add = direction == Direction.Right ? 1 : -1;
            var newNumber = Pointer + (clicks * add);

            if (newNumber > HighNumber)
            {
                var overrun = newNumber - HighNumber;
                var scaledOverrun = overrun % Scale;
                if (scaledOverrun == 0)
                    Pointer = HighNumber;
                else
                    Pointer = LowNumber + scaledOverrun - 1;

                var rotations = overrun / Scale;
            }
            else if (newNumber < LowNumber)
            {
                var underrun = LowNumber - newNumber;
                var scaledUnderRun = underrun % Scale;
                if (scaledUnderRun == 0)
                    Pointer = LowNumber;
                else
                    Pointer = HighNumber - scaledUnderRun + 1;

                var rotations = underrun / Scale;
                LowNumberHits = rotations;
            }
            else
            {
                Pointer = newNumber;
            }
        }

        public void RotateV2(int clicks, Direction direction)
        {
            int rotations = clicks / Scale;
            LowNumberHits += rotations;

            var moves = clicks % Scale;
            var newPointer = direction == Direction.Right ? Pointer + moves : Pointer - moves;

            if (newPointer > HighNumber)
            {
                LowNumberHits++;
                Pointer = LowNumber + (newPointer - HighNumber);
            }
            else if (newPointer < LowNumber)
            {
                LowNumberHits++;
                Pointer = HighNumber - LowNumber - newPointer;
            }
            else
            {
                Pointer = newPointer;
                if (Pointer == LowNumber)
                    LowNumberHits++;
            }
        }
    }

    public enum Direction
    {
        Left,
        Right,
    }
}
