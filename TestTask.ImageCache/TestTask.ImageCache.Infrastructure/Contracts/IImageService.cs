using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Contracts
{
    public interface IImageService
    {
        public Task<ImageDetails> GetById(string id);
        public Task<List<ImageDetails>> GetByMeta(string meta);
        public Task<List<ImageDetails>> GetByPage(int page);

        public void FirstLoad();
    }
}
