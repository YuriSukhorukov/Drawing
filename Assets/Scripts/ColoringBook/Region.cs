using System.Collections.Generic;
using UnityEngine;
using ColoringBook.Serialization;
using Utils;

namespace ColoringBook {
	
	[RequireComponent(typeof (MeshFilter))]
	[RequireComponent(typeof (PolygonCollider2D))]
	[RequireComponent(typeof (ColorfulMesh))]

	public class Region : MonoBehaviour
	{
		
		private MeshFilter mFilter;
		private PolygonCollider2D pCollider;

        private Mesh mesh
        {
            set
            {
                if (value != null)
                {
					mFilter.mesh = value;
				}
				else
				{
					DebugLog.LogWarning("No mesh");
				}
            }
        }

        private List<Vector2[]> colliderPaths
        {
            set
            {
				if (value != null)
				{
					pCollider.pathCount = value.Count;
					int i = 0;
					foreach (Vector2[] path in value)
					{
						pCollider.SetPath(i++, path);
					}
				}
				else
				{
					DebugLog.LogWarning("No vertices");
				}
            }
        }

		public RegionData Data
		{
			set {
                mesh = value.RegionMesh;
                colliderPaths = value.Paths;
			}
		}

		private void Awake ()
		{
			mFilter = GetComponent<MeshFilter> ();
			pCollider = GetComponent<PolygonCollider2D> ();
		}
		
	}
}