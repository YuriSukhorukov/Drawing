using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class URLManager
{

    #if UNITY_ANDROID
    public static readonly string ServerContentList = "http://138.197.96.226/upload/cb0/android/index.json";
    #else
    public static readonly string ServerContentList = "http://138.197.96.226/upload/cb0/ios/index.json";
    #endif
    
    public static readonly string StreamingAssetsFolder = Application.streamingAssetsPath;
    
    public static readonly string AssetBundleStorageFolder = CombinePath("Bundles");

    public static readonly string RepositoryStorageFolder = CombinePath("Repositories");
    
    public const string DEFAULT_PAGE_REPOSITORY_FILENAME = "default_page_repository.json";
    
    public const string DEFAULT_BOOK_REPOSITORY_FILENAME = "default_book_repository.json";

    public const string DEFAULT_TEXTURE_REPOSITORY_FILENAME = "default_texture_repository.json";

    public static readonly string LocalBookRepository = CombinePath(
        Local(RepositoryStorageFolder),
        DEFAULT_BOOK_REPOSITORY_FILENAME
    );
    
    public static readonly string LocalPageRepository = CombinePath(
        Local(RepositoryStorageFolder), 
        DEFAULT_PAGE_REPOSITORY_FILENAME
    );
    
    public static readonly string LocalTextureRepository = CombinePath(
        Local(RepositoryStorageFolder), 
        DEFAULT_TEXTURE_REPOSITORY_FILENAME
    );

    public static readonly string StreamingPageRepository = CombinePath(
        InStreamingAssets(RepositoryStorageFolder), 
        DEFAULT_PAGE_REPOSITORY_FILENAME
    );

    #region === Обертки над разными корнями для путей к файлам ===
    public static string Local(string path)
    {
        return CombinePath(Application.persistentDataPath, path);
    }
    
    public static string InStreamingAssets(string path)
    {
        return CombinePath(StreamingAssetsFolder, path);
    }
    #endregion

	#region === Вспомогательные методы для обработки путей ===
    public static string CombinePath (params string[] components) {
		string path = string.Empty;
		if (components.Length <= 0)
		{
			return path;
		}
		path = components [0];
		for (int c = 1; c < components.Length; c++) {
			path = Path.Combine (path, components [c]);
		}
		return path;
	}

	public static string[] SplitPath (string path)
	{
		return path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
	}
    #endregion

    #region === Работа с локальной файловой системой в рантайме ===

    public static void CreatePath(string path)
    {
        Directory.CreateDirectory(path);    
    }

    #endregion 

}
