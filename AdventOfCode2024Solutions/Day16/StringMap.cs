using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day16
{
    public class StringMap
    {
        protected string[] MapValue;
        protected GameObject?[][] GameObjectsValue;

        public string[] Map { get { return MapValue; } }
        public GameObject[] GameObjects { get; set; } = Array.Empty<GameObject>();

        /// <summary>
        /// Width and Height are only used if not zero.
        /// They are set automatically, so overwrite
        /// post instantiation is zero is needed.
        /// </summary>
        public Vector2I WidthAndHeight = Vector2I.Zero;

        public StringMap(string[] aMap)
        {
            MapValue = aMap;

            WidthAndHeight = FindWidthAndHeight(aMap);

            GameObjectsValue = new GameObject[WidthAndHeight.Y][];
            for(int y = 0; y < WidthAndHeight.Y; y++)
            {
                GameObjectsValue[y] = new GameObject[WidthAndHeight.X];
            }
        }

        /// <summary>
        /// Use the first line length to find X
        /// and amount of lines for y.
        /// </summary>
        protected static Vector2I FindWidthAndHeight(string[] aMap)
        {
            return new Vector2I(aMap.First().Length, aMap.Length);
        }

        public char CharOF(Vector2I index)
        {
            return Map[index.Y][index.X];
        }

        public void Spawn(GameObject aGameObject)
        {
            GameObjects = GameObject.Add(GameObjects, aGameObject);
        }

        public void Destroy(GameObject aGameObject)
        {
            GameObjects = GameObject.Remove(GameObjects, aGameObject);
        }
    }
}
