using API.Entities.Enums;
using API.Entities.Utils;
using API.Infra.Utils;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
    public class Gallery : BaseEntity
    {
        public Gallery(string title, string legend, string author, string tags, Status status, IList<String> galleryImages, string thumb)
        {
            Title = title;
            Legend = legend;
            Author = author;
            Thumb = thumb;
            Tags = tags;
            Slug = !string.IsNullOrEmpty(title) ? Helper.GenerateSlug(Title) : "";
            Status = status;
            GalleryImages = galleryImages;

            ValidateEntity();
        }

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("legend")]
        public string Legend { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("tags")]
        public string Tags { get; private set; }

        [BsonElement("thumb")]
        public string Thumb { get; private set; }

        [BsonElement("galleryImages")]
        public IList<string> GalleryImages { get; set; }


        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(Title, "O titulo nao pode estar vazio");
            AssertionConcern.AssertArgumentNotNullOrEmpty(Legend, "A legenda nao pode estar vazia");

            AssertionConcern.AssertArgumentLength(Title, 90, "O titulo deve ter ate 90 caracteres");
            AssertionConcern.AssertArgumentLength(Legend, 40, "A legenda deve ter ate 40 caracteres");
        }
    }
}
