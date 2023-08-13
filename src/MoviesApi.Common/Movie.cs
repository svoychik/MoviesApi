namespace MoviesApi.Common;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; }
    public double Rating { get; set; }
}