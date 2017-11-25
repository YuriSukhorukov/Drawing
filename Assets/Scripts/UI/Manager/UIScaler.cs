using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    public UIWidget RootWidget;
    public float rate;
    public float rateChild;
    
    public UIWidget RootScaleElementsScale;

    public List<UIWidget> ElementsToScale;

    private int _prevCameraPixelWidth;
    private float _aspect;
    private float _shortSide = 0;

    public float landScapeRate;
    public float portraitScaleRate;

    public bool flag;

    public void OnResolutionChanged()
    {
        if (ElementsToScale.Count > 0)
        {
            if (RootWidget.width > RootWidget.height)
            {
                RootScaleElementsScale.width = (int) (RootWidget.height / landScapeRate);
            }
            else
            {
                RootScaleElementsScale.width = (int) (RootWidget.width * rate);
            }
            
            foreach (UIWidget uiWidget in ElementsToScale)
            {
                uiWidget.width = (int) (RootScaleElementsScale.width * rateChild);
            }
        }
        
        print(Camera.main.aspect);
        if (flag)
        {
            if (Camera.main.aspect > 1)
            {
                RootScaleElementsScale.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
            }
            else
            {
                RootScaleElementsScale.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnWidth;
            }
        }
    }
}
