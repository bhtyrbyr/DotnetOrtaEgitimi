using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.ActorOperations.Command.CreateActorCommand;
using WebApi.Applications.ActorOperations.Command.DeleteActorCommand;
using WebApi.Applications.ActorOperations.Command.UpdateActorCommand;
using WebApi.Applications.ActorOperations.Queries.GetActorDetailQuery;
using WebApi.Applications.ActorOperations.Queries.GetActorsQuery;
using WebApi.Applications.MovieOperations.Commands.UpdateBookCommand;
using WebApi.DbOperations;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class ActorController : ControllerBase
    {
        
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ActorController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorsQuery queries = new GetActorsQuery(_context, _mapper);
            var list = queries.Handle();
            return Ok(list);
        }
   
        [HttpGet("id")]
        public IActionResult GetActorById(int id)
        {
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            query.id = id;
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
           
        [HttpPost]
        public IActionResult CreateActorById(ActorCreateModel model)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            command.model = model;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
           
        [HttpDelete("id")]
        public IActionResult DeleteActorById(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context, _mapper);
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            command.id = id;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
           
        [HttpPut("id")]
        public IActionResult UpdateActorById(int id, UpdateActorModel model)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            command.id = id;
            command.model = model;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}