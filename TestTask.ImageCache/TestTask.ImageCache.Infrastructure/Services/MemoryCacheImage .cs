using System;
using System.Collections.Generic;
using System.Text;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Services
{
    public class MemoryCacheImage : ICacheImage<ImageDetails>
    {
        public void Add(string key, ImageDetails obj)
        {
            throw new NotImplementedException();
        }

        public List<ImageDetails> GetAll()
        {
            throw new NotImplementedException();
        }

        public ImageDetails GetByKey(string id)
        {
            throw new NotImplementedException();
        }

        public List<ImageDetails> GetByMeta(string key)
        {
            throw new NotImplementedException();
        }
    }
}
