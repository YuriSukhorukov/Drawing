using System.Collections.Generic;
using System.Text;

namespace Geometry.MeshGeometry
{
		
	public class MeshContour
	{

		private readonly MeshContourTree graph;

		public int ID { get; private set; }

		public HashSet<int> Children = new HashSet<int>();

		public int Level = 0;

		public int Parent = -1;

		public bool IsEven { get { return Level % 2 == 0; } }

		public bool IsOdd { get { return Level % 2 != 0; } }

		public MeshContour (MeshContourTree graph, int id)
		{
			this.graph = graph;
			ID = id;
		}
		
		public MeshContour (MeshContourTree graph, int id, int level) : this(graph, id)
		{
			Level = level;
		}

		public bool HasChildren (int id) {
			return Children.Contains (id);

		}

		public void AppendTo (MeshContour parent)
		{
			if ((object) parent == null)
			{
				return;
			}
			Parent = parent.ID;
			parent.Children.Add (ID);
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append("\n");
			for (int l = 0; l < Level; l++)
			{
				sb.Append ("\t");
			}
			sb.Append (ID);
			foreach (int children in Children) {
				sb.Append (graph.FindById (children));
			}
			return sb.ToString ();
		}
	}

}
