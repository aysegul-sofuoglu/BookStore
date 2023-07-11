using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var authorBooks = _dbContext.Books.SingleOrDefault(x => x.AuthorId == AuthorId);

            if (author is null)
			    throw new InvalidOperationException("Author not found.");

            if (authorBooks is not null)
			throw new InvalidOperationException(author.Name + " " +  author.Surname + " has a published book. Please delete book first.");

            _dbContext.Authors.Remove(author);
		    _dbContext.SaveChanges();
        }
    }
}