using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorCommand
    {
        public readonly IBookStoreDbContext _DbContext;
        public readonly IMapper _mapper;
        
        public int Id { get; set; }
        public UpdateAuthorModel model { get; set; }
        public UpdateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _DbContext.Authors.SingleOrDefault(x => x.Id == Id);
            if(author is null)
                throw new OverflowException("Bu yazar daha önceden eklenmiştir!");

            if(_DbContext.Authors.Any(x => 
                    new string(x.Name.ToLower().Trim() + " " + x.Surname.ToLower().Trim()) ==
                    new string(model.Name.ToLower().Trim() + " " + model.Surname.ToLower().Trim())
                     && x.Id != Id))
            {
                throw new OverflowException("Bu yazar farklı bir Id ile önceden eklenmiştir!");
            }
            
            author.Name = model.Name != default ? model.Name : author.Name;
            author.Surname = model.Surname != default ? model.Surname : author.Surname;
            author.BirthDate = model.BirthDate != default ? model.BirthDate : author.BirthDate;
            _DbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}