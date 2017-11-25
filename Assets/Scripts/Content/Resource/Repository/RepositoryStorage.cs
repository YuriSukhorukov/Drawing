using System.Collections.Generic;
using UnityEngine;

namespace Content.Resource.Repository
{
    public class RepositoryStorage<T> : SortedDictionary<uint, T>, ISerializationCallbackReceiver where T:IHaveID
    {
        [SerializeField] 
        private List<T> values = new List<T>();
        
        public void OnBeforeSerialize()
        {
            values.Clear();
            foreach(T value in Values)
            {
                values.Add(value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            foreach (T value in values)
            {
                Add(value.ID, value);
            }
        }

    }
}