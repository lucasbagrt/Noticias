using API.Entities.Enums;
using API.Entities.Utils;
using API.Infra.Utils;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Entities
{
    public class News : BaseEntity
    {

        public News(string hat, string title, string text, string author, string img, Status status)
        {
            Hat = hat;
            Title = title;
            Text = text;
            Author = author;
            Img = img;            
            Included = DateTime.Now;
            Slug = Helper.GenerateSlug(Title);
            Status = status;

            ValidateEntity();
        }


        public Status ChangeStatus(Status status)
        {
            switch (status)
            {
                case Status.Active:
                    status = Status.Active;
                    break;
                case Status.Inactive:
                    status = Status.Inactive;
                    break;
                case Status.Draft:
                    status = Status.Draft;
                    break;              
            }

            return status;
        }

        [BsonElement("hat")]
        public string Hat { get; private set; }

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("text")]
        public string Text { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("img")]
        public string Img { get; private set; }        

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O titulo nao pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "O chapeu nao pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Text, "O texto nao pode estar vazio");

            AssertionConcern.AssertArgumentLength(Title, 90, "O titulo deve ter ate 90 caracteres");
            AssertionConcern.AssertArgumentLength(Hat, 40, "O chapeu deve ter ate 40 caracteres");
        }
    }
}
