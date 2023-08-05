
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace MoviesApi;
public class QueryFilmsApiHandler
{
    private readonly AmazonDynamoDBClient _dynamoDbClient;

    public QueryFilmsApiHandler()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
    }
    public async Task<APIGatewayProxyResponse> GetAll(APIGatewayProxyRequest request, ILambdaContext context)
    {                       
        var scanRequest = new ScanRequest
        {
            TableName = "Movies"
        };
        var scanResponse = await _dynamoDbClient.ScanAsync(scanRequest);
        var movies = scanResponse.Items.Select(MapToMovie);
        var response = Utils.CreateResponse(movies);
        return response;
    }

    public async Task<APIGatewayProxyResponse> GetById(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (!request.PathParameters.ContainsKey("id") || !int.TryParse(request.PathParameters["id"], out var movieId))
            return new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Body = "not valid id" + request.PathParameters["id"]};
        
        var response = await _dynamoDbClient.QueryAsync(new QueryRequest(Consts.MoviesTableName)
        {
            TableName = Consts.MoviesTableName,
            KeyConditionExpression = "Id = :v_Id",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                [":v_Id"] = new() { S = movieId.ToString() }
            }
        });
        return response.Items.Any() 
            ? Utils.CreateResponse(MapToMovie(response.Items.Single())) 
            : new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.NotFound };
    }

    private static Movie MapToMovie(Dictionary<string, AttributeValue> item)
    {
        if (item == null || !item.Any())
            return null;
        return new Movie
        {
            Id = int.Parse(item["Id"].S),
            Title = item["Title"].S,
            Description = item["Description"].S,
            ReleaseDate = DateTime.Parse(item["ReleaseDate"].S),
            Genre = item["Genre"].S,
            Rating = double.Parse(item["Rating"].N)
        };
    }
}                 