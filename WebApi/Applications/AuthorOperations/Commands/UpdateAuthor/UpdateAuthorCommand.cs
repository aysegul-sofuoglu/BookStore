using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorViewModel  Model { get; set; } 
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(IMapper mapper, BookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Handle()
	{
		var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
		
		if (author is null)
			throw new InvalidOperationException("Author not found.");
		
	
        
		_mapper.Map(Model, author);
		_dbContext.SaveChanges();
	}
    }

    public class UpdateAuthorViewModel 
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
	}
}