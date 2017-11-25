using System.Collections;
using System.Collections.Generic;
using Audio;
using UI;
using UnityEngine;

public class PencilScaleManager : MonoBehaviour
{
	public UIWidget RootWidget;
	public UIWidget UiWidget;
	public UIGrid UiGrid;
	public List<ColorSelector> ColorSelectors;

	public GameObject Prefab;

	private UIGrid grid;
	private ColorSelectorManager _colorSelectorManager;
	
	public BoxCollider2D BoxCollider;
	public MeshRenderer PencilRenderer;

	private void Awake()
	{
		grid = GetComponent<UIGrid>();
		_colorSelectorManager = GetComponent<ColorSelectorManager>();
		_colorSelectorManager.UiWidget = GetComponent<UIWidget>();
		
		
	}

	private void Start()
	{
		int btnsCount = MaterialManager.Colors.Count;
		for (int i = 0; i < btnsCount; i++)
		{
			GameObject selectorObject = createButton(gameObject, i);
			ColorSelector selector = selectorObject.GetComponent<ColorSelector>();
			selector.ColorSelectorManager = _colorSelectorManager;
			ColorSelectors.Add(selector);
		}
		grid.Reposition();
		UiGrid = grid;

		foreach (ColorSelector colorSelector in ColorSelectors)
		{
			UpdateColorSelectorSize(colorSelector);
		}
		
		grid.enabled = true;
	}

	// TODO перенести логику подписки на события в GameManager - загрузка ресурсов
	private GameObject createButton(GameObject go, int id)
	{
		GameObject newButton = NGUITools.AddChild(go, Prefab);
		newButton.transform.parent = go.transform;
		newButton.transform.localPosition =
			new Vector3(newButton.transform.position.x, newButton.transform.position.y + 30f, 0);
		ColorSelector colorSelector = newButton.GetComponent<ColorSelector>();
		colorSelector.Index = id;

		UIEventListener.Get(newButton).onClick += OnClickEventHandler;
		return newButton;
	}

	private void OnClickEventHandler(GameObject sender)
	{
		SFXManager.Instance.OnPressPencil();
	}

	// TODO перенести логику отписки от событий в GameManager - освобождение ресурсов
	private void OnDestroy()
	{
		foreach (ColorSelector cs in ColorSelectors)
		{
			UIEventListener.Get(cs.gameObject).onClick -= OnClickEventHandler;
		}
	}
	
	public void UpdateColorSelectorSize(ColorSelector colorSelector)
	{
		grid.cellWidth = (RootWidget.width / 19)+6;
		colorSelector.UISprite.width = RootWidget.width / 19;
		colorSelector.BoxCollider.size = new Vector2(colorSelector.UISprite.width, colorSelector.UISprite.height);
		BoxCollider2D boxCollider2D = colorSelector.gameObject.GetComponent<BoxCollider2D>();
	}
}
