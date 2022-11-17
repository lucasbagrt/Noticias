using API.Entities.Utils;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoExternalController : ControllerBase
    {

        private readonly ILogger<NewsExternalController> _logger;
        private readonly VideoService _videoSerivce;

        public VideoExternalController(ILogger<NewsExternalController> logger, VideoService videoSerivce)
        {
            _logger = logger;
            _videoSerivce = videoSerivce;
        }
        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<VideoViewModel>> Get(int page, int qtd) => _videoSerivce.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<VideoViewModel> Get(string slug) 
        {
            var news = _videoSerivce.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }     
    }
}
