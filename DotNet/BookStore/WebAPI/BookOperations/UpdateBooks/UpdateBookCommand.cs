using System;
using System.Linq;
using WebAPI.DBOperations;

namespace WebAPI.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        public string Title { get; set; }
        public UpdateBookModel Model { get; set; }
        private BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
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
            Book.Title = Model.Title != "string" ? Model.Title : Book.Title;
            Book.PageCount = Model.PageCount != default ? Model.PageCount : Book.PageCount;
            Book.PublishDate = Model.PublishDate != default ? Model.PublishDate : Book.PublishDate;
            _dbContext.SaveChanges();
        }
    }
    
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        
        public override string ToString()
        {
            string message = new string(
                "Title       : " + Title.ToString() + "\n" + 
                "PageCount   : " + PageCount.ToString() + "\n" + 
                "PublishDate : " + PublishDate.ToString("gg/AA/yyyy") + "\n"
            );
            return message;
        }
    }
    
}