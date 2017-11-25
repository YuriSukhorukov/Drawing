using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColoringBook.Serialization;
using Content;
using Utils;

namespace ColoringBook
{
	
	[RequireComponent(typeof (MeshFilter))]
	public class Image : MonoBehaviour 
	{

		private MeshFilter meshFilter;
		private MeshRenderer meshRenderer;

		private Page currentPage;
		public Page CurrentPage
		{
			set
			{
				DebugLog.LogFormat("Loading page {0}", value.Alias);
                LoadAsset(value.ImageData);
			}
		}

		public Material ContourMaterial;

		private void Awake ()
		{
			meshFilter = GetComponent<MeshFilter> ();
			gameObject.AddComponent<MeshRenderer> ();
		}

		private void Start()
		{
			List<Color> Colors = new List<Color>();
			//meshFilter.mesh.colors = Colors.ToArray();

			StartCoroutine(deel1());
		}

		private bool isPrint = false;
		public float distance;


	    // TODO: Навести порядок
		public GameObject Prefab;
		public float p;
		IEnumerator deel1()
		{
			yield return new WaitForSeconds(0.5f);
			
				
			for (int i = 0; i < meshFilter.mesh.uv.Length; i++)
			{
				//print(meshFilter.mesh.uv[i]);
			}
			//print(meshFilter.mesh.uv[0]);
			//meshFilter.mesh.uv = new Vector2[meshFilter.mesh.vertices.Length];
			int count = 0;
			for (int i = 0; i < meshFilter.mesh.vertexCount-1; i++)
			{
				//print(meshFilter.mesh.triangles.Length);
				//meshFilter.mesh.uv[i] = new Vector2(1, 0.01f);
				//p = Vector3.Distance(meshFilter.mesh.vertices[i], meshFilter.mesh.vertices[i + 1]);
				if (p > distance)
				{
					//print(meshFilter.mesh.vertices[i]);
					//GameObject point = GameObject.Instantiate(Prefab);
					//point.transform.position = new Vector3(meshFilter.mesh.vertices[i+1].x, meshFilter.mesh.vertices[i+1].y, meshFilter.mesh.vertices[i+1].z);
					//count += 1;
				}
				//meshFilter.mesh.bounds[0]
			}
			int i1 = 0;
			while (i1 < meshFilter.mesh.triangles[i1])
			{	
				//print(meshFilter.mesh.triangles[i1]);
				i1++;
			}
			print(count);
			//meshFilter.mesh.colors = new Color[1];
			//print(meshFilter.mesh.colors.Length);
			isPrint = true;
		}

		public void LoadAsset (ImageData data)
		{
		    if (data == null)
		    {
		        throw new ArgumentNullException("data");
		    }
		    System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew ();

			meshFilter.mesh = data.Contour;

			meshRenderer = GetComponent<MeshRenderer> ();
			meshRenderer.material = ContourMaterial;

			foreach (RegionData region in data.Regions)
			{
				CreateRegion (region);
			}

			sw.Stop ();
		}

		private void CreateRegion (RegionData region)
		{
            string regionName = "INNER_MESH_" + region.ID;

			GameObject go = GameObject.Find (regionName);
			if (go == null)
			{
				go = new GameObject(regionName);
				go.AddComponent<Region> ();
			}
			go.transform.SetParent (gameObject.transform, false);
			Region cr = go.GetComponent<Region> ();

			cr.Data = region;
		}

	}

}
