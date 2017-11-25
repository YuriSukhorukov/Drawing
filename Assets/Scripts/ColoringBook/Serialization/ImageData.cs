using System.Collections.Generic;
using UnityEngine;

using Geometry.MeshGeometry;
using Geometry.MeshGeometry.Serialization;

namespace ColoringBook.Serialization {

	// TODO: Можно проводить распаковку сразу после загрузки: https://docs.unity3d.com/ScriptReference/ScriptableObject.OnEnable.html

	public class ImageData : ScriptableObject
    {

		// 3D-меш для рендеринга контура
		[SerializeField]
		private SerializableMesh contour;
		public Mesh Contour {
			get { return contour; }
			set { contour = new SerializableMesh (value); }
		}

		// Список внешних контуров региона
		[SerializeField]
		private SerializablePathSet imagePaths;
        public SerializablePathSet ImagePaths { get { return imagePaths; } }
        public List<Vector2[]> RawImagePaths
        {
            set { imagePaths = new SerializablePathSet(value); }
        }

        // Список регионов
        [SerializeField]
        private List<RegionData> regions;
		public List<RegionData> Regions
        {
			get
			{
				if (regions == null)
				{
					regions = new List<RegionData>();
				}
				return regions; 
			}
        }

        public void AddRegion (List<int> paths)
        {
            Regions.Add(
                new RegionData(Regions.Count, this, paths)
            );
        }
	}

	[System.Serializable]
	public class RegionData
    {
        public ImageData Owner;

        public SerializablePathSet PathsLibrary
        {
            get { return Owner.ImagePaths; }
        }

        // Номер региона
		public int ID = 0;

        // Индексы контуров, ограничивающих регион
	    [SerializeField]
	    private List<int> paths;

		// Цвет региона
		public Color32 DefaultColor = Color.clear;

		// Вершины контуров, ограничивающих регион
        public List<Vector2[]> Paths
        {
            get { return PathsLibrary.GetPaths(paths); }
        }

		// 3D-меш для рендеринга региона
		[SerializeField]
		private SerializableMesh mesh;
		public Mesh RegionMesh {
			get { return mesh; }
			set { mesh = new SerializableMesh (value); }
		}

        public RegionData (int id, ImageData owner, List<int> paths) {
			ID = id;
            Owner = owner;
            this.paths = paths;
            RegionMesh = MeshUtils.BuildMesh(Paths);
		}
	}

}
