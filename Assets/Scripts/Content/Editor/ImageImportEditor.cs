using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using ColoringBook.Serialization;
using Geometry.MeshGeometry;
using UnityEditor;
using UnityEngine;
using Utils.Editor;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace Content
{
    
    // ЗДЕСЬ У НАС КОСТЫЛИ, ГЛАД, МОР И ПИЗДЕЦ-ЦЗЫ!

    [CustomEditor(typeof(ImageImportTool))]
    public class ImageImportEditor : Editor
    {

        public const string PAGE_PREFIX = "page_";

        private static string lastUsedAssetPath;

        private static ImageImportTool wizard;
        
        #region === Custom Inspector Actions bindings ===
        public override void OnInspectorGUI()
        {
            wizard = target as ImageImportTool;

            DrawDefaultInspector();

            if (GUILayout.Button(new GUIContent("Export Geometry", "Export current mesh")))
            {
                export();
            }

//            if (GUILayout.Button("Test Geometry"))
//            {
//                buildBundles();
//                run();
//            }
            if (GUILayout.Button("Build Previews"))
            {
                buildPreviewRepository();
            }

//            if (GUILayout.Button("Export & Test"))
//            {
//                export();
//                buildBundles();
//                run();
//            }
            if (GUILayout.Button("Finalize Content"))
            {
                buildBundles();
            }
        }
        #endregion

        private static void export()
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            
            TextureRepository texturesRepository = new TextureRepository();
            texturesRepository.FromJSON(File.ReadAllText(
                URLManager.CombinePath(
                    Application.streamingAssetsPath, 
                    AssetPath.DEFAULT_REPOSITORY_IN_SA, 
                    URLManager.DEFAULT_TEXTURE_REPOSITORY_FILENAME)
            ));
            
            PageRepository pagesRepository = new PageRepository();
            pagesRepository.FromJSON(File.ReadAllText(
                URLManager.CombinePath(
                    Application.streamingAssetsPath, 
                    AssetPath.DEFAULT_REPOSITORY_IN_SA, 
                    URLManager.DEFAULT_PAGE_REPOSITORY_FILENAME)
            ));
            
            lastUsedAssetPath =
                AssetPath.CreateAssetPath(AssetPath.DEFAULT_RESOURCES_PATH, wizard.Folder) + "/" +
                PAGE_PREFIX + wizard.Name + ".asset";
            ImageData data = exportMeshData();
            
            AssetDatabase.CreateAsset(data, lastUsedAssetPath);
            AssetImporter asset = AssetImporter.GetAtPath(lastUsedAssetPath);

            string bundleName = wizard.Folder + ".unity3d";

            asset.SetAssetBundleNameAndVariant(bundleName, "");
            
            AssetDatabase.SaveAssets();
            
            // Модификация реестра страниц
            string previewPath = AssetDatabase.GetAssetPath(wizard.PreviewTexture);
            
            uint textureID = texturesRepository.Find(t => t.Location.ResourceName == previewPath).ID;

            List<uint> allPagesIDs = pagesRepository.FindAll(p => true).Select(p => p.ID).ToList();
            uint maxPageID = 0;
            allPagesIDs.ForEach(id => maxPageID = (id > maxPageID) ? id : maxPageID);

            Page oldPage = pagesRepository.Find(p => p.Location.ResourceName == lastUsedAssetPath);
            if (oldPage == null)
            {
                pagesRepository.Add(new Page(
                    maxPageID + 1,
                    lastUsedAssetPath,
                    bundleName,
                    textureID
                ));
            }
            else
            {
                pagesRepository.Storage[oldPage.ID].TextureID = textureID;
//                    = new Page(
//                    oldPage.ID,
//                    oldPage.Location.ResourceName,
//                    bundleName,
//                    textureID
//                );
            }
            
            File.WriteAllText(URLManager.CombinePath(
                    Application.streamingAssetsPath, 
                    AssetPath.DEFAULT_REPOSITORY_IN_SA, 
                    URLManager.DEFAULT_PAGE_REPOSITORY_FILENAME), 
                pagesRepository.ToJSON());

            sw.Stop();
            Debug.Log(string.Format("Import completed. Time elapsed: {0}ms", sw.ElapsedMilliseconds));
        }

        private static void buildBundles()
        {   
            AssetBundleBuild[] existingBundles = AssetDatabase.GetAllAssetBundleNames().Select(
                    bundleName =>
                    {
                        Debug.Log(string.Format("Packing Bundle: {0}", bundleName));
                        string[] assets = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
                        foreach (string pageName in assets)
                        {
                            Debug.Log(string.Format("\tAsset: \"{0}\"", pageName));
                        }
                        return new AssetBundleBuild {
                            assetBundleName = bundleName,
                            assetBundleVariant = "",
                            assetNames = assets
                        };
                    }).ToArray();

            AssetPath.CreateAssetPath(AssetPath.DEFAULT_BUNDLE_EXPORT_PATH);

            BuildPipeline.BuildAssetBundles
            (
                AssetPath.DEFAULT_BUNDLE_EXPORT_PATH,
                existingBundles,
                BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget
            );
        }

        private static void run()
        {
            EditorApplication.isPlaying = false;
            if (!Application.isPlaying) {
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }
        }

        private static ImageData exportMeshData()
        {
            if (wizard.SourceMesh == null)
            {
                Debug.LogError("No mesh to import found!");
                return null;
            }
            try
            {
                ImageData asset = ScriptableObject.CreateInstance<ImageData>();
                asset.RawImagePaths = wizard.SourceMesh.GetMeshContours(wizard.RegionSizeTreshold);

                MeshContourTree tree = new MeshContourTree(asset.ImagePaths);

                List<List<int>> regionsPaths = tree.GetInnerFaces();
                foreach (List<int> face in regionsPaths)
                {
                    asset.AddRegion(face);
                }
                Mesh cMesh = MeshUtils.BuildMesh(asset.ImagePaths);

                cMesh.FlipTriangles();
                cMesh.FlipNormals();

                asset.Contour = cMesh;
                return asset;
            }
            catch (UnityException e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        private static void buildPreviewRepository()
        {
            uint i = 0;
            TextureRepository repository = new TextureRepository();
            
            AssetBundleBuild[] existingBundles = AssetDatabase.GetAllAssetBundleNames().Select(
                    bundleName =>
                    {
                        Debug.Log(string.Format("Scanning Bundle: {0}", bundleName));
                        string[] assets = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
                        foreach (string pageName in assets)
                        {
                            if (pageName == lastUsedAssetPath)
                            {
                                wizard.GameContainer.TestImageID = i;
                            }
                            if (pageName.Contains(".jpg"))
                            {
                                repository.Add(new Texture(
                                    i,
                                    pageName,
                                    bundleName
                                ));
                                Debug.Log(string.Format("\tAsset: \"{0}\"", pageName));
                                Debug.Log(repository.ToJSON());
                            }
                            i++;
                        }
                        return new AssetBundleBuild {
                            assetBundleName = bundleName,
                            assetBundleVariant = "",
                            assetNames = assets
                        };
                    }).ToArray();

            AssetPath.CreateAssetPath(AssetPath.DEFAULT_REPOSITORY_EXPORT_PATH);

            File.WriteAllText(URLManager.CombinePath(
                Application.streamingAssetsPath, 
                AssetPath.DEFAULT_REPOSITORY_IN_SA, 
                URLManager.DEFAULT_TEXTURE_REPOSITORY_FILENAME), 
                repository.ToJSON());
        }

    }
}
