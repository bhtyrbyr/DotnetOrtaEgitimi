using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.CreateOperationsTools;
using WebApi.DbOperations;

namespace WebApi.Applications.ActorOperations.Queries.GetActorsQuery
{
    public class GetActorsQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorsQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ActorsViewModel> Handle()
        {
            var actors = _context.Actors.Include(x => x.MovieActors).
                                        ThenInclude(t => t.Movie).
                                        OrderBy(x => x.Id).ToList();
            List<ActorsViewModel> modelList = _mapper.Map<List<ActorsViewModel>>(actors);
            return modelList;
        }
    }

    public class ActorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IReadOnlyList<string> Movies { get; set; }
    }
}