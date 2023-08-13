using FluentValidation;
using Newtonsoft.Json;

namespace MoviesApi.SimpleExample;
public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(movie => movie.Title).NotEmpty().MaximumLength(100);
        RuleFor(movie => movie.Description).NotEmpty();
        RuleFor(movie => movie.ReleaseDate).NotEmpty();
        RuleFor(movie => movie.Genre).NotEmpty().MaximumLength(50);
        RuleFor(movie => movie.Rating).InclusiveBetween(0, 10);
    }
}
public class CreateMovieHandler
{
    private readonly IValidator<Movie> _movieValidator;
    private readonly AmazonDynamoDBClient _dynamoDbClient;

    public CreateMovieHandler()
    {
        _movieValidator = new MovieValidator();
        _dynamoDbClient = new AmazonDynamoDBClient();
    }

    public async Task<APIGatewayProxyResponse> ExecuteAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        try
        {
            var movie = JsonConvert.DeserializeObject<Movie>(request.Body)!;
            var validationResult = await _movieValidator.ValidateAsync(movie);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(", ", validationResult.Errors);
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = errorMessage
                };
            }
            var putItemRequest = new PutItemRequest
            {
                TableName = "Movies",
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = movie.Id.ToString() } },
                    { "Title", new AttributeValue { S = movie.Title } },
                    { "Description", new AttributeValue { S = movie.Description } },
                    { "ReleaseDate", new AttributeValue { S = movie.ReleaseDate.ToString("yyyy-MM-dd") } },
                    { "Genre", new AttributeValue { S = movie.Genre } },
                    { "Rating", new AttributeValue { N = movie.Rating.ToString() } }
                }
            };
            await _dynamoDbClient.PutItemAsync(putItemRequest);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(movie),
                Headers = new Dictionary<string, string>()
                {
                    ["location"] = $"/movies/{movie.Id}"
                }
            };
            return response;
        }
        catch (Exception ex)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = $"Error: {ex.Message}" //not secure 
            };
        }
    }
}
