using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Contracts
{
    public interface ICacheImage // ToDo generic (T Class)
    {
        public void Set(string key, ListImagesModel obj);

        public ListImagesModel Get(string key);
       
    }
}
