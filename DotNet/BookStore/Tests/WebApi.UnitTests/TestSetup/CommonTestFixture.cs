using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context{ get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var option = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            Context = new BookStoreDbContext(option);

            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}