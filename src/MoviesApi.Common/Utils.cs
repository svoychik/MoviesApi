using System.IdentityModel.Tokens.Jwt;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace MoviesApi.Common;

public static class Utils
{
    public static Movie MapToMovie(Dictionary<string, AttributeValue> item)
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

    public static APIGatewayProxyResponse CreateResponse<T>(T obj)
    {
        var body = (obj != null) ? JsonConvert.SerializeObject(obj) : string.Empty;
        var response = new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = body,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }
            }
        };
        return response;
    }

    public static (bool Successful, Dictionary<string, string> Claims) ValidateJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = jsonToken as JwtSecurityToken;
        if (tokenS is null)
            return (false, new());
        var claims = tokenS!.Claims.ToDictionary(c => c.Type, c => c.Value);
        return (true, claims);

    }
}