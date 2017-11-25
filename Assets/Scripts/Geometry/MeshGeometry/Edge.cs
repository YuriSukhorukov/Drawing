using UnityEngine;

namespace Geometry.MeshGeometry
{
		
	public struct Edge
	{

		public readonly Vector2 a;
		public readonly Vector2 b;

        public Edge (Vector2 a, Vector2 b)
        {
            this.a = a;
            this.b = b;
        }

		public bool Equals (Edge edge)
		{
			return (a == edge.a && b == edge.b) || (a == edge.b && b == edge.a);
		}

		public override bool Equals (System.Object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Edge edge = (Edge)obj;
			if ((System.Object)edge == null)
			{
				return false;
			}

			return (a == edge.a && b == edge.b) || (a == edge.b && b == edge.a);
		}

		public override int GetHashCode ()
		{
			return a.GetHashCode() ^ b.GetHashCode();
		}

		public override string ToString ()
		{
			return string.Format("Edge [{0}, {1}] - [{2}, {3}]", a.x, a.y, b.x, b.y);
		}

	}
}