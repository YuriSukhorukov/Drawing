using System.Collections.Generic;
using Content;
using UnityEngine;

namespace UI.Widget
{
    public class BookListBuilder : MonoBehaviour
    {
        public GameObject BookUIItemPrototype;
        public GameObject Container;
        public List<BookListUIItem> BookListUIItems;
        
        private UIScaler _uiScaler;
        
        public List<BookListUIItem> Buils()
        {
            _uiScaler = GetComponent<UIScaler>();
            
            foreach (Book b in Library.Books.Storage.Values)
            {
                GameObject newBookUIItem = NGUITools.AddChild(Container, BookUIItemPrototype);
                newBookUIItem.transform.parent = Container.transform;
                BookListUIItem item = newBookUIItem.GetComponent<BookListUIItem>();
                BookListUIItems.Add(item);
                item.Init(b);
                
                _uiScaler.ElementsToScale.Add(newBookUIItem.GetComponent<UISprite>());
            }
            
            Invoke("Scale",0.01f);
            Invoke("UpdatePosition", 0.02f);
            // Update NGUI
            return BookListUIItems;
        }
        
        private void UpdatePosition()
        {
            Container.GetComponent<UIGrid>().Reposition();
        }

        private void Scale()
        {
            //_uiScaler.OnResolutionChanged();  
        }
    }
}