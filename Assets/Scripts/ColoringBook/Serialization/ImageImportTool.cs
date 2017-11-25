using UnityEngine;

namespace ColoringBook.Serialization
{
		
	// TODO: А нужен ли этот класс вообще
	public class ImageImportTool : MonoBehaviour
	{

        public ImageStarter GameContainer;
        public string Folder = "New Coloring Book Shelf";
		public string Name = "New Coloring Book";

        [Range(0.0f, 10.0f)]
        public float RegionSizeTreshold = 0.1f;

        public Mesh SourceMesh;

	    public Texture2D PreviewTexture;

	}

}
