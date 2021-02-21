using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.ImageCache.Infrastructure.Domain
{
    public class ImageModelResponse
    {
        public int page { get; set; }
        public int pageCount { get; set; }
        public bool hasMore { get; set; }
        public List<ImageBase> pictures { get; set; }

    }
}
