namespace UrlShortener.Data.Contracts
{
    public interface IShortenedUrlRepository
    {
        string GetLongUrlBy(int id);

        int? GetIdOf(string longUrl);

        int Add(string longUrl);
    }
}
