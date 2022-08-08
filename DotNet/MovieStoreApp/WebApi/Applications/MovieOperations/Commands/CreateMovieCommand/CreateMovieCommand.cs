using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            
            foreach (var item in model.Genres)
            {
                if(item.Name.Length < 3)
                    throw new InvalidOperationException("Tür ismi geçersiz! (Tür ismi minimum 3 karakter olmalıdır : " + item.Name);
            }
            foreach (var item in model.Actors)
            {
                if(item.Name.Length < 3)
                    throw new InvalidOperationException("Tür ismi geçersiz! (Tür ismi minimum 3 karakter olmalıdır : " + item.Name);
                if(item.Surname.Length < 3)
                    throw new InvalidOperationException("Tür soyismi geçersiz! (Tür soyismi minimum 3 karakter olmalıdır : " + item.Surname);
            }
            
            var director = _context.Directors.SingleOrDefault(x => x.Name == model.Director.Name && x.Surname == model.Director.Surname);

            if(director is null)
            {
                director = _mapper.Map<Director>(model.Director);
                _context.Directors.Add(director);
                _context.SaveChanges();
            }
            _context.Movies.Add(
                new Movie{
                    Name = model.Name,
                    DrirectorId = _context.Directors.SingleOrDefault(x => x.Name == model.Director.Name && x.Surname == model.Director.Surname).Id,
                    Price = model.Price
                }
            );
            _context.SaveChanges();
            foreach (var item in model.Genres)
            {
                var genre = _context.Genres.SingleOrDefault(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim());
                if(genre is not null)
                {  }
                else
                {
                    _context.Genres.Add(
                        new Genre{
                            Name = item.Name,
                        }
                    );
                    _context.SaveChanges();
                }
                
                genre = _context.Genres.SingleOrDefault(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim());
                _context.MovieGenres.Add(
                    new MovieGenre{
                        GenreId = genre.Id,
                        MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                    }
                );
                _context.SaveChanges();
            }
            
            foreach (var item in model.Actors)
            {
                var actor = _context.Actors.SingleOrDefault(x => (x.Name.ToLower().Trim() == item.Name.ToLower().Trim())&&
                                            (x.Surname.ToLower().Trim() == item.Surname.ToLower().Trim()));
                if(actor is not null)
                {  }
                else
                {
                    _context.Actors.Add(
                        new Actor{
                            Name = item.Name,
                            Surname = item.Surname
                        }
                    );
                    _context.SaveChanges();
                }
                actor = _context.Actors.SingleOrDefault(x => (x.Name.ToLower().Trim() == item.Name.ToLower().Trim())&&
                                            (x.Surname.ToLower().Trim() == item.Surname.ToLower().Trim()));
                _context.MovieActors.Add(
                    new MovieActor{
                        ActorId = actor.Id,
                        MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                    }
                );
                _context.SaveChanges();
            }
        }
    }
    public class CreateMovieModel
    {
        public string Name { get; set; }
        public CreateMovieDirectorModel Director { get; set; }
        public int Price { get; set; }
        public List<CreateMovieGenreModel> Genres { get; set; }
        public List<CreateMovieActorModel> Actors { get; set; }
    }
    public class CreateMovieDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class CreateMovieGenreModel
    {
        public string Name { get; set; }
    }
    public class CreateMovieActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}