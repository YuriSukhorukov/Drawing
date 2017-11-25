using System;

public class UIItemObjectPool : ObjectPool<ItemData>
{
	private CreatorStrategy<ItemData> _creatorStrategy;
	protected override CreatorStrategy<ItemData> CreatorStrategy { get { return _creatorStrategy; }}

	public UIItemObjectPool(int count) : base(count)
	{
		_creatorStrategy = new UIItemDataBasedCreatorStrategy();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public override void Free(ItemData uiItemData)
	{
		base.Free(uiItemData);
	}
}

public class UIItemDataBasedCreatorStrategy: CreatorStrategy<ItemData>
{
	private int _index;

	public UIItemDataBasedCreatorStrategy()
	{
		_index = 0;
	}
	public override ItemData Create()
	{
		_index += 1;
		ItemData uiItem = new ItemData();
		uiItem.name = GetName();
		uiItem.Index = _index;
		return uiItem;
	}
	public override void Reset()
	{
		throw new NotImplementedException();
	}
	
	private string GetName()
	{
		return _index.ToString();
	}
}
