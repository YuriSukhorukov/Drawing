using System.Collections;
using System.Collections.Generic;
using Audio;
using UI.Manager;
using UI.Widget;
using UnityEngine;

public class UIScreenViewCategory : UIScreenView {
    public UIButton UIButtonSettings;
    public UISlider UISlider;
    
    public List<BookListUIItem> BookListUIItems;
    
    // TODO: хегшировать используемые компоненты
    public override void Subscribe()
    {
        BookListUIItems = GetComponent<BookListBuilder>().Buils();
        UIEventListener.Get(UIButtonSettings.gameObject).onClick += GoToSettingsScreen;

        foreach (BookListUIItem bookListUIItem in BookListUIItems)
        {
            UIEventListener.Get(bookListUIItem.button.gameObject).onClick += GoToPicturesScreen;
            
            UIEventListener.Get(bookListUIItem.button.gameObject).onPress += OnPress;
            UIEventListener.Get(bookListUIItem.button.gameObject).onDragStart += OnDragStart;
            UIEventListener.Get(bookListUIItem.button.gameObject).onDragEnd += OnDragStart;
        }
        
        Invoke("ResetSliderCategoryList", 0.02f);
    }

    public void GoToSettingsScreen(GameObject go)
    {
        UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UISettingsScreenView);
        UIScreensManager.Instance.HideScreen(this);
        SFXManager.Instance.OnPressButton();
    }

    public void GoToPicturesScreen(GameObject go)
    {
        UIScreensManager.Instance.ShowScreen(UIScreensManager.Instance.UIPicturesScreenView);
        UIScreensManager.Instance.UIPicturesScreenView.GetComponent<PageListBuilder>().Buils();
        UIScreensManager.Instance.HideScreen(this);
        SFXManager.Instance.OnPressCategoryItem();
        
        go.GetComponent<UISprite>().color = new Color(1, 1, 1, 1);
        go.GetComponentInChildren<UITexture>().color = new Color(1, 1, 1, 1);
    }

    private void OnPress(GameObject go, bool isRelease)
    {
        if (isRelease)
        {
            go.GetComponent<UISprite>().color = new Color(1, 1, 1, 0.5f);
            go.GetComponentInChildren<UITexture>().color = new Color(1, 1, 1, 0.1f);
        }
    }

    private void OnDragStart(GameObject go)
    {
        go.GetComponent<UISprite>().color = new Color(1, 1, 1, 1);
        go.GetComponentInChildren<UITexture>().color = new Color(1, 1, 1, 1);
    }

    private void ResetSliderCategoryList()
    {
        UISlider.value = 0f;
    }
}
