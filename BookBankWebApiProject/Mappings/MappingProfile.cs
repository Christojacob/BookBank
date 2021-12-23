using AutoMapper;
using BookBankLibrary.Models;
using BookBankWebApiProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Author, AuthorWithoutBooksDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<AuthorForUpdationDto, Author>();
        }
    }
}
