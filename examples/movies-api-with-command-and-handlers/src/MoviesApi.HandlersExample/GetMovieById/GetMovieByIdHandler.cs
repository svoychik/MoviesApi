using System.Net;

namespace MoviesApi.HandlersExample;
public class GetMovieByIdHandler: BaseHttpHandler<GetMovieByIdInput, Movie>
{
    public GetMovieByIdHandler() : base(ConfigureServices)
    {
    }          

    public static IServiceProvider ConfigureServices() =>
        new ServiceCollection()
            //TODO: tell how to initialize Options<T>
            .AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>()
            .AddSingleton<ICommand<GetMovieByIdInput, Movie>, GetMovieByIdCommand>()
            .BuildServiceProvider();

    public override async Task<APIGatewayProxyResponse> ExecuteAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (!request.PathParameters.ContainsKey("id") || !int.TryParse(request.PathParameters["id"], out _))
            return new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Body = "not valid id" + request.PathParameters["id"]};
        return await base.ExecuteAsync(request, context);
    }          

    public override GetMovieByIdInput TransformRequest(APIGatewayProxyRequest request, ILambdaContext context) =>
        new GetMovieByIdInput
        {
            MovieId = int.Parse(request.PathParameters["id"])
        };

    public override APIGatewayProxyResponse TransformOutput(Movie? output)
    {
        return output == null
            ? new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.NotFound } 
            : Utils.CreateResponse(output);
    }
}