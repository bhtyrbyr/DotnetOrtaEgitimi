using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.ActorOperations.Command.DeleteActorCommand
{
    public class DeleteActorCommand
    {
        public int id { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteActorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.Id == id);
            if ( actor is null)
                throw new NullReferenceException("Girilen kimlikte bir oyuncu bulunamadı!");
            var movieActor = _context.MovieActors.Where(x => x.ActorId == id).ToList();
            _context.MovieActors.RemoveRange(movieActor);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            
        }
    }
}