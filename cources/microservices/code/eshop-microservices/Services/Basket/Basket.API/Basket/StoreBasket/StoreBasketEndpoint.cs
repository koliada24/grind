namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketEndpoint : ICarterModule
    {
        public record StoreBasketRequest(ShoppingCart Cart);

        public record StoreBasketResponse(string UserName);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (ShoppingCart cart, ISender sender) =>
            {
                var command = new StoreBasketCommand(cart);

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);
            })
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("StoreBasket")
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
        }
    }
}
