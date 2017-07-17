using System;
using System.Diagnostics.Contracts;
using System.Text;
using UrlShortener.Data.Contracts;
using UrlShortener.WebApi.Contracts.Services;

namespace UrlShortener.Core.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IShortenedUrlRepository _shortenedUrlRepository;

        public UrlShortenerService(IShortenedUrlRepository shortenedUrlRepository)
        {
            Contract.Requires<ArgumentNullException>(shortenedUrlRepository != null);

            _shortenedUrlRepository = shortenedUrlRepository;
        }

        public string Shorten(string longUrl)
        {
            var shortenUrlId = _shortenedUrlRepository.GetIdOf(longUrl);
            if (shortenUrlId.HasValue)
                return MapIdToShortenedUrlTail(shortenUrlId.Value);
            shortenUrlId = _shortenedUrlRepository.Add(longUrl);
            return MapIdToShortenedUrlTail(shortenUrlId.Value);
        }

        public string GetLongUrl(string shortenedUrlTail)
        {
            int id;
            if (!TryMapShortenedUrlTailToId(shortenedUrlTail, out id))
                return null;
            return _shortenedUrlRepository.GetLongUrlBy(id);
        }

        private static string MapIdToShortenedUrlTail(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id.ToString());
            return Convert.ToBase64String(plainTextBytes);
        }

        private static bool TryMapShortenedUrlTailToId(string shortenedUrlTail, out int id)
        {
            try
            {
                id = MapShortenedUrlTailToId(shortenedUrlTail);
                return true;
            }
            catch (FormatException)
            {
                id = default(int);
                return false;
            }
        }

        private static int MapShortenedUrlTailToId(string shortenedUrlTail)
        {
            var base64EncodedBytes = Convert.FromBase64String(shortenedUrlTail.ToUpper());
            var stringId = Encoding.UTF8.GetString(base64EncodedBytes);
            return int.Parse(stringId);
        }
    }
}
