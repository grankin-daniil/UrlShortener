namespace UrlShortener.WebApi.Contracts.Services
{
    public interface IUrlShortenerService
    {
        string Shorten(string longUrl);

        string GetLongUrl(string shortenedUrlTail);
    }
}
