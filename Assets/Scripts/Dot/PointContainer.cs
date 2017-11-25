using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointContainer : MonoBehaviour
{
	public List<Transform> Points;
	public Material[] Materials;
	public Transform selectedPoint;
	public int selectedDotIndex;

	public void AddPoint(Transform go)
	{
		Points.Add(go);
		UpdateDotsNames();
		UpdateDotsColors();
		SelectPoint(go);
	}

	public void Insert(Transform go, int _index)
	{
		Points.Insert(_index, go.transform);
		UpdateDotsNames();
		UpdateDotsColors();
		SelectPoint(go);
	}

	public void SelectPoint(Transform go)
	{
		UpdateDotsColors();
		go.GetComponent<MeshRenderer>().material = Materials[1];
		go.GetComponentInChildren<TextMesh>().color = Materials[1].color;
		selectedPoint = go;
		selectedDotIndex = Points.IndexOf(selectedPoint.transform);
	}

	public void RemovePoint(Transform go)
	{	
		Transform tr = go.transform;

		int prevIndex = Points.IndexOf(tr) - 1 >= 0 ? Points.IndexOf(tr) - 1 : Points.Count - 1;
		SelectPoint(Points[prevIndex]);
		
		Points.Remove(tr);
		Destroy(tr.gameObject);
		UpdateDotsNames();
	}

	public void UpdateDotsColors()
	{
		foreach (Transform tr in Points)
		{
			tr.GetComponent<MeshRenderer>().material = Materials[0];
			tr.GetComponentInChildren<TextMesh>().color = Materials[0].color;
		}
	}
	
	private void UpdateDotsNames()
	{
		int i = 0;
		int j = 1;
		while (i < Points.Count)
		{
			Points[i].name = "#" + j;
			Points[i].GetComponentInChildren<TextMesh>().text = j.ToString();
			i += 1;
			j += 1;
		}
	}
}
