using System;
using Content;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Widget
{
    public class PageListUIItem : MonoBehaviour
    {
        public UITexture sprite;
        public UIButton button;
        private uint id;
        
        public void Init(Page page)
        {
            sprite = GetComponentInChildren<UITexture>();
            button = GetComponent<UIButton>();
            
            Texture2D tex = page.Preview;
            
            try
            {
                sprite.mainTexture = page.Preview;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            
            id = page.ID;
            UIEventListener.Get(button.gameObject).onClick += ShowPage;
        }

        private void ShowPage(GameObject go)
        {
            Debug.LogFormat("Requested Page #{0}", id);
            GameManager.Instance.selectedPageId = (int)id;
        }
    }
}