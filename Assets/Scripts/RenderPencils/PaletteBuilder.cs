using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class PaletteContainer
{
	public Palette[] Palettes;
}

[Serializable]
public class Palette
{
	public string name;
	public Color[] Colors;
	public Texture[] Textures;
	public Shader[] Shaders;
	public List<Material> Materials;
}

public static class PaletteFactory
{
	public static Palette CreatePalette(string paletteName, Color[] colors = null, Texture[] textures = null, Shader[] shaders = null)
	{
		Shader shaderColorMaterial = Shader.Find("Legacy Shaders/Diffuse");
		Shader shaderTextureMaterial = Shader.Find("Legacy Shaders/Diffuse");
		
		Palette palette = new Palette();

		palette.name = paletteName;
		palette.Colors = colors;
		palette.Textures = textures;
		palette.Shaders = shaders;
		palette.Materials = new List<Material>();
		
		if (palette.Colors != null)
		{
			for (int i = 0; i < palette.Colors.Length; i++)
			{
				palette.Materials.Add(new Material(shaderColorMaterial) {color = palette.Colors[i]});
			}
		}
		if (palette.Textures != null)
		{
			for (int i = 0; i < palette.Textures.Length; i++)
			{
				palette.Materials.Add(new Material(shaderTextureMaterial) {color = Color.white, mainTexture = palette.Textures[i]});
			}
		}
		if (palette.Shaders != null)
		{
			for (int i = 0; i < palette.Shaders.Length; i++)
			{
				palette.Materials.Add(new Material(palette.Shaders[i]));
			}
		}
		return palette;
	}
}

public class PaletteBuilder : MonoBehaviour
{
	public bool isOverride;
	public int selectPaletteIndex;
	public Palette[] Palettes;
	private Palette palette;

	public PaletteContainer PaletteContainer;
	
	// Use this for initialization
	void Awake ()
	{
		if (isOverride)
		{
			PaletteContainer = CreatePalettes();
		}
		else
		{
			PaletteContainer = LoadPalette();
		}

		PaletteManager.SelectPalette(Palettes[selectPaletteIndex].name);

		MaterialManager.Init();
		MaterialManager.InitMaterials();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.C))
			PaletteContainer = CreatePalettes();
		if(Input.GetKeyDown(KeyCode.L))
			PaletteContainer = LoadPalette();
	}

	//save json
	public PaletteContainer CreatePalettes()
	{
		PaletteContainer paletteContainer = new PaletteContainer();
		paletteContainer.Palettes = Palettes;
		
		if(PaletteManager.Palettes != null)
			PaletteManager.Palettes.Clear();
		
		foreach (Palette palette in paletteContainer.Palettes)
		{
			Palette _palette = PaletteFactory.CreatePalette(palette.name, palette.Colors, palette.Textures, palette.Shaders);
			PaletteManager.AddPalete(_palette);
		}

		string patch = "data.json";
		string filePath = Path.Combine(Application.streamingAssetsPath, patch);
		string dataAsJson = JsonUtility.ToJson(paletteContainer);
		File.WriteAllText(filePath, dataAsJson);
		
		Debug.Log("Palette saved");
		return paletteContainer;
	}

	//load from json
	public PaletteContainer LoadPalette()
	{
		string patch = "data.json";
		string filePath = Path.Combine(Application.streamingAssetsPath, patch);

		string dataAsJson = File.ReadAllText(filePath);
		PaletteContainer loadedData = JsonUtility.FromJson<PaletteContainer>(dataAsJson);
		Palettes = loadedData.Palettes;

		if (PaletteManager.Palettes != null)
			PaletteManager.Palettes.Clear();

		foreach (Palette palette in loadedData.Palettes)
		{
			Palette _palette = PaletteFactory.CreatePalette(palette.name, palette.Colors, palette.Textures, palette.Shaders);
			PaletteManager.AddPalete(_palette);
		}

		Debug.Log("Palette loaded");
		return loadedData;
	}
}
