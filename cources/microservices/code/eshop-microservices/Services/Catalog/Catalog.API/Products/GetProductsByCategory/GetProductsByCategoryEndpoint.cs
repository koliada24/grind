
namespace Catalog.API.Products.GetProductsByCategory
{
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{category}", async (ISender sender, string category) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));

                return Results.Ok(result.Products);
            })
            .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetProductsByCategory")
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category");
        }
    }
}
