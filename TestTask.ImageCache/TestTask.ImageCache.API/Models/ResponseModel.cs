using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.ImageCache.API.Models
{
    public class ResponseModel<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public ResponseModel(bool _success, string _message, T d)
        {
            success = _success;
            data = d;
            message = _message;
        }
    }
}
