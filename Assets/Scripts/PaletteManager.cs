using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PaletteManager
{
	public static Dictionary<string, Palette> Palettes;
	public static string selectedPaletteName;

	private static List<string> palettesNames;

	public static void SelectPalette(string palleteName)
	{
		selectedPaletteName = palleteName;
		MaterialManager.Materials = Palettes[palleteName].Materials.ToArray();
	}

	public static void SelectPalette(int index)
	{
		selectedPaletteName = palettesNames[index];
		MaterialManager.Materials = Palettes[palettesNames[index]].Materials.ToArray();
	}

	public static void AddPalete(Palette palette)
	{
		Palettes = Palettes ?? new Dictionary<string, Palette>();
		palettesNames = palettesNames ?? new List<string>();
		
		Palettes.Add(palette.name, palette);
		palettesNames.Add(palette.name);
	}
}
