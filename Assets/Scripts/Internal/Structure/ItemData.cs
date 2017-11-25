public class ItemData
{
	private string _name;
	private int _index;
	
	public string name
	{
		get { return _name; }
		set { _name = value; }
	}

	public int Index
	{
		get { return _index; }
		set { _index = value; }
	}

	public string Id;

	public ItemData()
	{
		System.Random random = new System.Random();
		Id = random.GetHashCode().ToString();
	}
}