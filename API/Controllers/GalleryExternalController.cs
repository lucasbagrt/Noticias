using API.Entities.Utils;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GalleryExternalController : ControllerBase
    {
        private readonly ILogger<GalleryExternalController> _logger;
        private readonly GalleryService _galleryService;

        public GalleryExternalController(ILogger<GalleryExternalController> logger, GalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.Get(page, qtd);


        [HttpGet("{slug}")]
        public ActionResult<GalleryViewModel> Get(string slug)
        {
            var news = _galleryService.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }
    }
}
