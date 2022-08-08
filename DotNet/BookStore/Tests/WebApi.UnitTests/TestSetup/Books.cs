using System;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
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
        }
    }
}