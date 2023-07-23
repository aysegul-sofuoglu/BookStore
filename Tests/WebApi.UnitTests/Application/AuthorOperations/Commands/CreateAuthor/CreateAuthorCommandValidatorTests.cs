using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;

namespace Application.AuthorOperations.Commands
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(" ", " ")]
        [InlineData(" ", "abc" )]
        [InlineData("abc", " " )]
        [InlineData("ab", "a" )]
        [InlineData("a", "ab" )]
        [InlineData("a", "b")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel(){Name = firstname, Surname = lastname, Birthday= new System.DateTime(1900,01,25)};
            
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null!,null!);
            command.Model = new CreateAuthorViewModel()
            {
                Name = "Frank",
                Surname = "Tolkien",
                Birthday = DateTime.Now.Date
                
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
            
        }

        [Theory]
        [InlineData("abcd ", " abcd")]
        [InlineData("abcd", "abcd" )]
        [InlineData("ab  ", "ab  " )]
        [InlineData(" ab ", " a  " )]
        [InlineData("asdfghgfd", "asdfghgfdssdf" )]
        [InlineData(" aaa", "bbb " )]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel(){Name = firstname, Surname = lastname, Birthday= new System.DateTime(1900,01,25)};
            
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
           
        } 
    }
}
