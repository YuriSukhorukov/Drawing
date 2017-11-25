using System.Collections.Generic;
using System.Linq;
using Audio;
using UnityEngine;

namespace UI
{
	public class ColorSelectorBuilder : MonoBehaviour {

		public GameObject Prefab;
		public List<Sprite> SpriteList;

		private UIGrid grid;
		private ColorSelectorManager _colorSelectorManager;
		public List<ColorSelector> ColorSelectors;
		public UIAtlasCustomCreator UIPencilAtlasCustomCreator;

		private void Awake ()
		{
			SpriteList = new List<Sprite>();
			SpriteList.AddRange(Resources.LoadAll<Sprite>("AtlasesBaked/" + PaletteManager.selectedPaletteName + "/").ToList());
			
			grid = GetComponent<UIGrid>();
			_colorSelectorManager = GetComponent<ColorSelectorManager>();
			_colorSelectorManager.UiWidget = GetComponent<UIWidget>();
			UIPencilAtlasCustomCreator = GetComponent<UIAtlasCustomCreator>();
		}

		private void Update()
		{
			/*
			if (Input.GetMouseButtonDown(0))
			{
				//PaletteManager.SelectPalette("palette" + 1);
				//Sprite loadedSprite = Resources.Load<Sprite>("AtlasesBaked/" + PaletteManager.selectedPaletteName);

				Sprite loadedSprite = SpriteList[0];
				PaletteManager.SelectPalette("palette" + 2);
				
				UIPencilAtlasCustomCreator.UIAtlas.spriteMaterial.SetTexture("_MainTex", loadedSprite.texture);
				UIPencilAtlasCustomCreator.UIAtlas.MarkAsChanged();
				UIPencilAtlasCustomCreator.UIAtlas.MarkSpriteListAsChanged();
			}
			if (Input.GetMouseButtonDown(1))
			{
//				PaletteManager.SelectPalette("palette" + 2);
//				Sprite loadedSprite = Resources.Load<Sprite>("AtlasesBaked/" + PaletteManager.selectedPaletteName);
				
				Sprite loadedSprite = SpriteList[1];
				PaletteManager.SelectPalette("palette" + 2);
				
				UIPencilAtlasCustomCreator.UIAtlas.spriteMaterial.SetTexture("_MainTex", loadedSprite.texture);
				UIPencilAtlasCustomCreator.UIAtlas.MarkAsChanged();
				UIPencilAtlasCustomCreator.UIAtlas.MarkSpriteListAsChanged();
			}
			*/
		}

		private void Start ()
		{
			UIPencilAtlasCustomCreator.CreateAtlas(PaletteManager.selectedPaletteName);
			
			int btnsCount = MaterialManager.Materials.Length;
			
			for (int i = 0; i < btnsCount; i++)
			{
				GameObject selectorObject = createButton (gameObject, i);
				ColorSelector selector = selectorObject.GetComponent<ColorSelector>();
				selector.ColorSelectorManager = _colorSelectorManager;
				ColorSelectors.Add(selector);

				selector.UISprite.sprite2D = SpriteList[i];

				selector.UpdateColliderSize();
			}
			grid.Reposition();
			grid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
			_colorSelectorManager.UiGrid = grid;
			_colorSelectorManager.ColorSelectors = ColorSelectors;	
		}
		
		// TODO: перенести логику подписки на события в GameManager - загрузка ресурсов
		private GameObject createButton (GameObject go, int id)
		{
			GameObject newButton = NGUITools.AddChild(go, Prefab);
			newButton.transform.parent = go.transform;
			newButton.transform.localPosition = new Vector3(newButton.transform.position.x,newButton.transform.position.y,0);
			ColorSelector colorSelector = newButton.GetComponent<ColorSelector>();
			colorSelector.ResetPosition();
			colorSelector.Index = id;

			colorSelector.UpdateColliderSize();

			UIEventListener.Get(newButton).onClick += OnClickEventHandler;
			return newButton;
		}

		//TODO: завернуть выбор материала в отдельеный метод SelectMaterial
		private void OnClickEventHandler(GameObject sender){
			SFXManager.Instance.OnPressPencil();
		}
		
		// TODO: перенести логику отписки от событий в GameManager - освобождение ресурсов
		private void OnDestroy()
		{
			foreach (ColorSelector cs in ColorSelectors)
			{
				UIEventListener.Get(cs.gameObject).onClick -= OnClickEventHandler;
			}
		}
	}
}
