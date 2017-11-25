using UnityEngine;

public class UIAtlasCustomCreator : MonoBehaviour
{
    [HideInInspector]
    public UIAtlas UIAtlas;

    int _id = 0;

    public void CreateSpriteInAtlas(Rect spriteRect)
    {
        _id += 1;
        UISpriteData spriteData = new UISpriteData();
        spriteData.name = "pencil" + _id;
        spriteData.SetRect((int)spriteRect.x, (int)spriteRect.y, (int)spriteRect.width, (int)spriteRect.height);
        UIAtlas.spriteList.Add(spriteData);
    }
  
    public void CreateAtlas(string atlasName) {
        GameObject atlasGameObject = new GameObject("UIPencilsAtlas");
        UIAtlas = atlasGameObject.AddComponent<UIAtlas>();
        UIAtlas.spriteList.Clear();
        
        Material material = new Material(Shader.Find("Unlit/Transparent Colored"));
        UIAtlas.spriteMaterial = material;
        Texture texture = Resources.Load<Texture>("AtlasesBaked/" + atlasName);
        UIAtlas.spriteMaterial.SetTexture("_MainTex", texture);
    }
}
