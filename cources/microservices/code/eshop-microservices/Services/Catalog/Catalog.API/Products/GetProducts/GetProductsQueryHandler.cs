namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery
        : IQuery<GetProductsQueryResult>;

    public record GetProductsQueryResult(IEnumerable<Product> Products);
    

    internal class GetProductsQueryHandler(IDocumentSession session
        , ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
    {
        public async Task<GetProductsQueryResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", request);

            var products = await session.Query<Product>().ToListAsync();

            return new GetProductsQueryResult(products);
        }
    }
}
