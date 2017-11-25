using UnityEngine;
using System.IO;

public class RTextureToIMG : MonoBehaviour {
	public RenderTexture RT;
	
	private int width;
	private int height;

	private void Start()
	{
		width = RT.width;
		height = RT.height;
		Invoke("CreatePNG", 0.2f);
	}
	private void CreatePNG()
	{
		Texture2D myTexture = toTexture2D(RT);
		byte[] bytes = myTexture.EncodeToPNG();
		
		Directory.CreateDirectory("Assets/Resources/AtlasesBaked/"+PaletteManager.selectedPaletteName);
		System.IO.File.WriteAllBytes ("Assets/Resources/AtlasesBaked/" + PaletteManager.selectedPaletteName + "/" + PaletteManager.selectedPaletteName + ".png", bytes);
	}

	Texture2D toTexture2D(RenderTexture rTex)
	{
		Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
		RenderTexture.active = rTex;
		tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
		tex.Apply();
		return tex;
	}
}
