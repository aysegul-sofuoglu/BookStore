using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context,_mapper);
            command.BookId=0;
                        

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Book not found.");
        }

        [Fact]
        public void WhenGivenBookIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context,_mapper);
            command.BookId=1;
            

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var book=_context.Books.SingleOrDefault(book=>book.Id == command.BookId);
            book.Should().NotBeNull();
        }
    }
}