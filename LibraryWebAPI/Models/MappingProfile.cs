using AutoMapper;

namespace LibraryWebAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
        }
    }
}
