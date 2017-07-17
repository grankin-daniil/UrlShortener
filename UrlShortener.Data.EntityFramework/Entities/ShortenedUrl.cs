using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.EntityFramework.Entities
{
    public class ShortenedUrl : IEntityBase<int>
    {
        public int Id { get; set; }

        [Required]
        public string LongUrl { get; set; }
    }
}
