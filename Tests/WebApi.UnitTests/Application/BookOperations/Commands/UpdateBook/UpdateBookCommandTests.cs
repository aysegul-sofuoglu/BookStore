using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 0;

            // act & asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found");

        }

        [Fact]
        public void WhenGivenBookIdinDB_Book_ShouldBeUpdate()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            UpdateBookModel model = new UpdateBookModel(){Title="WhenGivenBookIdinDB_Book_ShouldBeUpdate", GenreId=2};            
            command.Model = model;
            command.BookId= 1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var book=_context.Books.SingleOrDefault(book=>book.Id == command.BookId);
            book.Should().NotBeNull();
            
        }

    }
}