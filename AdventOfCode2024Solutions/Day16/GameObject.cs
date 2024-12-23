using AdventOfCode2024Solutions.Day04;
using AdventOfCode2024Solutions.Day06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day16
{
    public class GameObject
    {
        public Guid Id { get; set; } = Guid.Empty;
        public GameObject? Parent { get; set; } = null;
        
        public Transform Transform { get; set; } = new Transform();
        protected GameObject[] ChildrenValue = Array.Empty<GameObject>();

        public GameObject[] Children { get{ return ChildrenValue; } }

        public GameObject[] GetChildrenAt(Vector2I location)
        {
            return Children.Where(a=> a.Transform.Location == location).ToArray();
        }

        public GameObject[] GetGrandChildrenAt(Vector2I location)
        {
            List<GameObject> gameObjects = GetChildrenAt(location).ToList();
            List<GameObject> children = Children.ToList();
            int childI = 0;

            while (childI < children.Count)
            {
                GameObject child = children[childI];

                if (0 < child.Children.Length)
                {
                    gameObjects.AddRange(child.GetChildrenAt(location));
                    children.AddRange(child.Children);
                }

                childI++;
            }

            return gameObjects.ToArray();
        }

        public virtual void Destroy()
        {
            Parent = null;

            foreach (var child in Children)
            {
                child.Destroy();
            }
        }

        public virtual void SpawnChild(GameObject aGameObject)
        {
            aGameObject.Parent = this;

            lock (ChildrenValue)
            {
                ChildrenValue = Add(Children, aGameObject);
            }
        }

        public virtual void DestroyChild(GameObject aGameObject)
        {
            lock(ChildrenValue)
            {
                ChildrenValue = Remove(Children, aGameObject);
            }
        }

        public static GameObject[] Add(GameObject[] aList, GameObject anObject)
        {
            if (anObject.Id == Guid.Empty)
            {
                anObject.Id = Guid.NewGuid();
            }
            return aList.Append(anObject).ToArray();
        }

        public static GameObject[] Remove(GameObject[] aList, GameObject anObject)
        {
            Task.Run( anObject.Destroy);
            return aList.Where(a => a.Id != anObject.Id).ToArray();
        }

        public T? GetLastChild<T>() where T : GameObject
        {
            if (0 < Children.Length)
            {
                for (int i = Children.Length - 1; 0 <= i; i--)
                {
                    if (Children[i] is T) { return (T)Children[i]; }
                }
            }

            return default(T);
        }
        public GameObject[] GetChildren()
        {
            return Children.ToArray();
        }

        public T[] GetChildren<T>() where T : GameObject
        {
            return Children.Where(a => a is T).Select(a=> (T)a ).ToArray();
        }
    }
}
