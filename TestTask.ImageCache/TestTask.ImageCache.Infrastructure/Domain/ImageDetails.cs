using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.ImageCache.Infrastructure.Domain
{
    public sealed class ImageDetails : ImageBase
    {
        public string author { get; set; }
        public string camera { get; set; }
        public string tags { get; set; }
        public string full_picture { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}",this.author.ToLower(), this.camera.ToLower(), this.tags.ToLower()); 
        }

    }
}
