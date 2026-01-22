using MediatR;

namespace MediatrDemo1.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var message1 = $"Started handling {request?.GetType().Name.ToString()}";

            Console.WriteLine(message1);

            var result = await next();

            var message2 = $"Finished handling {request?.GetType().Name.ToString()} with {result?.GetType().Name.ToString()}";

            Console.WriteLine(message2);

            return result;
        }
    }
}
