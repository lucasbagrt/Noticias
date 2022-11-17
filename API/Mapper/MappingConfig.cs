using API.Entities;
using API.Entities.Utils;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            #region [Entidades]
            CreateMap<News, NewsViewModel>().ReverseMap();
            CreateMap<Video, VideoViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Gallery, GalleryViewModel>().ReverseMap();
            #endregion

            #region [Resukt<T>]
            CreateMap<Result<Video>, Result<VideoViewModel>>().ReverseMap();
            CreateMap<Result<News>, Result<NewsViewModel>>().ReverseMap();
            CreateMap<Result<User>, Result<UserViewModel>>().ReverseMap();
            CreateMap<Result<Gallery>, Result<GalleryViewModel>>().ReverseMap();
            #endregion            
        }
    }
}
