using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;


namespace WebApi.Application.BookOperations.Queries.GetBookDetail{

    public class GetBookDetailQuery{

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId{ get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre ).Include(a => a.Author).Where(book => book.Id == BookId).SingleOrDefault();
            if(book is null){
                throw new InvalidOperationException("Book not found.");
            }
            
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); 
           
            return vm;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string AuthorName { get; set; }
		    public string AuthorSurname { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}