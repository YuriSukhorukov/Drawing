using System.Collections;
using System.Collections.Generic;
using Audio;
using Game;
using UnityEngine;

public class UIScreenViewGame : MonoBehaviour
{
	public delegate void EscapeKeyDownHandler();
	public event EscapeKeyDownHandler EscapeKeyDownEvent;
	
	public UIButton UIButtonBack;
	public UIButton UIButtonErace;
	public UIButton UIButtonOkay;

	private void Awake()
	{
		UIEventListener.Get(UIButtonBack.gameObject).onClick += OnPressButtonBack;
		UIEventListener.Get(UIButtonErace.gameObject).onClick += OnPressButtonErace;
		UIEventListener.Get(UIButtonOkay.gameObject).onClick += OnPressButtonOkay;
		EscapeKeyDownEvent += OnPressButtonEscape;
	}
	
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (EscapeKeyDownEvent != null)
			{
				EscapeKeyDownEvent();
			}
		}
	}
	
	public void OnPressButtonBack(GameObject go)
	{
		GameManager.Instance.GoToMenuScene();
		SFXManager.Instance.OnPressButton();
	}
	public void OnPressButtonErace(GameObject go)
	{
		ColorfulMeshesManager.Instance.ResetColoredRegions();
		SFXManager.Instance.OnPressButton();
	}
	public void OnPressButtonOkay(GameObject go)
	{
		GameManager.Instance.GoToMenuScene();
		SFXManager.Instance.OnPressButton();
	}

	public void OnPressButtonEscape()
	{
		GameManager.Instance.GoToMenuScene();
	}
}
