namespace BlockbusterAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public bool IsActive { get; set; }
        public string Genre { get; set; }
    }
}
