using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorCommand
    {
        public readonly BookStoreDbContext _DbContext;
        public readonly IMapper _mapper;
        
        public int Id { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _DbContext.Authors.SingleOrDefault(x => x.Id == Id);
            if(author is null)
                throw new OverflowException("Girdiğiniz Id'de bir yazar yoktur!");
            _DbContext.Authors.Remove(author);
            _DbContext.SaveChanges();
        }
    }
}