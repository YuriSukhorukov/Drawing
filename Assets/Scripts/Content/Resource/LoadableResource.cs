namespace Content.Resource
{

    public class LoadableResource<T> where T:UnityEngine.Object
    {
        private readonly ResourceLocation resource;

        public LoadableResource(ResourceLocation resource)
        {
            this.resource = resource;
        }
        
        public T Load()
        {
            return ResourceLoader.Load<T>(resource);
        }
    }

}