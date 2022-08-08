using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.MovieOperations.Commands.CreateMovieCommand;
using WebApi.Applications.MovieOperations.Commands.DeleteMovieCommand;
using WebApi.Applications.MovieOperations.Commands.UpdateBookCommand;
using WebApi.Applications.MovieOperations.Queries.GetMovieDetailQuery;
using WebApi.Applications.MovieOperations.Queries.GetMoviesQuery;
using WebApi.DbOperations;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery queries = new GetMoviesQuery(_context, _mapper);
            var list = queries.Handle();
            return Ok(list);
        }
        
        [HttpGet("id")]
        public IActionResult GetMovieById(int id)
        {
            GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            query.Id = id;
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateNewMovie([FromBody] CreateMovieModel model)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            command.model = model;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context, _mapper);
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            command.id = id;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieViewModel model)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            command.id = id;
            command.model = model;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}