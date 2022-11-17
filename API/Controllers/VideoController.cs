using API.Entities.Enums;
using API.Entities.Utils;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly VideoService _videoService;

        public VideoController(ILogger<VideoController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<VideoViewModel>> Get(int page, int qtd) => _videoService.Get(page, qtd);

        [HttpGet("{id:length(24)}", Name = "GetVideos")]
        public ActionResult<VideoViewModel> Get(string id)
        {
            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            return video;
        }

        [HttpPost]
        public ActionResult<VideoViewModel> Create(VideoViewModel video)
        {
            var result = _videoService.Create(video);

            return CreatedAtRoute("GetVideos", new { id = result.Id.ToString() }, result);
        }


        [HttpPut("{id:length(24)}")]
        public ActionResult<VideoViewModel> Update(string id, VideoViewModel videoIn)
        {
            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            _videoService.Update(id, videoIn);

            return CreatedAtRoute("GetVideos", new { id = id }, videoIn);

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            _videoService.Remove(video.Id);

            return Ok("Video deletado com sucesso!");
        }
    }
}
