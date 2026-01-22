using MediatR;
using MediatrDemo1.Data;

namespace MediatrDemo1.Handlers
{
    public record GetToDoByIdRequest(Guid Id);

    public record GetToDoByIdQuery(Guid Id) : IRequest<GetToDoByIdResult>;

    public record GetToDoByIdResult(ToDoTask Task);

    public record GetToDoByIdResponse(ToDoTask Task);

    public class GetToDoByIdQueryHandler(IToDoRepository repository) : IRequestHandler<GetToDoByIdQuery, GetToDoByIdResult>
    {
        public async Task<GetToDoByIdResult> Handle(GetToDoByIdQuery query, CancellationToken cancellationToken)
        {
            var task = repository.GetById(query.Id);

            var result = new GetToDoByIdResult(task!);

            return result;
        }
    }
}
