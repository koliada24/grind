using MediatR;

namespace MediatrDemo1.Behaviors
{
    public class RequestWrapperBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine();

            var result = await next();

            Console.WriteLine();

            return result;
        }
    }
}
