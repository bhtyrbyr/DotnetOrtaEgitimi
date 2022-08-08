using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.MovieOperations.Commands.DeleteMovieCommand
{
    public class DeleteMovieCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int id { get; set; }
        public DeleteMovieCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if(movie is null)
                throw new IndexOutOfRangeException("Silinmek istenen film bulunamadı!");
            var movieActor = _context.MovieActors.Where(x => x.MovieId == id).ToList();
            _context.MovieActors.RemoveRange(movieActor);
            var movieGenre = _context.MovieGenres.Where(x => x.MovieId == id).ToList();
            _context.MovieGenres.RemoveRange(movieGenre);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}