using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DBOperations;

namespace WebAPI.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        /*private static List<Book> BookList = new List<Book>
        {
            new Book{
                ID = 1,
                Title = "LEan Startup",
                GenreId = 1, // Personal Growth
                PageCount = 200,
                PublishDate = new System.DateTime(2001,06,12)
            },
            new Book{
                ID = 2,
                Title = "Herland",
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new System.DateTime(2010,05,12)
            },
            new Book{
                ID = 3,
                Title = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new System.DateTime(2010,05,12)
            }
            
        };*/

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.ID).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book getById(int id)
        {
            var book = _context.Books.Where(book => book.ID == id).SingleOrDefault();
            return book;
        }
/*
        [HttpGet]
        public Book get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.ID == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
*/
        [HttpPost]
        public IActionResult postBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x=> x.Title == newBook.Title);
            if(book is not null)
                return BadRequest();
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult updateBook(int id, [FromBody] Book updateBook)
        {
            var Book = _context.Books.Where(book => book.ID == id).SingleOrDefault();
            if(Book is not null){
                Book.Title = updateBook.Title != "string" ? updateBook.Title : Book.Title;
                Book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : Book.GenreId;
                Book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : Book.PageCount;
                Book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : Book.PublishDate;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public IActionResult deleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x=> x.ID == id);
            if(book is null)
                return BadRequest();
                
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }

    [ApiController]
    [Route("[controller]s")]
    public class DeleteBookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookController(BookStoreDbContext context)
        {
            _context = context;
        }
    }
}