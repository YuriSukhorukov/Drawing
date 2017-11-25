using System.Collections.Generic;
using UI.Manager;
using UnityEngine;
using UnityEngine.iOS;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	
	private int _screenWidth;
	private int _screenHeight;
	private float cameraAspectRation;
	private DeviceOrientation _currentDeviceOrientation;
	
	public delegate void ResolutionChangedHandler();
	public event ResolutionChangedHandler ResolutionChanged;
	
	public UIWidget UIScreensContainerWidget;
	public List<UIScaler> UIScreenScalers;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		
		_currentDeviceOrientation = DeviceOrientation.Portrait;
		foreach (UIScaler uiScaler in UIScreenScalers)
		{
			ResolutionChanged += uiScaler.OnResolutionChanged;
		}
	}

	private void Start()
	{
		if (ResolutionChanged != null)
		{
			ResolutionChanged();
		}
	}
	
	private void Update()
	{
		DetectResolutionChange();
	}

	private void DetectResolutionChange()
	{
		if (_screenWidth != UIScreensContainerWidget.width || 
		    _screenHeight != UIScreensContainerWidget.height || 
		    cameraAspectRation != Camera.main.aspect)
		{
			cameraAspectRation = Camera.main.aspect;
			_screenWidth = UIScreensContainerWidget.width;
			_screenHeight = UIScreensContainerWidget.height;
			if (ResolutionChanged != null)
			{
				ResolutionChanged();
			}
		}
	}
}
