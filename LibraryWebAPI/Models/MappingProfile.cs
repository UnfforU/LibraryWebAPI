using AutoMapper;
using LibraryWebAPI.Models.DB;
using File = LibraryWebAPI.Models.DB.File;

namespace LibraryWebAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<File, FileDTO>()
                .ForMember(
                    dest => dest.FileContent,
                    opt => opt.MapFrom(f => Convert.ToBase64String(f.FileContent)));

            CreateMap<Library, LibraryDTO>()
                .ForMember(l => l.Books, (opt) => opt.Ignore())
                .ReverseMap();


            CreateMap<UserDTO, User>()
                .ForMember(
                    dest => dest.UserRoleId,
                    opt => opt.MapFrom(u => u.UserRole))
                .ForMember(u => u.UserRole, (opt) => opt.Ignore())
                .ReverseMap();
         
        }
    }
}
