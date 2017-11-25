using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialManager {

	private static List<Color> colors;

	public static List<Color> Colors {
		get {
			if (!initialized) {
				Init ();
			}
			return colors;
		}
	}

	private static bool initialized = false;

	private static Material[] materials;

	public static Material[] Materials {
		get { 
			if (!initialized) {
				Init();
			}
			return materials;
		}
		set { materials = value; }
	}

	private static int active;
	public static int Active {
		get { return active; }
		set { active = Mathf.Clamp (value, 0, materials.Length - 1);}
	}
	public static Color ActiveColor {
		get { return Colors [active]; }
	}
	public static Material ActiveMaterial {
		get { return Materials [active]; }
	}

	public static void Init () {
		//colors = PaletteManager.CreatePalette ();
		//PaletteManager.CreatePalette ();
		//PaletteManager.SelectPalette();
		
		InitMaterials ();
		//active = Mathf.Min(1, colors.Count - 1);
		active = Mathf.Min(1, materials.Length - 1);
	}

	public static void InitMaterials () {
		// shader = Shader.Find("Legacy Shaders/Diffuse");

		//materials = PaletteManager.Palettes["palette1"].Materials;//new Material[colors.Count];
		//for (var i = 0; i < colors.Count; i++) {
			//materials[i] = new Material(shader) {color = colors[i]};
		//}
		initialized = true;
		Debug.Log(materials.Length);
	}

	private static int number;

	public static Material NextMaterial {
		get { 
			number++;
			if (number >= Materials.Length - 1) {
				number = 0;
			}
			return Materials [number];
		}
	}

	public static Material ZeroMaterial {
		get {
			return Materials [0];
		}
	}

}
