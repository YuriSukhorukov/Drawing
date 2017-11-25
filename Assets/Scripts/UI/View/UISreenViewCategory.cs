using UI.Manager;
using UnityEngine;

public class UISreenViewCategory : UIScreenView
{
	public UIButton UIButtonSettings;

	public override void Show(){}
	public override void Hide(){}

	public override void Subscribe()
	{
		UIEventListener.Get(UIButtonSettings.gameObject).onClick += GoToSettingsScreen;
	}

	public void GoToSettingsScreen(GameObject go)
	{
		UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UISettingsScreenView);
		UIScreensManager.Instance.HideScreen(this);
	}
}