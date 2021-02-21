using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.ImageCache.Infrastructure.Domain
{
    public class ListImagesModel
    {
        
        

        public ListImagesModel(DateTime now, List<ImageDetails> lista)
        {
            this.LastUpdate = now;
            this.ListImages = lista;
        }

        public List<ImageDetails> ListImages { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
