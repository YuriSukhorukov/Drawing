using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;

namespace Content.Resource
{
    /// <summary>
    /// Обертка над загрузкой ресурсов из ассет-бандлов. 
    /// Запрошенные бандлы остаются в памяти, для выгрузки - использовать ResourceLoader.Unload(All)Bundle(s) 
    /// </summary>
    public static class ResourceLoader
    {
        /// <summary>
        /// Расположение файлов ассетбандлов с ресурсами
        /// </summary>
        private static readonly string bundlesStoragePath = URLManager.Local(URLManager.AssetBundleStorageFolder);

        private static readonly Dictionary<string, AssetBundle> loadedBundles = new Dictionary<string, AssetBundle>();
        
        
        /// <summary>
        /// Загружает ресурс по указанному ResourceLocation
        /// </summary>
        /// <param name="resource">Адрес ресурса</param>
        /// <typeparam name="T">Тип загружаемого ресурса</typeparam>
        /// <returns>Загруженный ресурс</returns>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл бандла не найден</exception>
        // ReSharper disable once RedundantNameQualifier
        public static T Load<T>(ResourceLocation resource) where T:UnityEngine.Object
        {
            string path = string.IsNullOrEmpty(bundlesStoragePath)
                ? resource.BundleName
                : Path.Combine(bundlesStoragePath, resource.BundleName);
            
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            AssetBundle bundle = LoadBundle(resource.BundleName, path);
            T newData = bundle.LoadAsset<T>(resource.ResourceName);
            return newData;
        }

        /// <summary>
        /// Выгрузка всех открытых AssetBunle
        /// </summary>
        /// <param name="unloadBundleAssets">Выгружать ли из памяти загруженные из бандлов ресурсы</param>
        public static void UnloadAllBundles(bool unloadBundleAssets = true)
        {
            DebugLog.LogFormat("UNLOAD BUNDLES");

            List<string> keys = new List<string>(loadedBundles.Keys);
            foreach (var bundleName in keys)
            {
                UnloadBundle(bundleName, unloadBundleAssets);
            }
        }

        /// <summary>
        /// Выгрузка указанного AssetBundle
        /// </summary>
        /// <param name="bundleName">Имя AssetBundle</param>
        /// <param name="unloadBundleAssets">Выгружать ли из памяти загруженные из бандла ресурсы</param>
        public static void UnloadBundle(string bundleName, bool unloadBundleAssets = true)
        {
            if (loadedBundles.ContainsKey(bundleName))
            {
                AssetBundle ab = loadedBundles[bundleName];
                loadedBundles.Remove(bundleName);
                try
                {
                    ab.Unload(unloadBundleAssets);
                }
                catch (Exception ex)
                {
                    DebugLog.LogException(ex);    
                }
                DebugLog.LogFormat("UNLOADED BUNDLE: {0}", bundleName);
            }
        }

        private static AssetBundle LoadBundle(string bundleName, string path)
        {
            if (loadedBundles.ContainsKey(bundleName))
            {
                DebugLog.LogFormat("REQUESTED PRELOADED BUNDLE: {0}", bundleName);

                return loadedBundles[bundleName];
            }

            DebugLog.LogFormat("REQUESTED NEW BUNDLE: {0}", bundleName);

            AssetBundle ab = AssetBundle.LoadFromFile(path);
            loadedBundles.Add(bundleName, ab);
            return ab;
        }

    }
}