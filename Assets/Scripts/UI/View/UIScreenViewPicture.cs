using System.Collections.Generic;
using Audio;
using Game;
using UI.Manager;
using UI.Widget;
using UnityEngine;

public class UIScreenViewPicture : UIScreenView
{
	public UIButton UIButtonBack;
	public UIButton UIButtonSettings;
	
	public UIButton UIScrollUpButton;
	public UIButton UIScrollDownButton;
	public UIScrollView UIScrollViewContainerCategory;
	public UISprite UISpriteLoader;

	public float SCROLL_SPEED;
	public List<PageListUIItem> PageListUIItems;

	private SpringPanel springPanel;
	private int selectedPageItemIndex;
	private int maxSelectedIndex;
	
	private TweenFill uiSpriteLoaderTweenFill;

	//TODO почистить
	public override void Subscribe()
	{
		UIEventListener.Get(UIButtonBack.gameObject).onClick += GoToCategoryScreen;
		UIEventListener.Get(UIButtonSettings.gameObject).onClick += GoToSettingsScreen;
		UIEventListener.Get(UIScrollUpButton.gameObject).onClick += OnPressButtonScrollUpHandler;
		UIEventListener.Get(UIScrollDownButton.gameObject).onClick += OnPressButtonScrollDownHandler;

		uiSpriteLoaderTweenFill = UISpriteLoader.GetComponent<TweenFill>();
		uiSpriteLoaderTweenFill.onFinished.Add(new EventDelegate(LoadColoringScene));

		selectedPageItemIndex = 0;
	}

	public void GoToCategoryScreen(GameObject go)
	{
		springPanel = UIScrollViewContainerCategory.GetComponent<SpringPanel>();
		UIScrollViewContainerCategory.transform.position = Vector3.zero;
		springPanel.target = Vector3.zero;
		selectedPageItemIndex = 0;
		
		GetComponent<PageListBuilder>().Clear();
		UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UICategoryScreenView);
		UIScreensManager.Instance.HideScreen(this);
		SFXManager.Instance.OnPressButton();
	}

	public void GoToSettingsScreen(GameObject go)
	{
		UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UISettingsScreenView);
		UIScreensManager.Instance.HideScreen(this);
		SFXManager.Instance.OnPressButton();
	}

	public void OnPressBackButton()
	{
		UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UICategoryScreenView);
		UIScreensManager.Instance.HideScreen(this);
	}

	public void OnPressButtonScrollUpHandler(GameObject go)
	{
		StopScroll(-1);
		SFXManager.Instance.OnPicturesScroll();
	}
	
	public void OnPressButtonScrollDownHandler(GameObject go)
	{
		StopScroll(1);
		SFXManager.Instance.OnPicturesScroll();
	}

	public void OnPressPicture(GameObject go)
	{
		uiSpriteLoaderTweenFill.Play();
		UISpriteLoader.gameObject.SetActive(true);
	}

	private void LoadColoringScene()
	{
		GameManager.Instance.GoToGameScene();
	}

	private void OnEnable()
	{
		foreach (PageListUIItem pageListUIItem in PageListUIItems)
		{
			UIEventListener.Get(pageListUIItem.button.gameObject).onClick += OnPressPicture;
		}
	}

	private int _pageHeight;
	private SpringPanel _springPanel;
	private PageListBuilder _pageListBuilder;
	private void StopScroll(int sideCoeff)
	{
		if (_pageHeight == 0)
			_pageHeight = GetComponent<PageListBuilder>().PageUIItemPrototype.GetComponent<UISprite>().height;
		if(_springPanel == null)
			_springPanel = UIScrollViewContainerCategory.GetComponent<SpringPanel>();
		if (_pageListBuilder == null)
			_pageListBuilder = GetComponent<PageListBuilder>();
		
		selectedPageItemIndex = (int)(_springPanel.target.y / _pageHeight);
		maxSelectedIndex = PageListUIItems.Count;

		bool canMove = false;
		
		maxSelectedIndex = _pageListBuilder.PageListUIItems.Count;
		
		if (sideCoeff > 0)
		{
			if (selectedPageItemIndex + 1 < maxSelectedIndex)
			{
				selectedPageItemIndex += 1;
				canMove = true;
			}
		}else if (sideCoeff < 0)
		{
			if (selectedPageItemIndex - 1 >= 0)
			{
				selectedPageItemIndex -= 1;
				canMove = true;
			}
		}

		if (canMove)
		{
			_springPanel.target = new Vector3(0, _springPanel.target.y + (_pageHeight * sideCoeff), 0);
			_springPanel.enabled = true;
		}
	}

	public override void SubscribeOnPressPageUIItem(PageListUIItem pageListUIItem)
	{
		UIEventListener.Get(pageListUIItem.gameObject).onClick += OnPressPicture;
	}

	public override void Show()
	{
		UIScreensManager.Instance.EscapeKeyDownEvent += OnPressEscapeButton;
	}

	public override void Hide()
	{
		UIScreensManager.Instance.EscapeKeyDownEvent -= OnPressEscapeButton;
	}

	public void OnPressEscapeButton()
	{
		GoToCategoryScreen(gameObject);
	}
}
