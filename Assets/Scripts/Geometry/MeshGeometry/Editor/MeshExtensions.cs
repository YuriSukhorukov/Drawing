using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using Geometry;

namespace Geometry.MeshGeometry
{
	public static class MeshExtensions
    {

        // TODO: Проверить, где можно оптимизировать перфоманс: это самый ресурсоемкий метод
        public static List<Vector2[]> GetMeshContours (this Mesh mesh, double sizeTreshold)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
			List<Vector2[]> contoursList = new List<Vector2[]> ();

			// Списки граней меша
			HashSet<Edge> uniqueEdges = new HashSet<Edge>();
			HashSet<Edge> duplicateEdges = new HashSet<Edge>();

			List<Vector3> vertices = new List<Vector3> ();
			mesh.GetVertices(vertices);

			for (int sm = 0; sm < mesh.subMeshCount; sm++)
            {
				int[] triangles = mesh.GetTriangles (sm);
				for (int t = 0; t < triangles.Length; t += 3)
                {
					Vector2 p1 = vertices [triangles [t]]; 
					Vector2 p2 = vertices [triangles [t+1]];
					Vector2 p3 = vertices [triangles [t+2]];
                    updateEdgesList(uniqueEdges, duplicateEdges, new Edge(p1, p2));
                    updateEdgesList(uniqueEdges, duplicateEdges, new Edge(p2, p3));
                    updateEdgesList(uniqueEdges, duplicateEdges, new Edge(p3, p1));
				}
			}
			// В списке uniqueEdges - только существующие в единственном числе грани (т.е. внешние)
			uniqueEdges.ExceptWith(duplicateEdges);

			List<Edge> edges = uniqueEdges.ToList ();
			while (edges.Count > 0)
            {
				int i = 0;
				bool contourOpen = true;
				List<Vector2> contour = new List<Vector2> ();
				contour.Add(edges [i].a);
				Vector2 last = edges [i].b;

				while (edges.Count > 0 && contourOpen)
                {
					int edgeIndex = edges.FindIndex (e => (e.a == last || e.b == last));
					if (edgeIndex > -1)
                    {
						// Очередная точка для контура
						last = (edges [edgeIndex].a == last) ? edges [edgeIndex].b : edges [edgeIndex].a;
						contour.Add (last);
						edges.RemoveAt (edgeIndex);

						// Удаление дубликатов лишних вершин:
						if (contour.Count > 2)
                        {
							int j = contour.Count - 1; 
							Vector2 a = contour [j - 2];
							Vector2 b = contour [j - 1];
							Vector2 c = contour [j];
							if (Planimetry.IsLine (a, b, c))
                            {
								int excessPointIndex = (b - a).sqrMagnitude > (c - a).sqrMagnitude ? j : j - 1;
								contour.RemoveAt (excessPointIndex);
							}
						}
					}
                    else
                    {
						contourOpen = false;
					}
				}
                float square = polygonSquare(contour);
                if (contour.Count > 2 && square > sizeTreshold)
                {
					contoursList.Add (contour.ToArray());
				}
			}
			sw.Stop();
			Debug.Log(string.Format("\tGetMeshContours. Time elapsed: {0}ms", sw.ElapsedMilliseconds));

			return contoursList;
		}

        public static void FlipTriangles (this Mesh mesh)
        {
			int[] triangles = mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				int t = triangles[i];
				triangles[i] = triangles[i + 2];
				triangles[i + 2] = t;
			}
			mesh.triangles = triangles;
        }

        public static void FlipNormals (this Mesh mesh)
        {
			Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }
			mesh.normals = normals;
        }

        private static float polygonSquare (List<Vector2> poly)
        {
            float square = 0;
            if (poly.Count <= 2)
            {
                return square;
            }
            for (int i = 1; i < poly.Count - 1; i++)
            {
                square += poly[i].x * (poly[i + 1].y - poly[i - 1].y);
            }
            square += poly[0].x * (poly[1].y - poly[poly.Count - 1].y);
            square += poly[poly.Count - 1].x * (poly[0].y - poly[poly.Count - 2].y);
            return Mathf.Abs(square) * 0.5f;
        }

		private static void updateEdgesList (HashSet<Edge> uniqueEdges, HashSet<Edge> dublicateEdges, Edge edge)
        {
			if (!uniqueEdges.Add (edge))
            {
				dublicateEdges.Add (edge);
			};
		}
	}
}

