using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            DeleteBookCommand command = new DeleteBookCommand(_context);
            

            // act & asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
           var book = new Book() {Title="Lord Of The Rings", PageCount=100, PublishDate=new System.DateTime(1990,05,22), GenreId=1, AuthorId=1};
           _context.Add(book);
           _context.SaveChanges();

           DeleteBookCommand command = new DeleteBookCommand(_context);
           command.BookId = book.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            book = _context.Books.SingleOrDefault(x=> x.Id == book.Id);
            book.Should().BeNull();
        }
    }
}