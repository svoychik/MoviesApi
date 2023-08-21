// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MoviesApi.HandlersExample;

public class GetAllMoviesCommand: ICommand<Event, GetMoviesResponse>
{
    private readonly IAmazonDynamoDB _dynamoDbClient;
    public GetAllMoviesCommand(IAmazonDynamoDB dynamoDbClient)
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
        return new GetMoviesResponse { Movies = movies };
    }
}