using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0,"Lor","Asd")]
        [InlineData(0,"Lo ","ASDF ")]
        [InlineData(1,"Lord"," SD")]
        [InlineData(0,"Lor","ASDF")]
        [InlineData(-1,"Lord Of", " ")]
        [InlineData(1," "," ")]
        [InlineData(1,"","ASD")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int authorId, string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel(){ Name=name, Surname=surname};
            command.AuthorId=authorId;
            
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [InlineData(1,"Lord Of The Rings","ASDF")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int authorId, string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname                
            };
            command.AuthorId=authorId;

            UpdateAuthorCommandValidator validations=new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

      
    }
}