namespace ApiClient.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public float Price { get; set; }
        public int Bookstand { get; set; }
        public int Shelf { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}