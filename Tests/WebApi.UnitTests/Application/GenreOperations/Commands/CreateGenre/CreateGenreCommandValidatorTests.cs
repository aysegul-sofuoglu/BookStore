using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using static WebApi.Applications.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("a")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null!);
            command.Model = new CreateGenreModel(){Name = name};
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Theory]
        [InlineData("abcd ")]
        [InlineData("abcd")]
        [InlineData("ab123")]
        [InlineData("12abc")]
        [InlineData("    a")]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null!);
            command.Model = new CreateGenreModel(){Name = name};
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
           
        }
       
    }
}