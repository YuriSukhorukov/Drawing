using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackgroundSize : MonoBehaviour {

	private UIWidget cWidget;

	public UIWidget Parent = null;
	public BackgroundSize Size = BackgroundSize.Free;

	void Awake () {
		cWidget = gameObject.GetComponent<UIWidget> ();
	}

	void Start () {
		UpdateSize ();
	}

	void UpdateSize () {
		float aspect = (float)cWidget.width / cWidget.height;
		float pAspect = (float)Parent.width / Parent.height;

		switch (Size) {
		case BackgroundSize.Free:
			break;
		case BackgroundSize.Contains:
			cWidget.keepAspectRatio = (aspect >= pAspect) ? UIWidget.AspectRatioSource.BasedOnHeight : UIWidget.AspectRatioSource.BasedOnWidth;
			break;
		case BackgroundSize.Cover:
			cWidget.keepAspectRatio = (aspect <= pAspect) ? UIWidget.AspectRatioSource.BasedOnHeight : UIWidget.AspectRatioSource.BasedOnWidth;
			break;
		}
	}

	public enum BackgroundSize {
		Free,
		Contains,
		Cover
	}
}
