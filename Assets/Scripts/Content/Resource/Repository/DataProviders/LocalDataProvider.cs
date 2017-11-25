using System;
using System.IO;
using UnityEngine;

namespace Content.Resource.Repository.DataProviders
{
    public sealed class LocalDataProvider : AbstractDataProvider
    {

        private string path;
            
        public override string Source
        {
            get { return source; }
            set
            {
                source = value;
                path = Path.GetDirectoryName(source);
            }
        }

        public LocalDataProvider (string source)
        {
            Source = source;
        }
        
        public override string Read()
        {
            if (!File.Exists(Source))
            {
                throw new FileNotFoundException();
            }
            try
            {
                return File.ReadAllText(Source);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            return string.Empty;
        }

        public override void Write(string data)
        {
            URLManager.CreatePath(path);
            try
            {
                File.WriteAllText(source, data);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
        
    }
}