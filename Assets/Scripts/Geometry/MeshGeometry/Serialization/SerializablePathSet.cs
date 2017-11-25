using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geometry.MeshGeometry.Serialization
{

	[Serializable]
	public class SerializablePathSet
	{

        [SerializeField]
        private List<SerializablePath> paths;

		private List<Vector2[]> verticesList;
		public List<Vector2[]> AsList
        {
			get {
				if (verticesList == null)
                {
                    verticesList = paths.Select(p => p.Vertices).ToList();
				}
				return verticesList;
			}
		}

		public SerializablePathSet () {}

		public SerializablePathSet (IEnumerable<Vector2[]> pathSet)
        {
            paths = new List<SerializablePath>();
            int i = 0;
            foreach (Vector2[] path in pathSet)
            {
                paths.Add(new SerializablePath(i++, path));
            }
		}

        public Vector2[] GetPath (int id)
        {
            return paths[id].Vertices;
        }

        public List<Vector2[]> GetPaths (List<int> ids)
        {
            return paths
                .Where(path => ids.Contains(path.ID))
                .Select(path => path.Vertices)
                .ToList();
        }

		public static implicit operator List<Vector2[]> (SerializablePathSet pathSet)
        {
			return pathSet.AsList;
		}
	}
}
