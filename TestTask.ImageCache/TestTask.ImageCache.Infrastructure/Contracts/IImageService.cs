using System;
using System.Collections.Generic;
using System.Text;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Contracts
{
    public interface IImageService
    {
        public ImageDetails GetById(string id);
        public List<ImageDetails> GetByMeta(string meta);
        public List<ImageDetails> GetByPage(int page);
        protected void RefreshCache();
    }
}
