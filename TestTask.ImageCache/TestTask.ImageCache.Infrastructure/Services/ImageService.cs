using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private IAgileEngineClient AgileEngineClient { get; set; }
        private ICacheImage<ImageDetails> MemoryCacheImage { get; set; }

        public ImageService(HttpClient client, ICacheImage<ImageDetails> cacheImage)
        {
            AgileEngineClient = new AgileEngineClient(client);
            MemoryCacheImage = cacheImage;
        }

        public ImageDetails GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<ImageDetails> GetByMeta(string meta)
        {
            throw new NotImplementedException();
        }

        public List<ImageDetails> GetByPage(int page)
        {
            throw new NotImplementedException();
        }

        void IImageService.RefreshCache()
        {
            throw new NotImplementedException();
        }
    }
}
