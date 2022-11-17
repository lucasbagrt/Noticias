using API.Entities.Enums;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using ImageProcessor;
using API.Entities.Utils;

namespace API.Services
{
    public class UploadService
    {
        public string UploadFile(IFormFile file)
        {
            var validateTypeMedia = GetTypeMedia(file.FileName);
            return validateTypeMedia == Media.Image ? UploadImage(file) : UploadVideo(file);
        }

        public Media GetTypeMedia(string filename)
        {
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".webp" };

            string[] videoExtensions = {
                ".avi", ".mp4" };

            var fileInfo = new FileInfo(filename);

            return imageExtensions.Contains(fileInfo.Extension) ? Media.Image : videoExtensions.Contains(fileInfo.Extension) ? Media.Video : throw new DomainException("Formato errado de arquivo!"); ;
        }

        private string UploadImage(IFormFile file)
        {
            using (var stream = new FileStream(Path.Combine("Medias/Imagens", file.FileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var urlfile = Guid.NewGuid() + ".webp";
            
            using (var webPFileStream = new FileStream(Path.Combine("Medias/Imagens", urlfile), FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(file.OpenReadStream())
                                .Format(new WebPFormat()) 
                                .Quality(100) 
                                .Save(webPFileStream); 
                }
            }

            return $"http://localhost:7287/medias/imagens/{urlfile}";
        }

        private string UploadVideo(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);

            var fileName = Guid.NewGuid() + fi.Extension;
            
            using (var stream = new FileStream(Path.Combine("Medias/Videos", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"http://localhost:7287/medias/videos/{fileName}";
        }
    }
}
