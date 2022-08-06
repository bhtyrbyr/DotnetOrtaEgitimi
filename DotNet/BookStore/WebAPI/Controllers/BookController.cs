
using Microsoft.AspNetCore.Mvc;
using WebAPI.DBOperations;
using AutoMapper;
using FluentValidation;
using WebAPI.Applications.BookOperations.Queries.GetBooks;
using WebAPI.Applications.BookOperations.Commands.CreateBooks;
using WebAPI.Applications.BookOperations.Commands.UpdateBooks;
using WebAPI.Applications.BookOperations.Commands.DeleteBooks;

namespace WebAPI.Controller
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
        
        [HttpGet]
        public ActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{title}")]
        public ActionResult getById(string title)
        {
            GetBookByIDQuery query = new GetBookByIDQuery(_context, _mapper);
            GetBookByIDQueryValidator validator = new GetBookByIDQueryValidator();
            query.Title = title;
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
            
        }

        [HttpPost]
        public ActionResult addBooks([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand query = new CreateBookCommand(_context, _mapper);
            query.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }

        [HttpPut("{title}")]
        public ActionResult updateBook(string title, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            command.Title = title;
            command.Model = updateBook;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{title}")]
        public ActionResult deleteBook(string title)
        {
            DeleteBookCommand query = new DeleteBookCommand(_context);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            query.title = title;
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }
    }
}

/*

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
            GetBookByIDQuery query = new GetBookByIDQuery(_context, _mapper);
            try
            {
                query.Title = title;
                GetBookByIDQueryValidator validator = new GetBookByIDQueryValidator();
                validator.ValidateAndThrow(query);
                var result = query.Handle();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }


        [HttpGet]
        public Book get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.ID == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }


            CreateBookCommand query = new CreateBookCommand(_context, _mapper);
            //string message = "";
            try
            {
                query.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(query);
                query.Handle();
                ValidationResult result = validator.Validate(query);
                if(!result.IsValid)
                {
                    foreach (var item in result.Errors)
                        message  += "Özellik" + item.PropertyName + " - Error Message " + item.ErrorCode + " " + item.ErrorMessage + "\n";
                    throw new ArgumentException(message);
                }
                else
                    query.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Title = title;
                command.Model = updateBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

            DeleteBookCommand query = new DeleteBookCommand(_context);
            try
            {
                query.title = title;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(query);
                query.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
*/