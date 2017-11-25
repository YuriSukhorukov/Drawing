using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PencilPrototype
{
	protected GameObject Prototype;
	public GameObject RootGameObject;
	
	public MeshRenderer MeshRenderer;
	public MeshFilter MeshFilter;

	protected void CreatePrototype()
	{
		RootGameObject = Object.Instantiate(Prototype);
	}

	protected void GetComponents()
	{
		MeshFilter = RootGameObject.GetComponent<MeshFilter>();
		MeshRenderer = RootGameObject.GetComponent<MeshRenderer>();
	}

	public void ChildTo(Transform transform)
	{
		RootGameObject.transform.parent = transform;
	}

	public void ResetPos()
	{
		RootGameObject.transform.position = Vector3.zero;
	}
}

public class Pencil: PencilPrototype
{
	public Color Color;

	public Pencil(GameObject _prototype, Material material)
	{
		base.Prototype = _prototype;
		CreatePrototype();
		GetComponents();
		MeshRenderer.material = material;
	}
}

public static class PencilsFabric
{
	public static Pencil CreatePencilColor(GameObject _prototype, Material material)
	{
		Pencil pencilColor = new Pencil(_prototype, material);
		return pencilColor;
	}
}

public class ColorSelectorMeshBuilder : MonoBehaviour
{
	public GameObject Prototype;

	private int TotalPencilsCount;
	private List<Pencil> Pencils;
	
	private void Start()
	{	
		TotalPencilsCount = MaterialManager.Materials.Length;
		Pencils = new List<Pencil>();
		
		int i = 0;
		while (i < TotalPencilsCount)
		{
			Pencil pc = PencilsFabric.CreatePencilColor(Prototype, MaterialManager.Materials[i]);
			Pencils.Add(pc);
			pc.ResetPos();

			ScalePencil(pc.RootGameObject, i);
			EditUVs(pc.MeshFilter.mesh);
			i++;
		}
	}

	private void ScalePencil(GameObject pc, int i)
	{
		Camera camera = Camera.main;

		float offset = 0.01f;
		
		float scaleCoeff = (camera.orthographicSize * 2) / TotalPencilsCount;
		pc.transform.localScale = new Vector3(scaleCoeff - offset, scaleCoeff - offset, 0.1f);

		float aspect = 5f;
		float posX = -camera.orthographicSize + ((pc.transform.lossyScale.x + offset) * i) +
		             pc.transform.lossyScale.x / 2;
		float posY = -camera.orthographicSize + aspect * (pc.transform.lossyScale.x / 2);
		pc.transform.position = new Vector3(posX, posY, 0);
	}

	private void EditUVs(Mesh mesh)
	{
		float aspect = 5f;
		List<Vector2> newUVs;
		newUVs = new List<Vector2>();
		for (int i = 0; i < mesh.uv.Length; i++)
		{
			newUVs.Add(new Vector2(mesh.uv[i].x / aspect, mesh.uv[i].y));
		}
		mesh.SetUVs(0, newUVs);
	}
}
