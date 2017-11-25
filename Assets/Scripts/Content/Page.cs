using System;
using System.Collections.Generic;
using ColoringBook.Serialization;
using Content.Resource;
using UnityEngine;


namespace Content {
    
    [Serializable]
    public class Page : IHaveID
    {

        [SerializeField]
        private uint id;
        public uint ID
        {
            get { return id; }
            private set { id = value; }
        }

        [SerializeField]
        private ResourceLocation resource;
        public ResourceLocation Location
        {
            get { return resource; }
        }
        
        [SerializeField]
        private LoadableResource<ImageData> imageData;
        /// <summary> 
        /// Возвращает десериализованную инфу по картинке. В экземпляре Page результат не кэшируется,
        /// т.е. ресурс каждый раз грузится заново!!! КЭШИРОВАТЬ САМОСТОЯТЕЛЬНО!  
        /// </summary> 
        public ImageData ImageData
        {
            get
            {
                if (imageData == null)
                {
                    imageData = new LoadableResource<ImageData>(resource);
                }
                return imageData.Load();
            }
        }

//		[SerializeField]
//        private List<string> tags = new List<string>();
//        public List<string> Tags
//        {
//            get { return tags; }
//        }
        
        // TODO: Позволить работать непосредственно с ресурсом текстуры
        [SerializeField]
        private uint textureID;
        public ResourceLocation PreviewResource
        {
            get { return Library.Textures.Get(textureID).Location; }
        }
        public Texture2D Preview
        {
            get { return Library.Textures.Get(textureID).Texture2D; }
        }

        /// <summary>
        /// Только для использования в скриптах редактора!
        /// </summary>
        public uint TextureID
        {
            set { textureID = value; }
        } 

        [SerializeField]
        private string alias;
        public string Alias {
            get { return alias; }
        }

	    public string Name {
			get {
				// TODO: Логика локализации картинки
				return alias;
			}
		}

        public Page(uint id, string resourceName, string assetBundleName, uint textureID = 0)
        {
            ID = id;
            alias = resourceName;
            resource = new ResourceLocation(resourceName, assetBundleName);
            this.textureID = textureID;
        }

	}
}
