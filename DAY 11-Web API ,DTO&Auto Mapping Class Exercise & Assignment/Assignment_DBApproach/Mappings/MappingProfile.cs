using Assignment_DBApproach.Models;
using Assignment_DBApproach.DTOs;
using AutoMapper;

namespace Assignment_DBApproach.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping Author to AuthorDTO (hides AuthorId, shows Name)
            CreateMap<Author, AuthorDTO>();

            // Mapping Book to BookDTO (shows only required fields)
            CreateMap<Book, BookDTO>();
            CreateMap<Category, CategoryDTO>();

        }
    }
}
