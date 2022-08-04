using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BookOperations.CreateBooks;
using WebAPI.BookOperations.DeleteBooks;
using WebAPI.BookOperations.GetBooks;
using WebAPI.BookOperations.UpdateBooks;
using WebAPI.DBOperations;
using AutoMapper;

namespace WebAPI.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{title}")]
        public IActionResult getById(string title)
        {
            GetBookByIDQuery query = new GetBookByIDQuery(_context, _mapper);
            try
            {
                query.Title = title;
                var result = query.Handle();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public IActionResult addBooks([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand query = new CreateBookCommand(_context, _mapper);
            try
            {
                query.NewBook = newBook;
                query.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{title}")]
        public IActionResult updateBook(string title, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Title = title;
                command.Model = updateBook;
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{title}")]

        public IActionResult deleteBook(string title)
        {
            DeleteBookCommand query = new DeleteBookCommand(_context);
            try
            {
                query.title = title;
                query.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
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