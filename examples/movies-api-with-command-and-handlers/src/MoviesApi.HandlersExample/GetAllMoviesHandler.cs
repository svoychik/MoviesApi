namespace MoviesApi.HandlersExample;

public class GetAllMoviesHandler: BaseHttpHandler<Event, GetMoviesResponse>
{
    public GetAllMoviesHandler() : base(ConfigureServices)
    {
    }

    public static IServiceProvider ConfigureServices() =>
        new ServiceCollection()
            //TODO: tell how to initialize Options<T>
            .AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>()
            .AddSingleton<ICommand<Event, GetMoviesResponse>, GetAllMoviesCommand>()
            .BuildServiceProvider();

    public override Event TransformRequest(APIGatewayProxyRequest request, ILambdaContext context)
    {
        return new Event();
    }

    public override APIGatewayProxyResponse TransformOutput(GetMoviesResponse output) => Utils.CreateResponse(output);
}