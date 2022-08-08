using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebAPI.Applications.BookOperations.Commands.CreateBooks;
using WebAPI.DBOperations;
using WebAPI.Entitys;
using Xunit;

namespace Application.BookOperations.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public CreateBookCommandTests(CommonTestFixture testFixure)
        {
            _context = testFixure.Context;
            _mapper = testFixure.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arange - Hazırlık
            var book = new Book(){Title = "Kitap 4", PageCount = 100, GenreId = 1, AuthorId = 2, PublishDate = new System.DateTime(2000,01,01)};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){Title = book.Title};

            // Act & Assert - Çalıştırma & Doğrulama
            FluentActions
                   .Invoking(() => command.Handle())
                   .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!");
            
            // Act    - Çalıştırma


            // Assert - Doğrulama
        }

        [Fact]
        public void WhenNotAlreadyExistBookTitleGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel Model = new CreateBookModel(){Title = "Kitap 5", PageCount = 1000, 
            PublishDate = DateTime.Now.AddYears(-10), AuthorId = 1, GenreId = 1};
            command.Model = Model;
            // Act    - Çalıştırma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert - Doğrulama
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(Model.PageCount);
            book.PublishDate.Should().Be(Model.PublishDate);
        }
    }
}