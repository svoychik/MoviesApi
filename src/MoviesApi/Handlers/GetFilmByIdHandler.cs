using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
namespace MoviesApi;

public class GetFilmByIdHandler
{
    public APIGatewayProxyResponse Execute(APIGatewayProxyRequest request, ILambdaContext context)
    {
        // if (!request.PathParameters.ContainsKey("id") || !int.TryParse("id", out var movieId))
        //     return new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.BadRequest };
        var movieId = 1;
        var response = Utils.CreateResponse(MoviesApiService.GetMovies(x => x.Id == movieId));
        return response;
    }
}
