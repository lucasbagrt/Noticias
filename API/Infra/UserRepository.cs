using API.Entities;
using API.Entities.Utils;
using API.Entities.ViewModels;
using API.Infra.Utils;
using MongoDB.Driver;

namespace API.Infra
{
    public interface IUserRepository : IMongoRepository<User>
    {        
        User GetByUsername(string userName);
    }
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseSettings settings) : base(settings)
        {

        }
        public User GetByUsername(string userName) =>
            _model.Find<User>(user => user.Username == userName).FirstOrDefault();       
    }
}
