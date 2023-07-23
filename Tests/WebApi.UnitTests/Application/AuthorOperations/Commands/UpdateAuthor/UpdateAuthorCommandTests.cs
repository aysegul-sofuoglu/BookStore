using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId  = 0;

            // act & asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found.");

        }

        [Fact]
        public void WhenGivenAuthorIdinDB_Author_ShouldBeUpdate()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            UpdateAuthorModel model = new UpdateAuthorModel(){Name="WhenGivenAuthorIdinDB_Author_ShouldBeUpdate", Surname="Rebart"};            
            command.Model = model;
            command.AuthorId= 1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author=_context.Authors.SingleOrDefault(author=>author.Id == command.AuthorId);
            author.Should().NotBeNull();
            
        }
    }

}