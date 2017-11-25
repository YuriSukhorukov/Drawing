using System.Collections;
using System.Collections.Generic;
using UI.Widget;
using UnityEngine;

public class UIScreenView : MonoBehaviour {
    public virtual void Subscribe(){}
    public virtual void Show(){}
    public virtual void Hide(){}
    public virtual void SubscribeOnPressPageUIItem(PageListUIItem pageListUIItem){}
}
