using System;
using System.Linq;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.BookOperations.Commands.UpdateBooks
{
    public class UpdateBookCommand
    {
        public string Title { get; set; }
        public UpdateBookModel Model { get; set; }
        private IBookStoreDbContext _dbContext;
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var Book = _dbContext.Books.Where(book => book.Title == Title).SingleOrDefault();
            if(Book is null){
                string message = Title + " - Var olmayan bir kitap başlığı girdiniz!";
                throw new InvalidOperationException(message);
            }
            if(!(_dbContext.Authors.Any(x => x.Id == Model.AuthorId)))
                throw new InvalidOperationException("Yazar bilgisi yanlış girildi!");            
            if(!(_dbContext.Genres.Any(x => x.Id == Model.GenreId)))
                throw new InvalidOperationException("Kitap kategorisi mevcut değildir!");

            Book.Title = Model.Title != "string" ? Model.Title : Book.Title;
            Book.GenreId = Model.GenreId != default ? Model.GenreId : Book.GenreId;
            Book.AuthorId = Model.AuthorId != default ? Model.AuthorId : Book.AuthorId;
            Book.PageCount = Model.PageCount != default ? Model.PageCount : Book.PageCount;
            Book.PublishDate = Model.PublishDate != default ? Model.PublishDate : Book.PublishDate;
            _dbContext.SaveChanges();
        }
    }
    
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
    
}