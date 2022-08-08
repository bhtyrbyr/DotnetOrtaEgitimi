using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommand
    {
        public readonly IBookStoreDbContext _DbContext;
        public readonly IMapper _mapper;
        public CreateAuthorModel Model { get; set; }

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _DbContext.Authors.SingleOrDefault(x => new string(x.Name.ToLower().Trim() + " " + x.Surname.ToLower().Trim())
            == new string(Model.Name.ToLower().Trim() + " " +Model.Surname.ToLower().Trim()));
            if(author is not null)
                throw new OverflowException("Bu yazar daha önceden eklenmiştir!");
            _DbContext.Authors.Add(_mapper.Map<Author>(Model));
            _DbContext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}