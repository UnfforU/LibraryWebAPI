using AutoMapper;

namespace LibraryWebAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            //CreateMap<Order, OrderDTO>()
            //    .ForPath(
            //        o => DateTime.Parse(o.StartDateTime),
            //        opt => opt.MapFrom(u => u.StartDateTime))
            //    .ForPath(
            //        o => DateTime.Parse(o.EndDateTime),
            //        opt => opt.MapFrom(u => u.EndDateTime));

            //CreateMap<OrderDTO, Order>()
            //    .ForMember(
            //        odto => odto.StartDateTime.ToString(),
            //        opt => opt.MapFrom(u => u.StartDateTime))
            //    .ForMember(
            //        odto => odto.EndDateTime.ToString(),
            //        opt => opt.MapFrom(u => u.EndDateTime));

            CreateMap<Order, OrderDTO>().ReverseMap();

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
