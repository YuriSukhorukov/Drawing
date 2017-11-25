namespace Content.Resource.Repository.DataProviders
{
    public class AbstractDataProvider : IDataProvider
    {
        protected string source;

        public virtual string Source
        {
            get { return source; } 
            set { source = value; }
        }

        public virtual string Read()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Write(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}