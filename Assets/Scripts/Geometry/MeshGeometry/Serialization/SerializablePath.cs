using System;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry.MeshGeometry.Serialization
{
    [Serializable]
    public class SerializablePath
    {
        public int ID = 0;

        [SerializeField]
        private Vector2[] vertices;
        public Vector2[] Vertices { get { return vertices; } }

        private List<Vector2> verticesList;
        public List<Vector2>  AsList
        {
            get {
                if (verticesList == null)
                {
                    verticesList = new List<Vector2>(vertices);
                }
                return verticesList;
            }
        }

        public SerializablePath () {}

        public SerializablePath (int id, Vector2[] path)
        {
            ID = id;
            vertices = path;
        }

        public static implicit operator List<Vector2> (SerializablePath path)
        {
            return path.AsList;
        }

		public static implicit operator Vector2[] (SerializablePath path)
		{
			return path.Vertices;
		}
    }
}
