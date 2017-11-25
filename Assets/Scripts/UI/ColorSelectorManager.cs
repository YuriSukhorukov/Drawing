using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ColorSelectorManager : MonoBehaviour
    {
        public UIWidget RootWidget;
    
        private ColorSelector _activeColorSelector;
        public ColorSelector ActiveColorSelector
        {
            get { return _activeColorSelector; }
            set
            {
                if (_activeColorSelector)
                {
                    _activeColorSelector.IsActive = false;
                    HideColorSelector(_activeColorSelector);
                }

                _activeColorSelector = value;
                _activeColorSelector.IsActive = true;
                ShowColorSelector(_activeColorSelector);
            }
        }

        public UIWidget UiWidget;
        public UIGrid UiGrid;
        public List<ColorSelector> ColorSelectors;
    
        private int _prevCameraPixelWidth;
        private float _aspect;
        private float _shortSide = 0;

        public void ShowColorSelector(ColorSelector colorSelector)
        {
            colorSelector.Show();
        }

        public void HideColorSelector(ColorSelector colorSelector)
        {
            colorSelector.Hide();
        }

        private void Start()
        {
            OnScreenAspectChanged();
            Invoke("ShowFirstCS",0.5f);
        }

        private void ShowFirstCS()
        {
            ActiveColorSelector = ColorSelectors[ColorSelectors.Count - 1];
            MaterialManager.Active = MaterialManager.Materials.Length - 1;
        }
        
        private void Update()
        {
            OnScreenAspectChanged();
        }

        private void OnScreenAspectChanged()
        {
            if (_prevCameraPixelWidth != Camera.main.pixelWidth || _aspect != Camera.main.aspect)
            {       
                _prevCameraPixelWidth = Camera.main.pixelWidth;
                _aspect = Camera.main.aspect;

                _shortSide = RootWidget.width < RootWidget.height ? RootWidget.width : RootWidget.height;
                UiGrid.cellWidth = _shortSide / ColorSelectors.Count;
                UiGrid.maxPerLine = ColorSelectors.Count;
                foreach (ColorSelector us in ColorSelectors)
                {
                    us.UISprite.width = (int)UiGrid.cellWidth;
                }
                UiGrid.enabled = true;
                if (ActiveColorSelector)
                {
                    ActiveColorSelector.TweenPos.ResetToBeginning();
                }
            
                StartCoroutine(DelayUpdateColorSelectors());
                
                print("change resolution");
            }
        }

        private void UpdateTweens()
        {
            foreach (ColorSelector cs in ColorSelectors)
            {
                cs.InitTweens();
            }
            if (ActiveColorSelector)
            {
                ActiveColorSelector.Show();
            }
        }

        private void UpdateColliders()
        {
            foreach (ColorSelector cs in ColorSelectors)
            {
                cs.UpdateColliderSize();
            }
        }

        private void UpdatePosition()
        {
            foreach (ColorSelector cs in ColorSelectors)
            {
                cs.ResetPosition();
            }
        }

        private IEnumerator DelayUpdateColorSelectors()
        {
            yield return new WaitForSeconds(0.1f);
            UpdateTweens();
            UpdateColliders();
            UpdatePosition();
        }
    }
}
