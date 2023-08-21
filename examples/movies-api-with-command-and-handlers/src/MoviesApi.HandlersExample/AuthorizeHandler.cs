namespace MoviesApi.HandlersExample;

public class AuthorizeHandler
{
    private const string Issuer = "sevo"; // Replace with your issuer

    public APIGatewayCustomAuthorizerResponse Execute(APIGatewayCustomAuthorizerRequest input, ILambdaContext context)
    {
        context.Logger.LogInformation("MethodArn is: " + input.MethodArn);
        // const $"arn:aws:execute-api:{regionId}:{accountId}:{apiId}/{stage}/{httpVerb}/[{resource}/[{child-resources}]]"

        // extracts token from the string in format 'bearer xxxxx'
        string ExtractBearerToken(string authorizationHeader) =>
            authorizationHeader.Split(' ').LastOrDefault();

        try
        {
            var token = ExtractBearerToken(input.AuthorizationToken);
            context.Logger.Log("The token value is: " + token);
            var validationResult = Utils.ValidateJwtToken(token);
            if (!validationResult.Successful)
                return GenerateDenyPolicy("Invalid token!");

            var claims = validationResult.Claims;
            // Check if the token issuer matches your expected issuer
            if (!claims.TryGetValue("iss", out var issuer) || issuer != Issuer)
                return GenerateDenyPolicy("Invalid token issuer");

            var principalId = claims["sub"];
            var policy = GenerateAllowPolicy(input, principalId);

            return policy;
        }
        catch (Exception ex)
        {
            context.Logger.LogError(ex.Message);
            return GenerateDenyPolicy($"Unauthorized: {ex.Message}");
        }
    }

    private static APIGatewayCustomAuthorizerResponse GenerateAllowPolicy(APIGatewayCustomAuthorizerRequest input,
        string principalId)
    {
        
        var policy = new APIGatewayCustomAuthorizerResponse
        {
            PrincipalID = principalId,
            // Properties added here will be cached and available on requests to endpoints of API Gateway  
            Context = new APIGatewayCustomAuthorizerContextOutput()
            {
                ["access_level"] = "movies-api",
                ["authorized_at"] = DateTime.UtcNow.ToString("G")
            },
            PolicyDocument = new APIGatewayCustomAuthorizerPolicy
            {
                Version = "2012-10-17",
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>
                {
                    new()
                    {
                        Action = new HashSet<string> { "execute-api:Invoke" },
                        Effect = "Allow",
                        Resource = new HashSet<string> { input.MethodArn } //Better to allow requests to all API of the AG
                    }
                }
            }
        };
        return policy;
    }

    private APIGatewayCustomAuthorizerResponse GenerateDenyPolicy(string message)
    {
        return new APIGatewayCustomAuthorizerResponse
        {
            PrincipalID = null,
            PolicyDocument = new APIGatewayCustomAuthorizerPolicy
            {
                Version = "2012-10-17",
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>
                {
                    new()
                    {
                        Action = new HashSet<string> { "execute-api:Invoke" },
                        Effect = "Deny",
                        Resource = new HashSet<string> { "*" }
                    }
                }
            },
            Context = new APIGatewayCustomAuthorizerContextOutput { { "error", message } }
        };
    }
}