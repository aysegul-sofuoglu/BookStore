using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    
        
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            

            // act & asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
           var author = new Author() {Name="Frank", Surname="Rebart", Birthday=new DateTime(1950,05,02)};
           _context.Add(author);
           _context.SaveChanges();

           DeleteAuthorCommand  command = new DeleteAuthorCommand (_context);
           command.AuthorId = author.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            author = _context.Authors.SingleOrDefault(x=> x.Id == author.Id);
            author.Should().BeNull();
        }
    }
}