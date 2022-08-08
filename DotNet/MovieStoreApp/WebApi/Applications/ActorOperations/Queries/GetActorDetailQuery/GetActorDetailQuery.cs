using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.ActorOperations.Queries.GetActorsQuery;
using WebApi.DbOperations;

namespace WebApi.Applications.ActorOperations.Queries.GetActorDetailQuery
{
    public class GetActorDetailQuery
    {
        public int id { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ActorsViewModel Handle(){
            var actors = _context.Actors.Include(x => x.MovieActors).
                                        ThenInclude(t => t.Movie).
                                        SingleOrDefault(x => x.Id == id);
            ActorsViewModel modelList = _mapper.Map<ActorsViewModel>(actors);
            return modelList;
        }
    }
}