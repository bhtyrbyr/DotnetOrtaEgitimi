using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Applications.CreateOperationsTools;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.MovieOperations.Commands.CreateMovieCommand
{
    public class CreateMovieCommand
    {
        public CreateMovieModel model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Name == model.Name);
            
            if( movie is not null)
                throw new StackOverflowException("Bu film daha önce eklenmiştir!");
            CreateOperationTools tool = new CreateOperationTools(_context, _mapper);
            tool.ControlActorAndGenreFormant(model);
            tool.ControlDirectorInDatabase(model);
            tool.CreateNewMovieInDatabase(model);
            tool.BindMovieAndGenreTogetherInDatabase(model);   
            tool.BindMovieAndActorTogetherInDatabase(model);        
        }
    }
    public class CreateMovieModel
    {
        public string Name { get; set; }
        public MovieDirectorViewModel Director { get; set; }
        public int Price { get; set; }
        public List<MovieGenreViewModel> Genres { get; set; }
        public List<MovieActorViewModel> Actors { get; set; }
    }
}