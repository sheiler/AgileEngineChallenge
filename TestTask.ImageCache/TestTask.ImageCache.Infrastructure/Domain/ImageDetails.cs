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

        public override string ToString() // ugly
        {
            return string.Format("{0}{1}{2}",
               this.author == null ? "" :  this.author.ToLower(),
               this.camera == null ? "" : this.camera.ToLower(),
               this.tags == null ? "" : this.tags.ToLower()) ; 
        }

    }
}
