namespace MoviesApi.HandlersExample;

public class PutMovieInput: Event
{
    public PutMovieInput(Movie movie)
    {
        Movie = movie;
    }
    public Movie Movie { get; init; }
}