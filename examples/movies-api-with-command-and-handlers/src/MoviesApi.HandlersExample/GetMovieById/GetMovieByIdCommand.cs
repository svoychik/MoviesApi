namespace MoviesApi.HandlersExample;
public class GetMovieByIdCommand: ICommand<Event, GetMoviesResponse>
{
    private readonly IAmazonDynamoDB _dynamoDbClient;
    public GetMovieByIdCommand(IAmazonDynamoDB dynamoDbClient)
    {
        _dynamoDbClient = dynamoDbClient;
    }

    public async Task<GetMoviesResponse> ExecuteAsync(Event input)
    {
        var scanRequest = new ScanRequest
        {
            TableName = "Movies"
        };
        var scanResponse = await _dynamoDbClient.ScanAsync(scanRequest);
        var movies = scanResponse.Items.Select(Utils.MapToMovie).ToList();
        return new GetMoviesResponse(){ Movies = movies };
    }
}