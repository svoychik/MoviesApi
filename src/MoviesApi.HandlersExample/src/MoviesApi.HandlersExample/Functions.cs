using System.Net;
using System.Security.Cryptography.Xml;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MoviesApi.HandlersExample;


public class GetAllMoviesCommand<TInput, TOutput>: ICommand<TInput, TOutput> where TInput: Event
{
    public TOutput ExecuteAsync(TInput input)
    {
        throw new NotImplementedException();
    }
}

public record class Event(string CorrelationId);
public interface ICommand<in TInput, out TOutput>// why covariant and contrvariant
{
    TOutput ExecuteAsync(TInput input);
}

public class GetAllMoviesHandler: BaseHttpHandler<void, GetMovi>
{                                                      

    
}

public abstract class BaseHttpHandler<TInput, TOutput>
{
    public virtual Task<APIGatewayProxyResponse> ExecuteAsync(APIGatewayProxyRequest request,
        ILambdaContext context)
    {
        var input = TransformRequest<TInput>(request, context);
        var output = Function<TInput, TOutput>(input);
        return TransformOutput(output);
    }

    public abstract TOutput Function<TInput, TOutput>(TInput input); //should be delegated to command
    public abstract T TransformRequest<T>(APIGatewayProxyRequest request, ILambdaContext context);
    public abstract Task<APIGatewayProxyResponse> TransformOutput(TOutput output);
}