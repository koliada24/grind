namespace Basket.API.Basket.DeleteBasket
{
    public class DeleteBasketEndpoint : ICarterModule
    {
        public record DeleteBasketResponse(bool IsSuccess);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteBasketResponse>();

                return response;
            })
            .WithName("DeleteBasket")
            .WithDescription("Delete Basket")
            .WithSummary("Delete Basket")
            .Produces<DeleteBasketResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
