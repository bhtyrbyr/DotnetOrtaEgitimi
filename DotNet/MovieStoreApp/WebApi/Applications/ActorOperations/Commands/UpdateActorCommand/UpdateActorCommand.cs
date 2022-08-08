using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.ActorOperations.Command.UpdateActorCommand
{
    public class UpdateActorCommand
    {
        public int id { get; set; }
        public UpdateActorModel model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => (x.Name.Trim().ToLower() == model.Name.Trim().ToLower())||
                                                x.Surname.Trim().ToLower() == model.Surname.Trim().ToLower());
            if( actor is null)
                throw new InvalidOperationException("Bu sanatçı daha önce kayıt edilmemiş.");
            var movies = _context.Movies.OrderBy(x => x.Id).ToList();
            foreach (var item in model.Movies)
            {
                if(movies.Any(x => x.Name.Trim().ToLower() == item.Trim().ToLower()))
                    continue;
                else{
                    throw new InvalidOperationException("Sistemde kayıtlı olmayan film girildi. İlk olarak filmi giriniz! (Film : "+item+" )");
                } 
            }
            var ActorMovies = _context.MovieActors.Where(x => x.ActorId == actor.Id).ToList();
            _context.MovieActors.RemoveRange(ActorMovies);
            _context.SaveChanges();
            foreach (var item in model.Movies)
            {
                _context.MovieActors.Add(
                new MovieActor{
                    ActorId = actor.Id,
                    MovieId = _context.Movies.SingleOrDefault(x => x.Name.Trim().ToLower() == item.Trim().ToLower()).Id
                }
            );
            }
            _context.SaveChanges();
        }
    }
    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}