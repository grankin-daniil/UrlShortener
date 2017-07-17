namespace UrlShortener.Data.EntityFramework.Entities
{
    public interface IEntityBase<T> where T : struct
    {
        T Id { get; set; }
    }
}
