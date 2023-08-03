
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace MoviesApi;
public class QueryFilmsApiHandler
{
    public APIGatewayProxyResponse GetAll(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var response = Utils.CreateResponse(MoviesApiService.GetAllMovies());
        return response;
    }

    public APIGatewayProxyResponse GetById(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (!request.PathParameters.ContainsKey("id") || !int.TryParse(request.PathParameters["id"], out var movieId))
            return new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Body = "not valid id" + request.PathParameters["id"]};
        var response = Utils.CreateResponse(MoviesApiService.GetMovies(x => x.Id == movieId));
        return response;
    }
}                 