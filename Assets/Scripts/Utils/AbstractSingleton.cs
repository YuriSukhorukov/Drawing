namespace Utils
{
	public abstract class AbstractSingleton<T> where T : AbstractSingleton<T>, new()
	{
	    // ReSharper disable once InconsistentNaming
        private static readonly T instance = new T();
        public static T Instance
		{
			get
			{
				return instance;
			}
		}
	}
}
