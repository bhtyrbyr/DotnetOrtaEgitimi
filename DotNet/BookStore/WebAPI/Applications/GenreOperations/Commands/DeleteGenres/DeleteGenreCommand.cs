using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Applications.GenreOperations.Commands.DeleteGenres
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public DeleteGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Varolmayan bir kategori ID'si girdiniz!");
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}