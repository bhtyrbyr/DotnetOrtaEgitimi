using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.Applications.GenreOperations.Queries.GetGenres;
using WebAPI.DBOperations;

namespace WebAPI.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public int GenreId { get; set; }

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if(genres is null)
                throw new ArgumentException("Var olmayan bir kategori ID'si girdiniz!");
            return _mapper.Map<GenreDetailViewModel>(genres);
        }
    }    
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}