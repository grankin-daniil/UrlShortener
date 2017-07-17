using System.ComponentModel.DataAnnotations;

namespace UrlShortener.WebApi.Contracts.Models
{
    public class UrlShorteningModel
    {
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
