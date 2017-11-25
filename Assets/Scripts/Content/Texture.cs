using System;
using Content.Resource;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Texture : IHaveID
    {
        [SerializeField]
        private uint id;
        public uint ID
        {
            get { return id; }
            private set { id = value; }
        }

        [SerializeField] private ResourceLocation resource;

        public ResourceLocation Location
        {
            get { return resource; }
        }

        [SerializeField] private LoadableResource<Texture2D> texture2D;

        /// <summary> 
        /// Возвращает экземпляр Texture2D. В экземпляре Texture результат не кэшируется,
        /// т.е. ресурс каждый раз грузится заново!!! КЭШИРОВАТЬ САМОСТОЯТЕЛЬНО!  
        /// </summary> 
        public Texture2D Texture2D
        {
            get
            {
                if (texture2D == null)
                {
                    texture2D = new LoadableResource<Texture2D>(resource);
                }
                return texture2D.Load();
            }
        }

        public Texture(uint id, string resourceName, string assetBundleName)
        {
            ID = id;
            resource = new ResourceLocation(resourceName, assetBundleName);
        }
    }
}