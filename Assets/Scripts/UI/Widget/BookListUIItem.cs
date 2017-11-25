using System;
using Content;
using UnityEngine;

namespace UI.Widget
{
    public class BookListUIItem : MonoBehaviour
    {
        public UILabel label;
        public UITexture sprite;
        public UIButton button;
        private uint id;
        public static uint selectedID;
        
        public void Init(Book book)
        {
            label = GetComponentInChildren<UILabel>();
            sprite = GetComponentInChildren<UITexture>();
            button = GetComponent<UIButton>();
            
            label.text = book.Name;
            Texture2D tex = book.Preview;
            try
            {
                sprite.mainTexture = book.Preview;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            
            id = book.ID;
            UIEventListener.Get(button.gameObject).onClick += ShowBook;
        }

        private void ShowBook(GameObject go)
        {
            selectedID = id;
            Debug.LogFormat("Requested Book #{0}", id);
        }
    }
}