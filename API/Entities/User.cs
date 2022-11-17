using API.Entities.Enums;
using API.Entities.Utils;
using API.Infra;
using MongoDB.Bson.Serialization.Attributes;
using static System.Net.Mime.MediaTypeNames;

namespace API.Entities
{
    public class User : BaseEntity
    {
        public User(string username, string password, string email, Role role, Status status)
        {
            Username = username;
            Password = password;
            Email = email;
            Included = DateTime.Now;
            Status = status;
            Role = role;

            ValidateEntity();
        }

        [BsonElement("username")]
        public string Username { get; private set; }

        [BsonElement("password")]
        public string Password { get; private set; }

        [BsonElement("email")]
        public string Email { get; private set; }
        [BsonElement("role")]
        public Role Role { get; protected set; }
        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(Username, "O usuario nao pode estar vazio");
            AssertionConcern.AssertArgumentNotNullOrEmpty(Password, "A senha nao pode estar vazia");
            AssertionConcern.AssertArgumentNotNullOrEmpty(Email, "O email nao pode estar vazio");

            AssertionConcern.AssertArgumentNotNull(Role, "Selecione uma Role para o Usuario");

            AssertionConcern.AssertArgumentLength(Username, 50, "O usuario deve ter ate 50 caracteres");
            AssertionConcern.AssertArgumentLength(Email, 50, "O email deve ter ate 50 caracteres");

            AssertionConcern.AssertEmailNotValid(Email, "O email deve ser valido");
        }
    }
}
