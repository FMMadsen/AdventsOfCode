namespace AdventsOfCode2022.Day06TuningTrouble
{
    internal class SubroutineStartOfPacket
    {
        public string Buffer { get; private set; }
        public string StartOfPacketMarker { get; private set; }
        public int StartOfPacketMarkerEnd { get; private set; }

        private readonly int StartOfPacketLength;

        public SubroutineStartOfPacket(int startOfPacketLength)
        {
            Buffer = string.Empty;
            StartOfPacketMarker = string.Empty;
            StartOfPacketMarkerEnd = 0;
            StartOfPacketLength = startOfPacketLength;
        }

        public bool LoadBuffer(string dataStreamBuffer)
        {
            Buffer = dataStreamBuffer;

            for(int startOfPotentialMarkerBegin = 1; startOfPotentialMarkerBegin <= Buffer.Length - StartOfPacketLength + 1; startOfPotentialMarkerBegin++)
            {
                var potentialMarker = Buffer.Substring(startOfPotentialMarkerBegin-1, StartOfPacketLength);
                if(IsAllCaractersDifferent(potentialMarker))
                {
                    StartOfPacketMarker = potentialMarker;
                    StartOfPacketMarkerEnd = startOfPotentialMarkerBegin + StartOfPacketLength - 1;
                    return true;
                }
            }

            return false;
        }

        public bool IsAllCaractersDifferent(string characters)
        {
            for (int i1 = 0; i1 < characters.Length-1; i1++)
            {
                for (int i2 = i1+1; i2 < characters.Length; i2++)
                {
                    if(characters[i1] == characters[i2])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
