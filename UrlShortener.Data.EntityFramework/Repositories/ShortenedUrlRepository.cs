using System.Linq;
using UrlShortener.Data.Contracts;
using UrlShortener.Data.EntityFramework.Entities;

namespace UrlShortener.Data.EntityFramework.Repositories
{
    public class ShortenedUrlRepository : BaseRepository<ShortenedUrl, int>, IShortenedUrlRepository
    {
        public ShortenedUrlRepository(UrlShortenerDbContext context) : base(context)
        {
        }

        public int Add(string longUrl)
        {
            var entity = new ShortenedUrl() { LongUrl = longUrl.ToLower() };
            Add(entity);
            return entity.Id;
        }

        public int? GetIdOf(string longUrl)
        {
            return FindBy(x => x.LongUrl == longUrl.ToLower()).FirstOrDefault()?.Id;
        }

        public string GetLongUrlBy(int id)
        {
            return Get(id)?.LongUrl;
        }
    }
}
