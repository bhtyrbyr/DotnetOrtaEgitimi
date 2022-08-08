using System;
using System.Linq;
using AutoMapper;
using WebApi.Applications.MovieOperations.Commands.CreateMovieCommand;
using WebApi.Applications.MovieOperations.Commands.UpdateBookCommand;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.CreateOperationsTools
{
    public class CreateOperationTools
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateOperationTools(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void ControlActorAndGenreFormant(CreateMovieModel model)
        {
            if(model.Genres.Any(x => x.Name.Length < 3))
                throw new InvalidOperationException("Tür ismi geçersiz! (Tür ismi minimum 3 karakter olmalıdır)");
            if(model.Actors.Any(x => x.Name.Length < 3 || x.Surname.Length < 3))  
                throw new InvalidOperationException("Tür soyismi geçersiz! (Tür soyismi minimum 3 karakter olmalıdır=");
        }
        public void ControlActorAndGenreFormant(UpdateMovieViewModel model)
        {
            if(model.Genres.Any(x => x.Name.Length < 3))
                throw new InvalidOperationException("Tür ismi geçersiz! (Tür ismi minimum 3 karakter olmalıdır)");
            if(model.Actors.Any(x => x.Name.Length < 3 || x.Surname.Length < 3))  
                throw new InvalidOperationException("Tür soyismi geçersiz! (Tür soyismi minimum 3 karakter olmalıdır=");
        }

        public void ControlDirectorInDatabase(CreateMovieModel model)
        {
            
            if(!_context.Directors.Any(x => (x.Name.Trim().ToLower() == model.Director.Name.Trim().ToLower()) &&
                                            (x.Surname.Trim().ToLower() == model.Director.Surname.Trim().ToLower())))
            {
                var director = _mapper.Map<Director>(model.Director);
                _context.Directors.Add(director);
                _context.SaveChanges();
            }
        }

        public void ControlDirectorInDatabase(UpdateMovieViewModel model)
        {
            
            if(!_context.Directors.Any(x => (x.Name.Trim().ToLower() == model.Director.Name.Trim().ToLower()) &&
                                            (x.Surname.Trim().ToLower() == model.Director.Surname.Trim().ToLower())))
            {
                var director = _mapper.Map<Director>(model.Director);
                _context.Directors.Add(director);
                _context.SaveChanges();
            }
        }

        public void CreateNewMovieInDatabase(CreateMovieModel model)
        {
            _context.Movies.Add(
                new Movie{
                    Name = model.Name,
                    DrirectorId = _context.Directors.SingleOrDefault(x => x.Name == model.Director.Name && x.Surname == model.Director.Surname).Id,
                    Price = model.Price
                }
            );
            _context.SaveChanges();
        }

        public void BindMovieAndActorTogetherInDatabase(CreateMovieModel model)
        {
            
            foreach (var item in model.Actors)
            {
                var actor = _context.Actors.SingleOrDefault(x => (x.Name.ToLower().Trim() == item.Name.ToLower().Trim())&&
                                            (x.Surname.ToLower().Trim() == item.Surname.ToLower().Trim()));
                if(actor is not null)
                {
                    _context.MovieActors.Add(
                        new MovieActor{
                            ActorId = actor.Id,
                            MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                        }
                    );
                    _context.SaveChanges();
                }
                else
                {
                    actor = new Actor(){
                            Name = item.Name,
                            Surname = item.Surname
                    };
                    _context.Actors.Add(actor);
                    _context.SaveChanges();
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

        public void BindMovieAndActorTogetherInDatabase(UpdateMovieViewModel model)
        {
            
            foreach (var item in model.Actors)
            {
                var actor = _context.Actors.SingleOrDefault(x => (x.Name.ToLower().Trim() == item.Name.ToLower().Trim())&&
                                            (x.Surname.ToLower().Trim() == item.Surname.ToLower().Trim()));
                if(actor is not null)
                {
                    _context.MovieActors.Add(
                        new MovieActor{
                            ActorId = actor.Id,
                            MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                        }
                    );
                    _context.SaveChanges();
                }
                else
                {
                    actor = new Actor(){
                            Name = item.Name,
                            Surname = item.Surname
                    };
                    _context.Actors.Add(actor);
                    _context.SaveChanges();
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
        
        public void BindMovieAndGenreTogetherInDatabase(CreateMovieModel model)
        {
            
            foreach (var item in model.Genres)
            {
                var genre = _context.Genres.SingleOrDefault(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim());
                if(genre is not null)
                {  
                    _context.MovieGenres.Add(
                        new MovieGenre{
                            GenreId = genre.Id,
                            MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                        }
                    );
                    _context.SaveChanges();
                }
                else
                {
                    genre = new Genre(){
                        Name = item.Name,
                    };
                    _context.Genres.Add(genre);
                    _context.SaveChanges();
                    _context.MovieGenres.Add(
                        new MovieGenre{
                            GenreId = genre.Id,
                            MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                        }
                    );
                    _context.SaveChanges();
                }
            }
        }

        public void BindMovieAndGenreTogetherInDatabase(UpdateMovieViewModel model)
        {
            
            foreach (var item in model.Genres)
            {
                var genre = _context.Genres.SingleOrDefault(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim());
                if(genre is not null)
                {  
                    _context.MovieGenres.Add(
                        new MovieGenre{
                            GenreId = genre.Id,
                            MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                        }
                    );
                    _context.SaveChanges();
                }
                else
                {
                    genre = new Genre(){
                        Name = item.Name,
                    };
                    _context.Genres.Add(genre);
                    _context.SaveChanges();
                    _context.MovieGenres.Add(
                        new MovieGenre{
                            GenreId = genre.Id,
                            MovieId = _context.Movies.SingleOrDefault(x => x.Name == model.Name).Id
                        }
                    );
                    _context.SaveChanges();
                }
            }
        }

    }
    public class MovieDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class MovieGenreViewModel
    {
        public string Name { get; set; }
    }
    public class MovieActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}