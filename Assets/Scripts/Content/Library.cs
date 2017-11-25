using System.IO;
using UnityEngine;
using Utils;

namespace Content
{
    public class Library : AbstractSingleton<Library>
    {

        private readonly PageRepository pages = new PageRepository();
        /// <summary>
        /// Список картинок для раскрашивания
        /// </summary>
        public static PageRepository Pages { get { return Instance.pages; }}
        
        private readonly BookRepository books = new BookRepository();
        /// <summary>
        /// Список пакетов картинок
        /// </summary>
        public static BookRepository Books { get { return Instance.books; }}

        private readonly TextureRepository textures = new TextureRepository();
        /// <summary>
        /// Список текстур
        /// </summary>
        public static TextureRepository Textures { get { return Instance.textures; } }

        public Library()
        {
            string repositoryJSON = File.ReadAllText(URLManager.LocalPageRepository);
            pages.FromJSON(repositoryJSON);
            
            repositoryJSON = File.ReadAllText(URLManager.LocalBookRepository);
            books.FromJSON(repositoryJSON);
            
            repositoryJSON = File.ReadAllText(URLManager.LocalTextureRepository);
            textures.FromJSON(repositoryJSON);
            
        }
    }
}