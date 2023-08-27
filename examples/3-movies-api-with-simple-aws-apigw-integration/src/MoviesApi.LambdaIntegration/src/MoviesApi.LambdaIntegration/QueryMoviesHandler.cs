using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using MoviesApi.Common;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MoviesApi.LambdaIntegration;

public class QueryMoviesHandler
{
    private readonly AmazonDynamoDBClient _dynamoDbClient;

    public QueryMoviesHandler()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
    }

    public async Task<Movie> GetById(GetMovieByIdRequest request, ILambdaContext context)
    {
        var response = await _dynamoDbClient.QueryAsync(new QueryRequest("Movies")
        {
            KeyConditionExpression = "Id = :v_Id",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                [":v_Id"] = new() { S = request.MovieId.ToString() }
            }
        });
        return response.Items.Any() 
            ? Utils.MapToMovie(response.Items.Single()) 
            : throw new Exception("[404] Movie not found");
    }

    public async Task<List<Movie>> GetAll(Movie request, ILambdaContext context)
    {                       
        var scanRequest = new ScanRequest
        {
            TableName = "Movies"
        };
        var scanResponse = await _dynamoDbClient.ScanAsync(scanRequest);
        var movies = scanResponse.Items.Select(Utils.MapToMovie).ToList();
        return movies;
    }
}
public class GetMovieByIdRequest
{
    public int MovieId { get; set; }
}
