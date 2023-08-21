namespace MoviesApi.HandlersExample;
public class GetMovieByIdCommand: ICommand<GetMovieByIdInput, Movie>
{
    private readonly IAmazonDynamoDB _dynamoDbClient;
    public GetMovieByIdCommand(IAmazonDynamoDB dynamoDbClient)
    {
        _dynamoDbClient = dynamoDbClient;
    }

    public async Task<Movie> ExecuteAsync(GetMovieByIdInput input)
    {
        var response = await _dynamoDbClient.GetItemAsync(new GetItemRequest
        {
            TableName = "Movies",
            Key = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = input.MovieId.ToString() } }
            }
        });
        return Utils.MapToMovie(response.Item);
    }
}


