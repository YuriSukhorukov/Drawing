using System;

namespace Content.Resource
{
    [Serializable]
    public struct ResourceLocation
    {
        public string BundleName;

        public string ResourceName;

        public ResourceLocation(string resourceName, string bundleName)
        {
            BundleName = bundleName;
            ResourceName = resourceName;
        }
    }
}