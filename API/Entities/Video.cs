using API.Entities.Enums;
using API.Entities.Utils;
using API.Infra.Utils;
using MongoDB.Bson.Serialization.Attributes;
using static System.Net.Mime.MediaTypeNames;

namespace API.Entities
{
    public class Video : BaseEntity
    {
        public Video(string hat, string title, string author, string thumbnail, string urlVideo, Status status)
        {
            Hat = hat;
            Title = title;            
            Author = author;
            Thumbnail = thumbnail;
            Included = DateTime.Now;
            Slug = Helper.GenerateSlug(Title);
            Status = status;
            UrlVideo = urlVideo;
            ValidateEntity();
        }

        [BsonElement("hat")]
        public string Hat { get; private set; }

        [BsonElement("title")]
        public string Title { get; private set; }     

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("thumbnail")]
        public string Thumbnail { get; private set; }
        [BsonElement("urlVideo")]
        public string UrlVideo { get; private set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O titulo nao pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "O chapeu nao pode estar vazio");            

            AssertionConcern.AssertArgumentLength(Title, 90, "O titulo deve ter ate 90 caracteres");
            AssertionConcern.AssertArgumentLength(Hat, 40, "O chapeu deve ter ate 40 caracteres");
        }
    }
}
