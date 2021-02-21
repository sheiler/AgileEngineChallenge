using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;


namespace TestTask.ImageCache.Infrastructure.Services
{
    public class MemoryCacheImage : ICacheImage
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheImage(IMemoryCache cache)
        {
            _cache = cache;
        }


        public ListImagesModel Get(string cacheKey)
        {
            var item = (ListImagesModel)_cache.Get(cacheKey);

            return item;
        }

        public void Set(string cacheKey, ListImagesModel element)
        {
            var item = (ListImagesModel)_cache.Get(cacheKey);

            if (item == null)
            {
                _cache.Set(cacheKey, element);
            }
        }
    }
}
