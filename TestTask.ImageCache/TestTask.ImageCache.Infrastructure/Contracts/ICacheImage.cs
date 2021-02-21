using System;
using System.Collections.Generic;
using System.Text;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Contracts
{
    public interface ICacheImage<T>
    {
        public void Add(string key, T obj);
        public List<T> GetAll();
        public List<T> GetByMeta(string key);
        public T GetByKey(string id);
    }
}
