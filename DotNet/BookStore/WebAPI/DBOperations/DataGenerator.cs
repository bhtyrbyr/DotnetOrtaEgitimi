using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Entitys;

namespace WebAPI.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author{
                        Name = "Yazar",
                        Surname = "1",
                        BirthDate = new DateTime(1977,01,01)
                    },
                    new Author{
                        Name = "Yazar",
                        Surname = "2",
                        BirthDate = new DateTime(1977,01,01)
                    },
                    new Author{
                        Name = "Yazar",
                        Surname = "3",
                        BirthDate = new DateTime(1977,01,01)
                    }
                );

                context.Genres.AddRange(
                    new Genre{
                        Name = "Kategori 1"                        
                    },
                    new Genre{
                        Name = "Kategori 2"
                    },
                    new Genre{
                        Name = "Kategori 3"
                    }
                );  

                context.Books.AddRange(
                    new Book{
                        //ID = 1,
                        Title = "Kitap 1",
                        GenreId = 1,
                        AuthorId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)
                    },
                    new Book{
                        //ID = 2,
                        Title = "Kitap 2",
                        GenreId = 2,
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2001,06,12)
                    },
                    new Book{
                        //ID = 3,
                        Title = "Kitap 3",
                        GenreId = 3,
                        AuthorId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(2001,06,12)
                    }
                );
                context.SaveChanges();
            }            
        }
    }
}