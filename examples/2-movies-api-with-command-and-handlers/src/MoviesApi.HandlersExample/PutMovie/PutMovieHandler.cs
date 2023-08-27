using MoviesApi.HandlersExampleß;
using Newtonsoft.Json;
namespace MoviesApi.HandlersExample;

public class PutMovieHandler: BaseHttpHandler<PutMovieInput, Movie>
{
    private readonly MovieValidator _validator = new();
    public PutMovieHandler() : base(ConfigureServices)
    {
    }

    public static IServiceProvider ConfigureServices() =>
        new ServiceCollection()
            .AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>()
            .AddSingleton<ICommand<PutMovieInput, Movie>, PutMovieCommand>()
            .BuildServiceProvider();

    public override async Task<APIGatewayProxyResponse> ExecuteAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var transformedInput = TransformRequest(request, context);
        var validationResult = await _validator.ValidateAsync(transformedInput.Movie);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors);
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = errorMessage
            };
        }
        
        var commandOutput = await _command.ExecuteAsync(transformedInput);
        return TransformOutput(commandOutput);
    }

    public override PutMovieInput TransformRequest(APIGatewayProxyRequest request, ILambdaContext context)
    { 
        var movie = JsonConvert.DeserializeObject<Movie>(request.Body)!;
        return new PutMovieInput(movie);
    }

    public override APIGatewayProxyResponse TransformOutput(Movie output)
    {
        var response = Utils.CreateResponse(output);
        response.Headers.Add("location", $"/movies/{output.Id}");
        return response;
    }
}