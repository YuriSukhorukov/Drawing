using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeepAspectRatio : MonoBehaviour
{
	private UIWidget _uiWidget;
	private void Awake()
	{
		_uiWidget = GetComponent<UIWidget>();
		UIManager.Instance.ResolutionChanged += ChangeKeepAspectRatio;
	}
	private void ChangeKeepAspectRatio()
	{
		if (Camera.main.aspect > 1)
		{
			_uiWidget.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
		}
		else
		{
			_uiWidget.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnWidth;
		}
	}

	private void OnEnable()
	{
		ChangeKeepAspectRatio();
	}
}
