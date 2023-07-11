using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations

{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author{Name = "Eric", Surname = "Ries", Birthday = new DateTime(1978, 09, 22)},
                    new Author{Name = "Charlotte", Surname = "Perkins Gilman", Birthday = new DateTime(1860, 07, 03)},
                    new Author{Name = "Frank", Surname = "Herbert", Birthday = new DateTime(1920, 10, 08)}
                );

                context.Genres.AddRange(
                    new Genre{Name = "Personal Growth"},
                    new Genre{Name = "Science Fiction"},
                    new Genre{Name = "Romance"}

                );

                context.Books.AddRange(
                    new Book { Title = "Lean Startup", AuthorId = 2, GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
                    new Book { Title = "Herland", AuthorId = 2, GenreId = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23) },
                    new Book { Title = "Dune", AuthorId = 2, GenreId = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) }
                    

                );

                context.SaveChanges();
            }
        }
    }
}