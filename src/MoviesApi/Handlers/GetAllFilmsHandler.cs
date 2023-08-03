using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace MoviesApi;
public class GetAllFilmsHandler
{
    public APIGatewayProxyResponse Execute(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var response = Utils.CreateResponse(MoviesApiService.GetAllMovies());
        return response;
    }
}                 