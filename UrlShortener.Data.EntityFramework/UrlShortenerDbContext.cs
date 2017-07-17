using System.Data.Entity;
using UrlShortener.Data.EntityFramework.Entities;

namespace UrlShortener.Data.EntityFramework
{
    public class UrlShortenerDbContext : DbContext
    {
        public IDbSet<ShortenedUrl> ShortenedUrls { get; set; }
    }
}
