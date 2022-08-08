
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Applications.AuthorOperations.Queries.GetAuthors;
using WebAPI.Applications.AuthorOperations.Commands.CreateAuthors;
using WebAPI.Applications.AuthorOperations.Commands.DeleteAuthors;
using WebAPI.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebAPI.DBOperations;
using WebAPI.Applications.AuthorOperations.Commands.UpdateAuthors;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult GetBooks()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            query.Id = id;
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
            
        }

        [HttpPost]
        public ActionResult addBooks([FromBody] CreateAuthorModel newBook)
        {
            CreateAuthorCommand query = new CreateAuthorCommand(_context, _mapper);
            query.Model = newBook;
            Applications.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommandValidator validator = 
            new Applications.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommandValidator();
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult updateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            UpdateAuthorsCommandValidator validator = new UpdateAuthorsCommandValidator();
            command.Id = id;
            command.model = updateAuthor;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteAuthor(int id)
        {
            DeleteAuthorCommand query = new DeleteAuthorCommand(_context, _mapper);
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            query.Id = id;
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }
    }
}