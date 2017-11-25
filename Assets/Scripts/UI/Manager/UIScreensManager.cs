using System.Collections.Generic;
using UI.Widget;
using UnityEngine;

namespace UI.Manager
{
    public class UIScreensManager : MonoBehaviour
    {
        public UIScreenView UICurrentScreenView;
        public UIScreenView UIPreviousScreenView;

        private static UIScreensManager _instance;
	
        public UIScreenView UISettingsScreenView;
        public UIScreenView UICategoryScreenView;
        public UIScreenView UIPicturesScreenView;

        public delegate void EscapeKeyDownHandler();
        public event EscapeKeyDownHandler EscapeKeyDownEvent;

        public void Awake()
        {
            Instance = this;
            UICategoryScreenView.Subscribe();
            UISettingsScreenView.Subscribe();
            UIPicturesScreenView.Subscribe();
        }

        public static UIScreensManager Instance
        {
            get { return _instance; }
            set
            {
                if(_instance == null)
                    _instance = value;
            }
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (EscapeKeyDownEvent != null)
                {
                    EscapeKeyDownEvent();
                }
            }
        }

        public void ShowScreen(UIScreenView screenView)
        {
            UICurrentScreenView = screenView;
            screenView.Show();
            screenView.gameObject.SetActive(true);
        }

        public void HideScreen(UIScreenView screenView)
        {
            UIPreviousScreenView = screenView;
            screenView.Hide();
            screenView.gameObject.SetActive(false);
        }
    }
}
