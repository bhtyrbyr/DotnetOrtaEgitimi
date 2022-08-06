using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _DbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authors = _DbContext.Authors.OrderBy(x => x.Id).ToList();
            List<AuthorViewModel> returnObj = _mapper.Map<List<AuthorViewModel>>(authors);
            return returnObj;
        }
    }

    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}