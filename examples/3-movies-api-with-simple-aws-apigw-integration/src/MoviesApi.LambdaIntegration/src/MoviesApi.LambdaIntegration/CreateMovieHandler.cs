using System.Net;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FluentValidation;
using MoviesApi.Common;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MoviesApi.LambdaIntegration;

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

    public async Task<Movie> ExecuteAsync(Movie movie, ILambdaContext context)
    {
        var validationResult = await _movieValidator.ValidateAsync(movie);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors);
            throw new Exception("[400] Not valid input", new Exception(errorMessage));
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
        return movie;
    }
}