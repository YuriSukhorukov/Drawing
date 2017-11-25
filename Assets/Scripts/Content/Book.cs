using System;
using System.Collections.Generic;
using Content.Resource;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Book : IHaveID
    {
        
        [SerializeField]
        private uint id;
        public uint ID
        {
            get { return id; }
            private set { id = value; }
        }

        [SerializeField]
        private List<uint> pagesIDs;

        private List<Page> pages = new List<Page>();

        public List<Page> Pages
        {
            get
            {
                // TODO Почему pages == null???
                if (pages == null || pages.Count == 0)
                {
                    pages = Library.Pages.Get(pagesIDs);
                }
                return pages;
            }
        }
        
        [SerializeField]
        private List<string> tags = new List<string>();
        public List<string> Tags
        {
            get { return tags; }
        }

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

        [SerializeField]
        private string alias;
        public string Alias {
            get { return alias; }
        }

        public string Name {
            get {
                // TODO: Логика локализации набора картинок
                return alias;
            }
        }

        public Book(uint id, string alias)
        {
            ID = id;
            this.alias = alias;
        }
        
    }
}