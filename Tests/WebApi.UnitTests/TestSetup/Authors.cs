using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author{Name = "Eric", Surname = "Ries", Birthday = new DateTime(1978, 09, 22)},
                    new Author{Name = "Charlotte", Surname = "Perkins Gilman", Birthday = new DateTime(1860, 07, 03)},
                    new Author{Name = "Frank", Surname = "Herbert", Birthday = new DateTime(1920, 10, 08)}
                );
              
        }
    }
}