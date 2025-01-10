using AdventOfCode2024Solutions.Day16;
using Common;

namespace AdventOfCode2024Solutions.Day10
{
    public enum MapCharD10
    {

    }


    public class Solution : Int2DEngine<MapCharD16>, IAOCSolution
    {
        public string PuzzleName => "Day 10: ";

        public Dictionary<MapCharD16, Func<GameObject>> CharToTypeList = new Dictionary<MapCharD16, Func<GameObject>>() {
            { MapCharD16.Empty, ()=>{return new Walkable(); } },
            { MapCharD16.Wall, ()=>{return new Wall(); } },
            { MapCharD16.Spawn, ()=>{return new Spawn(); } },
            { MapCharD16.Exit, ()=>{return new Mapexit(); } }
        };
        public Dictionary<char, MapCharD16> CharToEnumList = new Dictionary<char, MapCharD16>(){
            {'.', MapCharD16.Empty },
            {'#', MapCharD16.Wall},
            {'S', MapCharD16.Spawn},
            {'E', MapCharD16.Exit}
        };

        public string SolvePart1(string[] datasetLines)
        {
            return "To be implemented";
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
