using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.DBOperations;

namespace WebAPI.BookOperations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel NewBook { get; set; }

        private readonly BookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Title == NewBook.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut!");
            _dbContext.Books.Add(
                //Mapper sonrası:

                _mapper.Map<Book>(NewBook)
                
                //Mapper öncesi :

                /*new Book(){
                Title = NewBook.Title,
                PublishDate = NewBook.PublishDate,
                GenreId = (int)NewBook.convertEnumToGenreId(),
                PageCount = NewBook.PageCount
            }*/);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

        public int convertEnumToGenreId()
        {
            int i = 0;
            Enum.TryParse(Genre, out GenreEnum myStatus);
            i = Convert.ToInt32(myStatus);
            if(i == 0)
                throw new InvalidOperationException("Tür sınıfı tanımsız. Geçerli bir tür girin");
            return i;
        }
    }
}