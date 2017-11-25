using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Geometry.MeshGeometry {

	public class MeshContourTree
	{

		private readonly List<MeshContour> members = new List<MeshContour> ();

		public int Levels { get; private set; }

		public MeshContourTree () {}

		public MeshContourTree (List<Vector2[]> contours)
		{
			int x, y;
			bool[,] inclusion = new bool[contours.Count, contours.Count]; // inclusion [x, y] = входит ли x в y
			int[] inclusionLevel = new int[contours.Count];
			for (x = 0; x < contours.Count; x++)
			{
				inclusionLevel[x] = 0;
				for (y = 0; y < contours.Count; y++)
				{
					inclusion[x, y] = false;
				}
			}

			x = 0;
			y = 0;

			// Матрица смежности
			foreach (Vector2[] inner in contours)
			{
				foreach (Vector2[] outer in contours)
				{
					if (inner[0] != outer[0] && inner[1] != outer[1])
					{
						inclusion[x, y] = Planimetry.IsPointInPolygon(ref inner[0], outer);
						inclusionLevel[x] += inclusion[x, y] ? 1 : 0;
					}
					y++;
				}
				Add(x, inclusionLevel[x]);
				x++;
				y = 0;
			}

			for (int l = 1; l < Levels; l++)
			{
				List<MeshContour> byLevel = FindByLevel(l);
				List<MeshContour> parents = FindByLevel(l - 1);
				foreach (MeshContour contour in byLevel)
				{
					MeshContour parent = parents.Find(potentialParent => inclusion[contour.ID, potentialParent.ID]);
					contour.AppendTo(parent);
				}
			}
		}

		public void Add (int id, int level)
		{
			MeshContour newContour = new MeshContour(this, id, level);
			Levels = Mathf.Max (Levels, level);
			members.Add (newContour);
		}

		public MeshContour FindById (int id)
		{
			return members.Find(member => member.ID == id);
		}

		public List<MeshContour> FindByLevel (int level)
		{
			return members.FindAll (member => member.Level == level);
		}

		public MeshContour FindParent (int children)
		{
			return members.Find (member => member.HasChildren (children));
		}

		public List<MeshContour> FindAllOdd ()
		{
			return members.FindAll (member => member.IsOdd);
		}

		public List<MeshContour> FindAllEven ()
		{
			return members.FindAll (member => member.IsEven);
		}

		public List<List<int>> GetInnerFaces ()
		{
			List<List<int>> facesList = new List<List<int>>();

			FindAllOdd().ForEach(node => {
				List<int> contoursInFace = new List<int>(node.Children);
				contoursInFace.Add(node.ID);
				facesList.Add(contoursInFace);
			});

			return facesList;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			foreach (MeshContour member in FindByLevel(0)) {
				sb.Append (member);
				sb.Append ("\n");
			}
			return sb.ToString ();
		}

	}

}
	