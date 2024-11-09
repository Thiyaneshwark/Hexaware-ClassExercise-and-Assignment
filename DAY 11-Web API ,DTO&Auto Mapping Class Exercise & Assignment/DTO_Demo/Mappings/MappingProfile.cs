using AutoMapper;
using DTO_Demo.Models;

namespace DTO_Demo.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile() 
        {
            //CreateMap<Book, BookDTO>();
            //CreateMap<BookDTO, Book>();

            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
