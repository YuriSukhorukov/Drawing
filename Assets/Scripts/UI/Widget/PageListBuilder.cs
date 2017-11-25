using System.Collections.Generic;
using Content;
using Content.Resource;
using UI.Manager;
using UnityEngine;

namespace UI.Widget
{
    public class PageListBuilder : MonoBehaviour
    {    
        public GameObject PageUIItemPrototype;
        public GameObject Container;
        public List<PageListUIItem> PageListUIItems;
        public Book book;

        private UIScaler _uiScaler;
        
        public void Buils()
        {
            _uiScaler = GetComponent<UIScaler>();
            
            book = Library.Books.Get(BookListUIItem.selectedID);
            List<Page> pages = book.Pages;
            
            foreach (Page p in pages)
            {
                GameObject newPageUIItem = NGUITools.AddChild(Container, PageUIItemPrototype);
                newPageUIItem.transform.parent = Container.transform;
                PageListUIItem item = newPageUIItem.GetComponent<PageListUIItem>();
                PageListUIItems.Add(item);
                item.Init(p);
                
                _uiScaler.ElementsToScale.Add(newPageUIItem.GetComponent<UISprite>());
                
                UIScreensManager.Instance.UIPicturesScreenView.SubscribeOnPressPageUIItem(item);
            }
            Invoke("Scale",0.01f);
            Invoke("UpdatePosition", 0.02f);
            Invoke("Scale", 0.03f);
            //Invoke("ActivateWrapMode", 0.01f);
            // Update NGUI
        }

        public void Clear()
        {
            _uiScaler.ElementsToScale.Clear();
            PageListUIItems.Clear();
            foreach (var page in book.Pages)
            {
                ResourceLoader.UnloadBundle(page.PreviewResource.BundleName);
            }
            foreach (Transform tr in Container.transform)
            {
                Destroy(tr.gameObject);
            }
        }

        private void UpdatePosition()
        {
            Container.GetComponent<UIGrid>().Reposition();
        }

        private void ActivateWrapMode()
        {
            Container.GetComponent<UIWrapContent>().enabled = true;
        }
        
        private void Scale()
        {
            _uiScaler.OnResolutionChanged();  
        }
    }
}