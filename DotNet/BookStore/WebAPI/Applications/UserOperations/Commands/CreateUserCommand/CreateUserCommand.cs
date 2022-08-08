using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.UserOperations.Commands.CreateUserCommand
{
    public class CreateUserCommand
    {
        public readonly IBookStoreDbContext _DbContext;
        public readonly IMapper _mapper;
        public CreateUserModel Model { get; set; }

        public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _DbContext.Users.SingleOrDefault(x => x.Email == Model.Email);
            if(user is not null)
                throw new OverflowException("Bu mail adresi başka bir hesap tarafından kullanılıyor!");
            _DbContext.Users.Add(_mapper.Map<User>(Model));
            _DbContext.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}