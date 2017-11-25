using System;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry.MeshGeometry.Serialization 
{

	[Serializable]
	public class SerializableMesh
    {	
		public Vector3[] Vertices;

		[SerializeField]
		private int[] triangles;

		[SerializeField]
		private int[] subMeshes;

		public SerializableMesh () {}

		public SerializableMesh (Mesh mesh)
        {
			Vertices = mesh.vertices;
			List<int> trianglesList = new List<int> ();
			subMeshes = new int[mesh.subMeshCount];
			int submesh = 0;
			for (submesh = 0; submesh < subMeshes.Length; submesh++)
            {
				List<int> verticesInSubMesh = new List<int> (mesh.GetTriangles (submesh));
				subMeshes [submesh] = verticesInSubMesh.Count;
				trianglesList.AddRange (verticesInSubMesh);
			}
			triangles = trianglesList.ToArray ();
		}

		public static implicit operator Mesh(SerializableMesh source)
		{
			Mesh mesh = new Mesh ();
			mesh.SetVertices (new List<Vector3> (source.Vertices));
			
//			SetUVs(mesh);
			
			int s = 0;
			foreach (int verticesInSubMesh in source.subMeshes)
            {
				List<int> triangles = new List<int> ();
				for (int v = 0; v < verticesInSubMesh; v++)
                {
					triangles.Add(source.triangles[v]);
				}
				mesh.SetTriangles (triangles, s++);
			}
			return mesh;
		}

	    public static void SetUVs(Mesh mesh)
	    {
		    Vector2[] newUVs = new Vector2[mesh.vertexCount];
		    for (int i = 0; i < mesh.vertexCount; i++)
		    {
			    newUVs[i] = new Vector2(Mathf.Abs(mesh.vertices[i].x / 5 + 1000), Mathf.Abs(mesh.vertices[i].y / 5f + 1000));
		    }
		    mesh.SetUVs(0, new List<Vector2>(newUVs));
	    }
	}
}