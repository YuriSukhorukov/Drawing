using System;
using Audio;
using UI.Manager;
using UnityEngine;

[System.Serializable]
public class UISettingsBlock
{
	public string SpriteOn;
	public string SpriteOff;

	public UIButton UIButtonIcon;
	public UIButton UIButtonSlider;
	public UISlider UISlider;

	public ISoundManager Manager;

	private bool _isActive;

	public void Subscribe(ISoundManager concreteSoundManager)
	{
		Manager = concreteSoundManager;
		UIEventListener.Get(UIButtonIcon.gameObject).onClick += OnPressUIButtonIconHandler;
		UIEventListener.Get(UIButtonSlider.gameObject).onDrag += OnDragUISlider;
	}

	public void Unsubscribing()
	{
		UIEventListener.Get(UIButtonIcon.gameObject).onClick -= OnPressUIButtonIconHandler;
		UIEventListener.Get(UIButtonSlider.gameObject).onDrag -= OnDragUISlider;
	}
	
	private void ActivateButton()
	{
		_isActive = true;
		Manager.UnMute();
		UIButtonIcon.normalSprite = SpriteOn;
	}
	private void DeactivateButton()
	{
		_isActive = false;
		Manager.Mute();
		UIButtonIcon.normalSprite = SpriteOff;	
	}
	
	public void ActivateSlider()
	{
		ActivateButton();
	}
	public void DeactivateSlider()
	{
		DeactivateButton();
	}
	
	private void OnPressUIButtonIconHandler(GameObject go)
	{
		SFXManager.Instance.OnPressButton();
		if (_isActive)
		{
			UISlider.value = 0;
			DeactivateButton();
		}else if (!_isActive)
		{
			UISlider.value = 1;
			ActivateButton();
		}
	}
	private void OnDragUISlider(GameObject go, Vector2 distance)
	{
		Manager.SetVolume(UISlider.value);

		double TOLERANCE = 0.01;
		if (Math.Abs(UISlider.value) < TOLERANCE)
		{
			DeactivateSlider();
		}
		else
		{
			ActivateSlider();
		}
	}
}

public class UIScreenViewSettings : UIScreenView
{
	public UIButton UIButtonClose;

	[SerializeField] public UISettingsBlock UISettingsBlockMusic;
	[SerializeField] public UISettingsBlock UISettingsBlockSFX;

	private bool _isMusicOn = true;

	public override void Show()
	{
		UISettingsBlockMusic.Subscribe(MusicManager.Instance);
		UISettingsBlockSFX.Subscribe(SFXManager.Instance);
		UIScreensManager.Instance.EscapeKeyDownEvent += OnPressButtonEscape;
		UIUpdate();
	}
	
	public override void Hide()
	{
		UISettingsBlockMusic.Unsubscribing();
		UISettingsBlockSFX.Unsubscribing();
		UIScreensManager.Instance.EscapeKeyDownEvent -= OnPressButtonEscape;
	}
	
	public override void Subscribe()
	{
		UIEventListener.Get(UIButtonClose.gameObject).onClick += GoToPreviousViewSceen;
	}
	
	public void GoToPreviousViewSceen(GameObject go)
	{
		UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UIPreviousScreenView);
		UIScreensManager.Instance.HideScreen(this);
	}
	
	public void OnPressButtonEscape()
	{
		UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UIPreviousScreenView);
		UIScreensManager.Instance.HideScreen(this);
	}

	private void UIUpdate()
	{
		UISettingsBlockMusic.UISlider.value = MusicManager.Instance.Volume;
		UISettingsBlockSFX.UISlider.value = SFXManager.Instance.Volume;
		
		if(MusicManager.Instance.Volume < 0.01f)
			UISettingsBlockMusic.DeactivateSlider();
		if(SFXManager.Instance.Volume < 0.01f)
			UISettingsBlockSFX.DeactivateSlider();
	}
}
