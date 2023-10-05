﻿using AutoMapper;
using LibraryWebAPI.Models.Extra;
using System.Drawing.Text;

namespace LibraryWebAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();

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
