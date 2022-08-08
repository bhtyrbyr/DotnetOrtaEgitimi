using FluentAssertions;
using WebAPI.Applications.BookOperations.Commands.CreateBooks;
using Xunit;

namespace Application.BookOperations.CreateBook
{   
    public class CreateBookCommandValidatorTests
    {
        [Theory]
        [InlineData("ada",150,1,1)]
        [InlineData("Lord of the ",0,0,0)]
        [InlineData("the ",0,1,0)]
        [InlineData("Lord of the ",150,0,1)]
        [InlineData(" ",0,0,0)]
        [InlineData("Lord of the ",150,0,1)]
        [InlineData("Lord of The Rings",150,1,1)]
        public void WhenInValidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int PageCount, int GenreId, int AuthorId)
        {
            // Arange - Hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = title, 
                PageCount = PageCount,
                PublishDate = System.DateTime.Now.AddYears(-1),
                GenreId = GenreId,
                AuthorId = AuthorId
            };

            // Act    - Çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);       

            // Assert - Doğrulama
            result.Errors.Count
            .Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShoulBeRetunError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = "title", 
                PageCount = 150,
                PublishDate = System.DateTime.Now,
                GenreId = 1,
                AuthorId = 1
            };

            // Act    - Çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);       

            // Assert - Doğrulama
            result.Errors.Count.Should()
                .BeGreaterThan(0);
        }
    }
}