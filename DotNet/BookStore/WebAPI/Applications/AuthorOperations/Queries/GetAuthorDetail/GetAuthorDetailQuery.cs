using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _DbContext;
        private readonly IMapper _mapper;
        public int Id { get; set; }
        public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var authors = _DbContext.Authors.SingleOrDefault(x => x.Id == Id);
            if(authors is null)
                throw new NullReferenceException("Var olmayan bir yazar Id'si girdiniz!");
            return _mapper.Map<AuthorDetailViewModel>(authors);
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}