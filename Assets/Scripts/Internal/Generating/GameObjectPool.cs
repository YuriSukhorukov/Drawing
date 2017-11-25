using UnityEngine;

public class GameObjectPool : ObjectPool<GameObject>
{
	private readonly CreatorStrategy<GameObject> _creatorStrategy;
	protected override CreatorStrategy<GameObject> CreatorStrategy{get { return _creatorStrategy; }}

	public GameObjectPool(GameObject prefab, int count) : base(count)
	{
		_creatorStrategy = new PrefabBasedCreatorStrategy(prefab);
	}

	public override void Dispose()
	{
		foreach (var go in Objects)
		{
			Object.Destroy(go);
		}
		base.Dispose();
	}

	public override void Free(GameObject go)
	{
		base.Free(go);
		Object.Destroy(go);
	}
}

public class PrefabBasedCreatorStrategy: CreatorStrategy<GameObject>
{
	private readonly GameObject _prefab;
	private int _index;
	
	public PrefabBasedCreatorStrategy(GameObject prefab)
	{
		_index = 0;
		_prefab = prefab;
	}
	
	public override GameObject Create()
	{
		GameObject go = Object.Instantiate(_prefab);
		go.name = GetName();
		return go;
	}
	public override void Reset()
	{
		_index = 0;
	}

	private string GetName()
	{
		_index += 1;
		return _index.ToString();
	}
}
