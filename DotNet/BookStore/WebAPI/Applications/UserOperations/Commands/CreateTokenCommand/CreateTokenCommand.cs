using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebAPI.DBOperations;
using WebAPI.TokenOperations;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Applications.UserOperations.Commands.CreateTokenCommand
{
    public class CreateTokenCommand
    {
        public readonly IBookStoreDbContext _DbContext;
        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public CreateTokenModel Model { get; set; }

        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _DbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var findMail = _DbContext.Users.Any(x => x.Email == Model.Email);
            if(findMail){
                var user = _DbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
                if(user is null)
                    throw new FieldAccessException("Mail adresi veya şifre yanlış!");
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpirationDate = token.Expiration.AddMinutes(5);  
                _DbContext.SaveChanges();
                return token;              
            }else{
                throw new InvalidOperationException("Bu mail adresine kayıtlı hesap bulunamadı!");
            }
        }
    }
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}