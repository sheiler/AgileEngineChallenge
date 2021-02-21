using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ImageCache.API.Models;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;

namespace TestTask.ImageCache.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }

        [HttpGet("{page}", Name = "Get")]
        public ResponseModel<List<ImageDetails>> Get(int page = 1)
        {
            try
            {
                return new ResponseModel<List<ImageDetails>>(true, "", this._service.GetByPage(page));
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageDetails>>(false, ex.Message, null);
            }
        }
        [HttpGet("{meta}", Name = "GetByMeta")]
        public ResponseModel<List<ImageDetails>> GetByMeta(string meta)
        {
            try
            {
                return new ResponseModel<List<ImageDetails>>(true, "", this._service.GetByMeta(meta));
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageDetails>>(false, ex.Message, null);
            }
        }
        [HttpGet("{id}", Name = "GetById")]
        public ResponseModel<ImageDetails> GetById(string id)
        {
            try
            {
                return new ResponseModel<ImageDetails>(true, "", this._service.GetById(id));
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageDetails>(false, ex.Message, null);
            }
        }
    }
}
