using System;

namespace Content.Resource.Repository.DataProviders
{
    public class WebDataProvider : AbstractDataProvider
    {
        public WebDataProvider() {}
        
        public WebDataProvider(string source)
        {
            Source = source;
        }

        public override string Read()
        {
            throw new NotImplementedException();
        }

        public override void Write(string data)
        {
            throw new NotImplementedException();
        }
    }
}