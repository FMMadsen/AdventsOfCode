using AdventOfCode2024Solutions.Day04;
using AdventOfCode2024Solutions.Day06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day16
{
    public class StringMap<T> where T : Enum
    {
        protected string[] MapValue;
        protected GameObject WorldValue = new();

        public Dictionary<T, Func<GameObject>> CharToTypeList;
        public Dictionary<char, T> CharToEnumList;
        public static StringMapD16 Empty { get { return new StringMapD16(); } }

        public string[] Map { get { return MapValue; } }

        public GameObject World { get{ return WorldValue; } }

        
        public Vector2I WidthAndHeight = Vector2I.Zero;

        public StringMap()
        {
            MapValue = Array.Empty<string>();
            CharToTypeList = new Dictionary<T, Func<GameObject>>();
            CharToEnumList = new Dictionary<char, T>();
        }

        public StringMap(string[] aMap)
        {
            MapValue = aMap;

            CharToTypeList = new Dictionary<T, Func<GameObject>>();
            CharToEnumList = new Dictionary<char, T>();

            WidthAndHeight = FindWidthAndHeight(aMap);
        }

        /// <summary>
        /// Width and Height are only used if not zero.
        /// They are set automatically, so overwrite
        /// post instantiation is zero is needed.
        /// </summary>
        public StringMap(string[] aMap, Dictionary<T, Func<GameObject>> aCharToTypeList, Dictionary<char, T> aCharToEnumList)
        {
            MapValue = aMap;

            CharToTypeList = aCharToTypeList;
            CharToEnumList = aCharToEnumList;

            WidthAndHeight = FindWidthAndHeight(aMap);

            
        }

        /// <summary>
        /// Use the first line length to find X
        /// and amount of lines for y.
        /// </summary>
        protected static Vector2I FindWidthAndHeight(string[] aMap)
        {
            return new Vector2I(aMap.First().Length, aMap.Length);
        }

        public char CharOf(Vector2I index)
        {
            return Map[index.Y][index.X];
        }

        public GameObject[] GetAllAt(Vector2I location)
        {
            return World.GetGrandChildrenAt(location);
        }

        public Ta[] GetAll<Ta>() where Ta : GameObject
        {
            List<Ta> gameObjects = World.GetChildren<Ta>().ToList();
            List<GameObject> children = World.Children.ToList();
            int childI = 0;

            while (childI < children.Count)
            {
                GameObject child = children[childI];
                if (0 < child.Children.Length)
                {
                    gameObjects.AddRange(child.GetChildren<Ta>());
                    children.AddRange(child.Children);
                }

                childI++;
            }

            return gameObjects.ToArray();
        }

        public Ta[] FindNeighborsOf<Ta>(Vector2I index, bool useDiagonal = false, bool onlyDiagonal = false) where Ta : GameObject
        {
            IEnumerable<Ta> neighbors = Enumerable.Empty<Ta>();
            GameObject? gameObject;
            T foundMapChar;
            Vector2I testPos;

            if (Map.Length < 1)
            {
                return neighbors.ToArray();
            }

            Vector2I[] neighborsTest = index.Neighbors(useDiagonal, onlyDiagonal);
            
            for (int i = 0; i < neighborsTest.Length; i++)
            {
                testPos = neighborsTest[i];
                
                if (-1 < testPos.X
                    && -1 < testPos.Y
                    && testPos.Y < Map.Length 
                    && testPos.X < Map.First().Length)
                {
                    foundMapChar = CharToEnumList[CharOf(testPos)]; 

                    if (CharToTypeList.TryGetValue(foundMapChar, out Func<GameObject>? value)) 
                    { gameObject = value.Invoke(); }
                    else { gameObject = null; continue; }

                    if (gameObject is Ta)
                    {
                        gameObject.Transform = new Transform( testPos, index - testPos);
                        neighbors = neighbors.Append((Ta)gameObject);
                    }
                }

                gameObject = null;
            }

            return neighbors.ToArray();
        }

        public void Destroy()
        {
            World.Destroy();
        }

        public void Spawn(GameObject aGameObject)
        {
            World.SpawnChild(aGameObject);
        }

        public void Destroy(GameObject aGameObject)
        {
            World.DestroyChild(aGameObject);
        }
    }
}
