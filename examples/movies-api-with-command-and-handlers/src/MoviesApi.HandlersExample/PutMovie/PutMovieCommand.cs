namespace MoviesApi.HandlersExample.PutMovie;

public class PutMovieCommand: ICommand<PutMovieInput, Movie>
{
    private readonly AmazonDynamoDBClient _dynamoDbClient;
    public PutMovieCommand()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
    }

    public async Task<Movie> ExecuteAsync(PutMovieInput input)
    {
        var movie = input.Movie;
        var putItemRequest = new PutItemRequest
        {
            TableName = "Movies",
            Item = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = movie.Id.ToString() } },
                { "Title", new AttributeValue { S = movie.Title } },
                { "Description", new AttributeValue { S = movie.Description } },
                { "ReleaseDate", new AttributeValue { S = movie.ReleaseDate.ToString("yyyy-MM-dd") } },
                { "Genre", new AttributeValue { S = movie.Genre } },
                { "Rating", new AttributeValue { N = movie.Rating.ToString() } }
            }
        };
        await _dynamoDbClient.PutItemAsync(putItemRequest);
        return movie;
    }
}