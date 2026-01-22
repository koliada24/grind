using MediatR;

namespace MediatrDemo1.Behaviors
{
    public class OperationTimeLoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;

            var result = await next();

            var operationDuration = DateTime.Now - startTime;

            Console.WriteLine($"Operation completed in {operationDuration.TotalMilliseconds} miliseconds");

            return result;
        }
    }
}
