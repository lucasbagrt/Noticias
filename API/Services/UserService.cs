using API.Entities.Utils;
using API.Entities.ViewModels;
using API.Entities;
using API.Infra;
using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using API.Services.Cache;

namespace API.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _user;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "user";
        public UserService(IUserRepository user, IMapper mapper, ICacheService cacheService)
        {
            _mapper = mapper;
            _user = user;
            _cacheService = cacheService;
        }

        public Result<UserViewModel> Get(int page, int qtd)
        {
            var keyCache = $"{keyForCache}/{page}/{qtd}";
            var users = _cacheService.Get<Result<UserViewModel>>(keyCache);

            if (users is null)
            {
                users = _mapper.Map<Result<UserViewModel>>(_user.Get(page, qtd));
                _cacheService.Set(keyCache, users);
            }
            return users;
        }


        public UserViewModel Get(string id)
        {
            var cacheKey = $"{keyForCache}/{id}";
            var user = _cacheService.Get<UserViewModel>(cacheKey);

            if (user is null)
            {
                user = _mapper.Map<UserViewModel>(_user.Get(id));
                _cacheService.Set(cacheKey, user);
            }
            return user;
        }      

        public UserViewModel Create(UserViewModel user)
        {
            var entity = new User(user.Username, BCrypt.Net.BCrypt.HashPassword(user.Password), user.Email, user.Role, user.Status);
            _user.Create(entity);

            var cacheKey = $"{keyForCache}/{entity.Username}";
            _cacheService.Set(cacheKey, entity);

            return Get(entity.Id);
        }

        public void Update(string id, UserViewModel user)
        {
            var cacheKey = $"{keyForCache}/{id}";
            _user.Update(id, _mapper.Map<User>(user));

            _cacheService.Remove(cacheKey);
            _cacheService.Set(cacheKey, user);
        }

        public void Remove(string id)
        {
            var cacheKey = $"{keyForCache}/{id}";
            _user.Remove(cacheKey);

            var user = Get(id);
            cacheKey = $"{keyForCache}/{user.Username}";
            _user.Remove(cacheKey);

            _cacheService.Remove(cacheKey);
        }

        public UserViewModel GetByUsername(string username)
        {
            var cacheKey = $"{keyForCache}/{username}";
            var user = _cacheService.Get<UserViewModel>(cacheKey);

            if (user is null)
            {
                user = _mapper.Map<UserViewModel>(_user.GetByUsername(username));
                _cacheService.Set(cacheKey, user);
            }
            return user;            
        }

        public bool ValidateUser(UserViewModel user)
        {
            var entity = _user.GetByUsername(user.Username);
            if (entity is null) return false;

            return BCrypt.Net.BCrypt.Verify(user.Password, entity.Password);
        }
    }
}
