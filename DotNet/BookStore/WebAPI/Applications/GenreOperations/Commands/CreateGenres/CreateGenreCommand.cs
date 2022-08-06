using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace WebAPI.Applications.GenreOperations.Commands.CreateGenres
{

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
    
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
       private readonly IMapper _mapper;
        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=> x.Name == Model.Name);
            if(genre is not null)
                throw new InvalidOperationException("Kategori zaten mevcut!");
            _dbContext.Genres.Add(_mapper.Map<Genre>(Model));
            _dbContext.SaveChanges();
        }
    }
}