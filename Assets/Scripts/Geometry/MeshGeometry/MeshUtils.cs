using System.Collections.Generic;
using SVGImporter.LibTessDotNet;
using UnityEngine;
#if UNITY_EDITOR
//using UnityEditor;
#endif

namespace Geometry.MeshGeometry 
{

	public static class MeshUtils 
	{

		public static UnityEngine.Mesh BuildMesh (List<Vector2[]> contours)
		{

            Tess tesselation = new Tess();
			foreach (Vector2[] contour in contours)
			{
				if (contour == null)
					continue;

				int pathLength = contour.Length;
				ContourVertex[] path = new ContourVertex[pathLength];
				for(int j = 0; j < pathLength; j++)
				{
					Vector2 position = contour[j];
					path[j].Position = new Vec3{X = position.x, Y = position.y, Z = 0f};
				}
				tesselation.AddContour(path);
			}

			tesselation.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);

			UnityEngine.Mesh mesh = new UnityEngine.Mesh();
			int meshVertexCount = tesselation.Vertices.Length;
			Vector3[] vertices = new Vector3[meshVertexCount];

			for(int i = 0; i < meshVertexCount; i++)
			{
				vertices[i] = new Vector3(tesselation.Vertices[i].Position.X, tesselation.Vertices[i].Position.Y, 0f);
			}

			int numTriangles = tesselation.ElementCount;
			int[] triangles = new int[numTriangles * 3];
			for (int i = 0; i < numTriangles; i++)
			{
				triangles[i * 3] = tesselation.Elements[i * 3];
				triangles[i * 3 + 1] = tesselation.Elements[i * 3 + 1];
				triangles[i * 3 + 2] = tesselation.Elements[i * 3 + 2];
			}

			mesh.vertices = vertices;
			mesh.triangles = triangles;

			return mesh;
		}

	}
}
