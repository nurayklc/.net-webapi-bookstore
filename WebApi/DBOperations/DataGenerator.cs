using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations 
{
    public class DataGenerator 
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>))
        }
    }
}