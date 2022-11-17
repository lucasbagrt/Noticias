using API.Services;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> _logger;
        private readonly UploadService _uploadService;
        public UploadController(ILogger<UploadController> logger, UploadService uploadService)
        {
            _logger = logger;
            _uploadService = uploadService;
        }
        [HttpPost]
        public IActionResult Post(IFormFile file)
        {
            try
            {
                if (file == null) return BadRequest("Arquivo invalida");

                var urlFile = _uploadService.UploadFile(file);

                return Ok(new
                {
                    message = "Imagem salva com sucesso",
                    urlImagem = urlFile
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro no upload: " + ex.Message);
            }
        }
    }
}
