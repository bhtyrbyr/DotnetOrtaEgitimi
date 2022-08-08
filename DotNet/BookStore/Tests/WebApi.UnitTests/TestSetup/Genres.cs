using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre{ Name = "Kategori 1" },
                new Genre{ Name = "Kategori 2" },
                new Genre{ Name = "Kategori 3" }
            );  
        }
    }
}