using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;

namespace Application.AuthorOperations.Commands
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int authorid)
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null!);
            command.AuthorId = authorid;
            
            //act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Theory]
        [InlineData(200)]
        [InlineData(2)]
        public void WhenInvalidBookIdisGiven_Validator_ShouldNotBeReturnError(int authorid)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null!);
            command.AuthorId = authorid;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
            
        }
    }
}