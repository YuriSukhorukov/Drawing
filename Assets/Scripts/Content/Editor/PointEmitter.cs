using UnityEditor;
using UnityEngine;

public class PointEmitter : MonoBehaviour
{
    public string PictureName;
	public GameObject PointPrefab;
	public GameObject PointContainer;
	
	private PointContainer _pointContainer;
	private bool _isInsert;

	private void Awake()
	{
		_pointContainer = PointContainer.GetComponent<PointContainer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			if (_pointContainer.Points.Count == 0)
			{
				NewDot(mousePosition);
			}
			else
			{
				bool isSelected = false;
				TrySelectDot(ref isSelected, mousePosition);	
				if (!isSelected)
				{
					NewDot(mousePosition);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.D))
		{
			DeleteDot();
			print("dot deleted");
		}
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			SavePicture();
			print("prefab saved");
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			_isInsert = _isInsert ? DeactivateInsertMode() : ActivateInsertMode(); 
			print("insert mode is: " + _isInsert);
		}
	}
	
	private void NewDot(Vector3 mousePosition)
	{
		mousePosition.z = gameObject.transform.position.z;
		GameObject point =
			Instantiate(PointPrefab, Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);
		point.transform.position = mousePosition;
		point.transform.parent = PointContainer.transform;
		
		if (_isInsert && _pointContainer.selectedPoint != null)
		{
			_pointContainer.Insert(point.transform, _pointContainer.Points.IndexOf(_pointContainer.selectedPoint.transform)+1);
		}
		else
		{
			_pointContainer.AddPoint(point.transform);
		}
	}

	private void TrySelectDot(ref bool isSelected, Vector3 mousePosition)
	{
		foreach (Transform p in _pointContainer.Points)
		{
			if (Vector3.Distance(p.position, mousePosition) < .35)
			{
				_pointContainer.SelectPoint(p);
				isSelected = true;
				break;
			}
		}
	}
	
	private bool ActivateInsertMode()
	{
		return true;
	}

	private bool DeactivateInsertMode()
	{
		_pointContainer.SelectPoint(_pointContainer.Points[_pointContainer.Points.Count - 1]);
		return false;
	}

	private void DeleteDot()
	{
		if (_pointContainer.selectedPoint != null)
		{
			_pointContainer.RemovePoint(_pointContainer.selectedPoint);
		}
	}

	private void SavePicture()
	{
		PrefabUtility.CreatePrefab("Assets/Resources/Dot/DotPictures/" + PictureName + "Dots.prefab", PointContainer);
	}
}
