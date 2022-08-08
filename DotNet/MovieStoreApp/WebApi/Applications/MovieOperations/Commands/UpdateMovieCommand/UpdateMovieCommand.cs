using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Applications.CreateOperationsTools;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.MovieOperations.Commands.UpdateBookCommand
{
    public class UpdateMovieCommand
    {
        public int id { get; set; }
        public UpdateMovieViewModel model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMovieCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
             var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            
            if( movie is null)
                throw new StackOverflowException("Böyle bir film bulunamadı!");   
            
            CreateOperationTools tool = new CreateOperationTools(_context, _mapper);
            tool.ControlActorAndGenreFormant(model);
            movie.Name = model.Name;
            movie.Price = movie.Price;
            var movieActor = _context.MovieActors.Where(x => x.MovieId == id).ToList();
            _context.MovieActors.RemoveRange(movieActor);
            var movieGenre = _context.MovieGenres.Where(x => x.MovieId == id).ToList();
            _context.MovieGenres.RemoveRange(movieGenre);
            _context.SaveChanges();
            tool.ControlDirectorInDatabase(model);
            tool.BindMovieAndGenreTogetherInDatabase(model);   
            tool.BindMovieAndActorTogetherInDatabase(model);   
            _context.SaveChanges();

        }
    }
    public class UpdateMovieViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public MovieDirectorViewModel Director { get; set; }
        public List<MovieGenreViewModel> Genres { get; set; }
        public List<MovieActorViewModel> Actors { get; set; }
    }
}