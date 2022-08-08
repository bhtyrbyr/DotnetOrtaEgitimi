using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Applications.GenreOperations.Commands.UpdateGenres
{
    public class UpdateGenreCommand
    {
        public int GenreID { get; set; }
        public UpdateGenreModel model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
       private readonly IMapper _mapper;
        public UpdateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=> x.Id == GenreID);
            if(genre is null)
                throw new InvalidOperationException("Varolmayan bir kategori ID'si girdiniz!");

            if(_dbContext.Genres.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != GenreID))
                throw new InvalidOperationException("Bu kategori zaten listede var!");

            genre.Name = model.Name.Trim() == default ? genre.Name : model.Name;
            genre.IsActive = model.IsActive;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;      
    }
}