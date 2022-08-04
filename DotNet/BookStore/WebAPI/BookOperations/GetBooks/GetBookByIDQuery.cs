using System;
using System.Linq;
using WebAPI.Common;
using WebAPI.DBOperations;

namespace WebAPI.BookOperations.GetBooks
{
    public class GetBookByIDQuery
    {
        public string Title { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIDQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModel Handle()
        {
            var bookList = _dbContext.Books.Where(x => x.Title == Title).SingleOrDefault();
            if(bookList is null){
                throw new ArgumentException("Var olmayan bir kitap başlığı girdiniz!");
            }
            BooksViewModel vm = new BooksViewModel{
                Title = bookList.Title,
                PageCount = bookList.PageCount,
                PublishDate = bookList.PublishDate.Date.ToString("dd/MM/yyyy"),
                Genre = ((GenreEnum)bookList.GenreId).ToString()
            };
            return vm;
        }        
    }
}