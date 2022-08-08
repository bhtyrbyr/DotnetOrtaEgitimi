using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.BookOperations.Commands.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
       private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Title == Model.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut!");

            if(!(_dbContext.Authors.Any(x => x.Id == Model.AuthorId))){
                throw new InvalidOperationException("Yazar bilgisi yanlış girildi!");
            }
            if(!(_dbContext.Genres.Any(x => x.Id == Model.GenreId)))
                throw new InvalidOperationException("Kitap kategorisi mevcut değildir!");

            book = _mapper.Map<Book>(Model);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}