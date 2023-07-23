
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }

        private readonly IBookStoreDbContext _dbContext;
       
        public CreateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
     
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=>x.Name==Model.Name);

            if(genre is not null){
                throw new InvalidOperationException("A genre with the same name already exists.");
            }
            
            genre = new Genre();
            genre.Name = Model.Name;
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        
            
        }

        public class CreateGenreModel
        {
            public string Name { get; set; }
            
        }


    }
}