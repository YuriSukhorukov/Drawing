using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content.Resource.Repository
{
    public abstract class AbstractRepository<T> : IRepository<T> where T:IHaveID
	{

	    private RepositoryStorage<T> storage;
		public RepositoryStorage<T> Storage
		{
			get
			{
				// TODO Стейт-машина для репозитория (если надобна)
			    if (storage == null)
				{
					Init();
				}
				return storage;
			}
		}

		public virtual void Init()
		{
			storage = new RepositoryStorage<T>();
		}

	    // TODO Логика обновления и репликации репозитория
	    public virtual void Update()
	    {
	        throw new NotImplementedException();
	    }

		public void Add(T item)
		{
			Storage.Add(item.ID, item);
		}

		public T Get(uint id)
		{
			return Storage.ContainsKey(id) ? Storage[id] : default(T);
		}

	    public List<T> Get(List<uint> ids)
	    {
	        return ids.Where(Storage.ContainsKey).Select(Get).ToList();
	    }

	    public T Find(Predicate<T> match)
		{
			return Storage.Values.ToList().Find(match);
		}

		public List<T> FindAll(Predicate<T> match)
		{
			return Storage.Values.ToList().FindAll(match);
		}

        #region Serialiazation Tests
	    public string ToJSON()
	    {
	        return JsonUtility.ToJson(Storage);
	    }

	    // TODO Логика неполного обновления
	    public void FromJSON(string json)
	    {
	        try
	        {
	            RepositoryStorage<T> newStorage = JsonUtility.FromJson<RepositoryStorage<T>>(json);
	            storage = newStorage;
	            
//	            // Более годный вариант?
//	            Storage.Clear();
//	            foreach (T item in newStorage.Values)
//	            {
//	                Add(item);
//	            }
	        }
	        catch (Exception ex)
	        {
	            Debug.LogException(ex);
	        }
	    }
	    #endregion
	}
}
