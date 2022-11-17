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
    public class NewsExternalController : ControllerBase
    {

        private readonly ILogger<NewsExternalController> _logger;
        private readonly NewsService _newsService;

        public NewsExternalController(ILogger<NewsExternalController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }
        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<NewsViewModel>> Get(int page, int qtd) => _newsService.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<NewsViewModel> Get(string slug) 
        {
            var news = _newsService.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }     
    }
}
