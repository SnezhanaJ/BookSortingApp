namespace BookStore.Domain.Domain.Models
{
    public class Book : BaseEntity
    {
        public string? BookImage { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
