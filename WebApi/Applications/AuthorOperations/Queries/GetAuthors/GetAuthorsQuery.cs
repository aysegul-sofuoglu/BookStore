using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
         private readonly IMapper _mapper;

        public GetAuthorsQuery(IMapper mapper, IBookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _dbContext.Authors.OrderBy(x => x.Id).ToList();
            List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(authorList);

            return viewModel; 
        }
    }

    public class AuthorsViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Birthday { get; set; }
	}
}