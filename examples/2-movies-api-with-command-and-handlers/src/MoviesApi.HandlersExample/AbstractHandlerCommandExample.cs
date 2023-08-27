
public class Event
{
    public string ConversationId { get; init; } = string.Empty;
}

public interface ICommand<in TInput, TOutput> where TInput: Event
{
    Task<TOutput> ExecuteAsync(TInput input);
}

public abstract class BaseHttpHandler<TInput, TOutput> where TInput: Event
{
    protected readonly ICommand<TInput,TOutput> _command;

    protected BaseHttpHandler(Func<IServiceProvider> serviceProviderFunc)
    {
        var serviceProvider = serviceProviderFunc();
        _command = serviceProvider.GetRequiredService<ICommand<TInput, TOutput>>();
    }

    public virtual async Task<APIGatewayProxyResponse> ExecuteAsync(
        APIGatewayProxyRequest request, ILambdaContext context)
    {               
        /*
         * Add validation here of API request to respond with
         *      - 404, 400, 401, ...
         */
        TInput transformedInput = TransformRequest(request, context);
        TOutput commandOutput = await _command.ExecuteAsync(transformedInput);
        return TransformOutput(commandOutput);
    }

    /// <summary>
    /// Transforms an incoming APIGatewayProxyRequest into the desired input format.
    /// Implement this method to customize the transformation logic for incoming requests.
    /// </summary>
    /// <param name="request">The incoming APIGatewayProxyRequest to be transformed.</param>
    /// <param name="context">The ILambdaContext providing information about the Lambda execution environment.</param>
    /// <returns>The transformed input object of type TInput.</returns>
    public abstract TInput TransformRequest(APIGatewayProxyRequest request, ILambdaContext context);

    /// <summary>
    /// Transforms an output object of type TOutput into an APIGatewayProxyResponse.
    /// Implement this method to customize the transformation logic for outgoing responses.
    /// </summary>
    /// <param name="output">The output object of type TOutput to be transformed.</param>
    /// <returns>The transformed APIGatewayProxyResponse.</returns>
    public abstract APIGatewayProxyResponse TransformOutput(TOutput output);
}