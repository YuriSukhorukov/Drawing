using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

[RequireComponent(typeof (MeshRenderer))]
public class ColorfulMesh : MonoBehaviour {

	private MeshRenderer mRenderer;
	private int materialID = 0;
	public int MaterialID {
		get { return materialID; }
		set {
			materialID = value;
			ActiveMaterial = MaterialManager.Materials[materialID];
		}
	}
	public Material ActiveMaterial {
		set { mRenderer.material = value; }
	}

	void Awake () {
		mRenderer = GetComponent<MeshRenderer> ();
		mRenderer.material = MaterialManager.ZeroMaterial;
	}

	// Non-NGUI message receiver
	public void Click () {
		OnClick ();
	}

	// NGUI Event
	public void OnClick ()
	{
		MaterialID = (MaterialID == MaterialManager.Active) ? 0 : MaterialManager.Active;
		ColorfulMeshesManager.Instance.SetRegionToDictionary(this, MaterialManager.Materials[MaterialID]);
	}

}
