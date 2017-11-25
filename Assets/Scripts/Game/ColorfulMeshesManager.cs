using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ColorfulMeshesManager : MonoBehaviour
	{
		private static ColorfulMeshesManager _instance;

		public static ColorfulMeshesManager Instance
		{
			get
			{
				return _instance;
			}
			set
			{
				if (_instance == null)
				{
					_instance = value;
				}
			}
		}

		public Dictionary<ColorfulMesh, Material> ColoredRegions;
	
		// Use this for initialization
		void Awake ()
		{
			Instance = this;
			ColoredRegions = new Dictionary<ColorfulMesh, Material>();
		}

		public void SetRegionToDictionary(ColorfulMesh region, Material material)
		{
			Material _material;
			if (material != MaterialManager.ZeroMaterial)
			{
				if (!ColoredRegions.TryGetValue(region, out _material))
				{
					ColoredRegions.Add(region, material);
				}
			}
			else if(material == MaterialManager.ZeroMaterial)
			{
				if (ColoredRegions.TryGetValue(region, out _material))
				{
					ColoredRegions.Remove(region);
				}
			}
		}

		public void ResetColoredRegions()
		{
			foreach (ColorfulMesh colorfulMesh in ColoredRegions.Keys)
			{
				colorfulMesh.MaterialID = 0;
			}
		}
	}
}
