using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Applications.UserOperations.Commands.RefreshTokenCommand;
using WebAPI.Applications.UserOperations.Commands.CreateTokenCommand;
using WebAPI.Applications.UserOperations.Commands.CreateUserCommand;
using WebAPI.DBOperations;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            command.Model = newUser;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            command.Model = login;
            validator.ValidateAndThrow(command);
            var token = command.Handle();
            return token;
        }

        [HttpPost("connect/refreshToken")]
        public ActionResult<Token> TakeNewToken([FromBody] RefreshToken refreshToken)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _mapper, _configuration);
            command.RefreshToken = refreshToken;
            var token = command.Handle();
            return token;
        }
    }
}