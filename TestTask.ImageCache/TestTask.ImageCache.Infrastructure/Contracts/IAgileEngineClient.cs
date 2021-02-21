using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Contracts
{
    public interface IAgileEngineClient
    {
        public  Task<ImageModelResponse> GetAll(int page);
        public Task<ImageDetails> GetDetails(string id);
    }
}
