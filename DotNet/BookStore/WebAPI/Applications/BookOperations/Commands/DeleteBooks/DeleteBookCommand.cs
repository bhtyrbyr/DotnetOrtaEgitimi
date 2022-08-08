using System;
using System.Linq;
using WebAPI.DBOperations;

namespace WebAPI.Applications.BookOperations.Commands.DeleteBooks
{
    public class DeleteBookCommand
    {
        public string title { get; set; }
        public IBookStoreDbContext _dbContext;
        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }    

        public void Handle()
        {
            var Book = _dbContext.Books.SingleOrDefault(x=> x.Title == title);
            if(Book is null)
                throw new ArgumentException("Var olmayan bir kitap başlığı girdiniz!");
            _dbContext.Books.Remove(Book);
            _dbContext.SaveChanges();
        }
    }
}