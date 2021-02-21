using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        const int pageCount = 50; // ToDo app.config
        const int refreshTime = 15; //in minutes ToDo app.config
        private IAgileEngineClient AgileEngineClient { get; set; }
        private ICacheImage MemoryCacheImage { get; set; }

        public ImageService(HttpClient client, ICacheImage cacheImage)
        {
            AgileEngineClient = new AgileEngineClient(client);
            MemoryCacheImage = cacheImage;
        }

        public async Task<ImageDetails> GetById(string id)
        {
            var lst = await this.GetAllFromCache();
            return lst.SingleOrDefault(d=>d.id == id);
        }

        public async Task<List<ImageDetails>> GetByMeta(string meta)
        {
            var lst = await this.GetAllFromCache();
            return lst.Where(d=>d.ToString().Contains(meta.ToLower())).ToList();
        }

        public async Task<List<ImageDetails>> GetByPage(int page)
        {
            var list = await this.GetAllFromCache();
            int skip = (page - 1) * pageCount;
            int count = list.Count >= (skip + pageCount) ? pageCount : list.Count-skip;

            return  list.Skip(skip).Take(count).ToList();
        }

        async Task<List<ImageDetails>> GetAllFromCache() { 
            var lst = MemoryCacheImage.Get("ImageList"); 

            if(lst.LastUpdate < DateTime.Now.AddMinutes(-refreshTime))
            {
                this.RefreshCache();
                lst = MemoryCacheImage.Get("ImageList");
            }

            return lst.ListImages;
        }

        void RefreshCache() // must be synchronic
        {
            int page = 1;

            List<ImageDetails> lista = new List<ImageDetails>();
            
            var result = AgileEngineClient.GetAll(page).Result;

            while (result.hasMore)
            {
                foreach(var pic in result.pictures)
                {
                    lista.Add(AgileEngineClient.GetDetails(pic.id).Result);
                }

                page++;
                result = AgileEngineClient.GetAll(page).Result;
            }

            var imageListModel = new ListImagesModel(DateTime.Now, lista);

            MemoryCacheImage.Set("ImageList",imageListModel);

        }

        public void FirstLoad()
        {
           RefreshCache();
        }
    }
}
