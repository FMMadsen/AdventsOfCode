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
            HighEnd = highNumber;
            LowEnd = lowNumber;
        }

        public int Pointer { get; private set; }
        public int RelativePointer { get => Pointer - LowEnd; }
        public int Scale { get; init; }
        public int LowEnd { get; init; }
        public int HighEnd { get; init; }
        public int ZeroHits { get; private set; }

        public void RotateV2(int clicks, Direction direction)
        {
            int rotations = clicks / Scale;
            int deltaPosition = clicks % Scale;

            //Determine Zero hits (hits on low end number)
            ZeroHits += rotations;

            if (deltaPosition == 0)                 // pointer stay on same number
                return;

            if (Pointer != LowEnd)                   // pointer on low end alredy, no more hits
            {
                if (direction == Direction.Left && deltaPosition >= (Pointer - LowEnd))
                    ZeroHits++;                     // pointer land on or pass low number

                if (direction == Direction.Right && deltaPosition > (HighEnd - Pointer))
                    ZeroHits++;                     // pointer pass high number
            }

            // Determine the new Pointer location - already a fact that pointer is moving
            int newPosition = (direction == Direction.Right)
                ? Pointer + deltaPosition
                : Pointer - deltaPosition;

            if (newPosition < LowEnd)
                Pointer = HighEnd - (LowEnd - newPosition - 1);
            else if (newPosition > HighEnd)
                Pointer = LowEnd + (newPosition - HighEnd - 1);
            else
                Pointer = newPosition;
        }
    }

    public enum Direction
    {
        Left,
        Right,
    }
}