namespace MoviesApi.Common;

public class GetMoviesResponse
{
    public IList<Movie> Movies { get; init; } = new List<Movie>();
}