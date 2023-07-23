
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel  Model { get; set; } 
        private readonly IBookStoreDbContext _dbContext;
     

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
           
            _dbContext = dbContext;
        }

        public void Handle()
	{
		var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
		
		if (author is null)
			throw new InvalidOperationException("Author not found.");
		
	
        
		
        author.Name = Model.Name != default ? Model.Name : author.Name;
        author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
        
		_dbContext.SaveChanges();
	}
    }

    public class UpdateAuthorModel 
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
	}
}