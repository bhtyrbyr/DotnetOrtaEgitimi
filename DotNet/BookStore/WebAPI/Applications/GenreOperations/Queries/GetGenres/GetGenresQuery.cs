using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenreViewModel> returnObj = _mapper.Map<List<GenreViewModel>>(genres);
            return returnObj;
        }
    }    
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}