using UnityEngine;

public class UISliderItems : MonoBehaviour
{
//    public GameObject PanelScrollView;
//    public UIButton SliderButton;
//
//    private AbstractUIItemsManager _abstractUIItemsManager;
//    private UIScrollView UiScrollView;
//    private UISlider UiSlider;
//
//    private SpringPanel sp;
//
//    private bool isSliderActive;
//    private void Awake()
//    {
//        _abstractUIItemsManager = PanelScrollView.GetComponent<AbstractUIItemsManager>();
//        UiScrollView = PanelScrollView.GetComponent<UIScrollView>();
//        UiSlider = PanelScrollView.GetComponent<UISlider>();
//        
//        UiSlider = GetComponent<UISlider>();
//        sp = UiScrollView.gameObject.AddComponent<SpringPanel>();
//        sp.target = new Vector3(0, 0, 0);
//        SliderButton.onClick.Add(new EventDelegate(ActivateSlider));
//    }
//
//    private void Update()
//    {     
//        if (Input.GetMouseButton(0))
//        {
//            float targetY = _abstractUIItemsManager.HeightItemsPanel * UiSlider.value;
//            if (SliderButton.state == UIButtonColor.State.Pressed)
//            {
//                ActivateSlider();
//                sp.target = new Vector3(0, targetY, 0);
//                sp.enabled = true;
//            }
//            else
//            {
//                DeactivateSlider();
//            }
//        }
//
//        if (!isSliderActive)
//        {
//            UiSlider.value = _abstractUIItemsManager.gameObject.transform.localPosition.y / _abstractUIItemsManager.HeightItemsPanel;
//        }
//    }
//
//    private void ActivateSlider()
//    {
//        isSliderActive = true;
//    }
//
//    private void DeactivateSlider()
//    {
//        isSliderActive = false;
//    }
}
