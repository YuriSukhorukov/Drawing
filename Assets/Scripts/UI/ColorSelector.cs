using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public class ColorSelector : MonoBehaviour
	{
		public TweenPosition TweenPos;
		public bool IsActive;
		public ColorSelectorManager ColorSelectorManager;
		public UI2DSprite UISprite;
		public BoxCollider2D BoxCollider;

		public int index = 0;
		public int Index {
			set
			{ 
				index = Mathf.Clamp (value, 0, MaterialManager.Materials.Length - 1);
			}
			get { return index; }
		}

		private UIButtonColor btnColor;

		private void Awake ()
		{
			btnColor = GetComponent<UIButtonColor> ();
			UISprite = GetComponent<UI2DSprite>();
			BoxCollider = GetComponent<BoxCollider2D>();
		}

		private void Start ()
		{
			TweenPos = GetComponent<TweenPosition>();
			
			btnColor.defaultColor = Color.white;
			btnColor.hover = Color.white;
			btnColor.pressed = Color.white;
			btnColor.disabledColor = Color.white;
			gameObject.name = "ColorSelector #" + index;
			transform.position = new Vector3(transform.position.x, transform.position.y, -1);
			UISprite.aspectRatio = 0.19f;
			
			InitTweens();
		}

		private void OnClick ()
		{
			MaterialManager.Active = index;
			if(ColorSelectorManager.ActiveColorSelector != this)
				ColorSelectorManager.ActiveColorSelector = this;
		}

		public void InitTweens()
		{
			TweenPos.from = new Vector3(transform.localPosition.x, UISprite.height/9f, transform.localPosition.z);
			TweenPos.to = new Vector3(transform.localPosition.x, UISprite.height/2.15f, transform.localPosition.z);
			transform.localPosition = TweenPos.from;
		}

		public void Show()
		{		
			TweenPos.PlayForward();
		}

		public void Hide()
		{
			TweenPos.PlayReverse();
		}

		public void UpdateColliderSize()
		{
			BoxCollider.size = new Vector2(UISprite.width, UISprite.height);
			UISprite.width += 4;
		}

		public void ResetPosition()
		{
			transform.localPosition = new Vector3(transform.localPosition.x, UISprite.height/9f, transform.localPosition.z);
		}
	}
}
