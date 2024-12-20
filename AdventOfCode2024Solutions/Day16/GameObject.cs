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

        public Transform Transform { get; set; } = new Transform();

        public GameObject[] Children { get; set; } = Array.Empty<GameObject>();

        public virtual void Destroy()
        {
            foreach (var child in Children)
            {
                child.Destroy();
            }
        }

        public virtual void SpawnChild(GameObject aGameObject)
        {
            Children = Add(Children, aGameObject);
        }

        public virtual void DestroyChild(GameObject aGameObject)
        {
            Children = Remove(Children, aGameObject);
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
            Task.Run( () => { anObject.Destroy(); });
            return aList.Where(a => a.Id != anObject.Id).ToArray();
        }
    }
}
