using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePainter : MonoBehaviour
{
    public PointContainer PointContainer;
    public int LoadDotsCount;
    private Transform _selectedDot;
    private Transform _targetDot;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        PointContainer = FindObjectOfType<PointContainer>();
        PointContainer.SelectPoint(PointContainer.Points[0]);
        _lineRenderer = GetComponent<LineRenderer>();
        _selectedDot = PointContainer.selectedPoint;
        _targetDot = PointContainer.Points[1];
        _lineRenderer.SetPosition(0, PointContainer.Points[0].position);  
    }

    private void Start()
    {
        LoadProgress();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PointContainer.Points.Count > 0)
            {
                TrySelectDot();	
            }
        }
    }

    private void TrySelectDot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (Transform p in PointContainer.Points)
        {
            if (Vector3.Distance(p.position, mousePosition) < .35)
            {
                PointContainer.SelectPoint(p);
                _selectedDot = PointContainer.selectedPoint;
                if (_selectedDot.name == _targetDot.name)
                {
                    DrawLineToDot();
                }
                break;
            }
        }
    }

    private void DrawLineToDot()
    {
        int index = PointContainer.Points.IndexOf(_selectedDot.transform);
        _lineRenderer.positionCount += 1;
        _lineRenderer.SetPosition(index, PointContainer.Points[index].position);

        int targetIndex = index + 1 < PointContainer.Points.Count ? index + 1 : 0;
        _targetDot = PointContainer.Points[targetIndex];
        
        if (_targetDot.name == PointContainer.Points[0].name)
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, PointContainer.Points[0].position);
        }
    }

    private void LoadProgress()
    {
        for (int i = 0; i < LoadDotsCount; i++)
        {
            _selectedDot = PointContainer.Points[i+1];
            DrawLineToDot();
        }
    }
}
