using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.ImageCache.Infrastructure.Domain
{
    public class AuthRequest
    {
        public string apiKey { get; }
        public AuthRequest(string secret)
        {
            this.apiKey = secret;
        }
    }
}
