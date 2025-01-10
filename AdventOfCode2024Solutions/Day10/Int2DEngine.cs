using AdventOfCode2024Solutions.Day16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day10
{
    public class Int2DEngine<MapChar> where MapChar : Enum
    {
        private StringMap<MapChar> MapValue = new StringMap<MapChar>();
        public StringMap<MapChar> Map { get { return MapValue; } }

        public Dictionary<MapChar, Func<GameObject>> CharToTypeList { get; set; }
        public Dictionary<char, MapChar> CharToEnumList { get; set; }

        private static LimitedValue<int> FrametimeValue = new LimitedValue<int>(10, 10000, 30);

        public static int Frametime {
            get { return FrametimeValue.Value; }
            set {  FrametimeValue.Value = value; }
        }

        private int PrintMapIndex = -1;
        private bool Running = false;
        private Task RenderTask = Task.CompletedTask;
        private static string[] Canvas = Array.Empty<string>();
        public static int ComputeDelay = 0;
        protected static bool Printing = false;


        public Int2DEngine(Dictionary<MapChar, Func<GameObject>> charToTypeList, Dictionary<char, MapChar> charToEnumList)
        {
            CharToTypeList = charToTypeList;
            CharToEnumList = charToEnumList;
        }

        public void LoadMap(string[] mapData)
        {
            MapValue = new StringMap<MapChar>(mapData.Select(a => a.TrimEnd()).ToArray(), CharToTypeList, CharToEnumList);
            Canvas = Map.Map.ToArray();
        }
    }
}
