using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace MoviesApi;

public static class Utils
{
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
}