using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebAPI.DBOperations;
using WebAPI.TokenOperations;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Applications.UserOperations.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommand
    {
        public readonly IBookStoreDbContext _DbContext;
        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public RefreshToken RefreshToken { get; set; }

        public RefreshTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _DbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _DbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken.Token);
            if(user is null)
                throw new UnauthorizedAccessException("Geçersiz anahtar. Lütfen giriş yapınız!");
            if(user is not null && user.RefreshTokenExpirationDate <= DateTime.Now)
            {
                string message = "Anahtarın geçerliliği " + user.RefreshTokenExpirationDate.ToString("dd.MM.yyyy - HH:mm:ss") + " tarihinde sona erdi. Tekrar giriş yapın!";
                throw new UnauthorizedAccessException(message);
            }
            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpirationDate = token.Expiration.AddMinutes(5);  
            _DbContext.SaveChanges();
            return token;
            
        }
    }
    public class RefreshToken
    {
        public string Token { get; set; }
    }
}