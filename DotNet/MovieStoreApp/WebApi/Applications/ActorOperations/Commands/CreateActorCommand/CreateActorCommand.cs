using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.ActorOperations.Command.CreateActorCommand
{
    public class CreateActorCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ActorCreateModel model { get; set; }
        public CreateActorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => (x.Name.Trim().ToLower() == model.Name.Trim().ToLower())||
                                                x.Surname.Trim().ToLower() == model.Surname.Trim().ToLower());
            if( actor is not null)
                throw new InvalidOperationException("Bu sanatçı daha önce kayıt altına alınmış. Id: " + actor.Id);
            actor = _mapper.Map<Actor>(model);
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
    }

    public class ActorCreateModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}