using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    public static class AssetPath
    {
        // ReSharper disable InconsistentNaming
        public static readonly string STREAMINGASSETS_PATH = URLManager.CombinePath("Assets", "StreamingAssets"); 
        public static readonly string DEFAULT_RESOURCES_PATH = URLManager.CombinePath("Assets", "Content");
        public static readonly string DEFAULT_BUNDLE_EXPORT_PATH = URLManager.CombinePath(STREAMINGASSETS_PATH, "Bundles");
        public static readonly string DEFAULT_REPOSITORY_IN_SA = "Repositories";
        public static readonly string DEFAULT_REPOSITORY_EXPORT_PATH = URLManager.CombinePath(STREAMINGASSETS_PATH, DEFAULT_REPOSITORY_IN_SA);

        public static string CreateAssetPath(string path, string folder)
        {
            return CreateAssetPath(Path.Combine(path, folder));
        }
        
        public static string CreateAssetPath(string path)
        {
            string[] foldersChain = URLManager.SplitPath(path);
            if (foldersChain.Length <= 0)
            {
                return string.Empty;
            }
            string currentPath = foldersChain[0];
            for (int i = 0; i < foldersChain.Length - 1; i++)
            {
                if (!AssetDatabase.IsValidFolder(currentPath + "/" + foldersChain[i + 1]))
                {
                    AssetDatabase.CreateFolder(currentPath, foldersChain[i + 1]);
                }
                currentPath = Path.Combine(currentPath, foldersChain[i + 1]);
            }
            return path;
        }
    }
}