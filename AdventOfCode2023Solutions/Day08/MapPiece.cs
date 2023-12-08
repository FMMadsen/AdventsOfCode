namespace AdventOfCode2023Solutions.Day08
{
    public class MapPiece
    {
        public string Label { get; private set; }
        public string LeftMapPieceLabel { get; private set; }
        public string RightMapPieceLabel { get; private set; }
        public MapPiece? LeftMapPiece { get; private set; }
        public MapPiece? RightMapPiece { get; private set; }

        public MapPiece(string mapInputString)
        {
            var inputStringsSplit = mapInputString.Split("=");
            Label = inputStringsSplit[0].Trim();

            var connectedMaps = inputStringsSplit[1].Split(",");
            LeftMapPieceLabel = connectedMaps[0].Replace("(", " ").Trim();
            RightMapPieceLabel = connectedMaps[1].Replace(")", " ").Trim();
        }

        public void InitializeNetwork(Dictionary<string, MapPiece> mapPieces)
        {
            if (mapPieces.TryGetValue(LeftMapPieceLabel, out MapPiece? leftMapPiece))
            {
                LeftMapPiece = leftMapPiece;
            }
            if (mapPieces.TryGetValue(RightMapPieceLabel, out MapPiece? rightMapPiece))
            {
                RightMapPiece = rightMapPiece;
            }
        }
    }
}
