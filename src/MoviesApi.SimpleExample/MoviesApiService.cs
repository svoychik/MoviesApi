namespace MoviesApi;

public static class MoviesApiService
{
    private static List<Movie> _movies = new List<Movie>()
    {
        new Movie
        {
            Id = 1,
            Title = "The Shawshank Redemption",
            Description = "Two imprisoned men bond over several years, finding solace and eventual redemption through acts of common decency.",
            ReleaseDate = new DateTime(1994, 9, 23),
            Genre = "Drama",
            Rating = 9.3
        },
        new Movie
        {
            Id = 2,
            Title = "The Godfather",
            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
            ReleaseDate = new DateTime(1972, 3, 24),
            Genre = "Crime, Drama",
            Rating = 9.2
        },
        new Movie
        {
            Id = 3,
            Title = "The Dark Knight",
            Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
            ReleaseDate = new DateTime(2008, 7, 18),
            Genre = "Action, Crime, Drama",
            Rating = 9.0
        },
        // Add more movies here...
        new Movie
        {
            Id = 10,
            Title = "Inception",
            Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
            ReleaseDate = new DateTime(2010, 7, 16),
            Genre = "Action, Adventure, Sci-Fi",
            Rating = 8.8
        }
    };

    public static IEnumerable<Movie> GetAllMovies() => _movies;
    public static IEnumerable<Movie> GetMovies(Func<Movie, bool> func) => _movies.Where(func);
}