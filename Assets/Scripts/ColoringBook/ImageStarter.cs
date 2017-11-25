using Content;
using Content.Resource;
using Game;
using UnityEngine;
using Utils;

namespace ColoringBook
{
    [RequireComponent(typeof(Image))]
    public class ImageStarter : MonoBehaviour
    {
        private Image image;

        [SerializeField]
        private uint testImageID;
        public uint TestImageID
        {
            get
            {
                return testImageID;
            }
            set
            {
                testImageID = value;
            }
        }

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            ResourceLoader.UnloadAllBundles();
            // TODO Переписать по феншую
            var sw = System.Diagnostics.Stopwatch.StartNew();
            if (GameManager.Instance.selectedPageId > -1)
            {
                testImageID = (uint)GameManager.Instance.selectedPageId;
            }
            image.CurrentPage = Library.Pages.Get(testImageID);
            UpdateBounds(); // TODO: Костыль, надо будет убрать
            sw.Stop();
            DebugLog.LogFormat("DONE {0}", sw.ElapsedMilliseconds);
            
        }

        private void UpdateBounds()
        {
            var myBounds = new Bounds ( Vector3.zero, new Vector3 (0,0,0));
            foreach (Transform child in transform) {
                myBounds.Encapsulate (child.GetComponent<Collider2D>().bounds);
            }
            float scale = 21f / Mathf.Max(myBounds.size.x, myBounds.size.y); // TODO MAGIC NUMBERZ!
            transform.localScale = new Vector3(scale, scale, 1f);
            Vector3 position = transform.localPosition;
            position.y += 1.32f; // TODO MAGIC NUMBERZ!
            transform.localPosition = position;
            DebugLog.LogFormat("BOUNDS: {0} {1}", myBounds.center, myBounds.size);
        }
    }
}
