using MediatR;
using MediatrDemo1.Data;
using MediatrDemo1.Handlers;

namespace MediatrDemo1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
            });
            builder.Services.AddSingleton<IToDoRepository, ToDoRepository>();
            
            var app = builder.Build();

            app.MapGet("/todo", async (IToDoRepository repository, ISender sender) =>
            {
                var query = new GetAllToDosQuery();

                var result = await sender.Send(query);

                var response = new GetAllToDosResponse(result.Tasks);

                return response;
            });

            app.MapGet("/todo/{id}", async (Guid id, IToDoRepository repository, ISender sender) =>
            {
                var query = new GetToDoByIdQuery(id);

                var result = await sender.Send(query);

                var response = new GetToDoByIdResponse(result.Task);

                return response;
            });

            app.MapPost("/todo", async (AddToDoRequest addToDoRequest, ISender sender) =>
            {
                var command = new AddToDoCommand(addToDoRequest.Title);

                var result = await sender.Send(command);

                var response = new AddToDoResponse(result.Id);

                return response;
            });

            app.MapPut("/todo/{id}", async (Guid id, UpdateToDoRequest updateToDoDto, ISender sender) =>
            {
                var command = new UpdateToDoCommand(id, updateToDoDto.Title);

                var result = await sender.Send(command);

                var response = new UpdateToDoResponse();

                return response;
            });

            app.MapDelete("/todo/{id}", async (Guid id, IToDoRepository repository, ISender sender) =>
            {
                var command = new DeleteToDoCommand(id);

                var result = await sender.Send(command);

                var response = new DeleteToDoResponse();

                return response;
            });

            app.Run();
        }
    }
}
