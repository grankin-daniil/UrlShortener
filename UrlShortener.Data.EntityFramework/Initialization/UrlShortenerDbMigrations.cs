using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace UrlShortener.Data.EntityFramework.Initialization
{
    public class UrlShortenerDbMigrationsConfiguration : DbMigrationsConfiguration<UrlShortenerDbContext>
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UrlShortenerDbContext>());
        }
    }
}
