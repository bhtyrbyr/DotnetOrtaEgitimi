using System;
using WebAPI.DBOperations;
using WebAPI.Entitys;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}