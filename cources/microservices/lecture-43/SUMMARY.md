## Lecture 43. CQRS Design Pattern With MedaitR Library

General info about MediatR:
- IRequest interface is used to define a request which can be either a command or a query. The return type of the request can be specified as a generic parameter.
- Handler inherits from IRequestHandler<TRequest, TResponse>, where TRequest is the type of the command or query, and TResponse is the return type.
- TO make the distinction of the commands and queries clearer, we can define two custom interfaces: "ICommand<TResult> : IRequest<TResult>" and "IQuery<TResult> : IRequest<TResult>"