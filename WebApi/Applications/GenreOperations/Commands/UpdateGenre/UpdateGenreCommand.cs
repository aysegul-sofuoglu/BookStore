using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre{
    public class UpdateGenreCommand{
        private readonly IBookStoreDbContext _dbContext;
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        public UpdateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
             var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

            if(genre is null){
                throw new InvalidOperationException("Genre not found");
            }

            if(_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("A genre with the same name already exists.");

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;

            genre.IsActive = Model.IsActive;

            _dbContext.SaveChanges();
        }

        public class UpdateGenreModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
       
        }
    }
}