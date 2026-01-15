using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10)
        : IQuery<GetProductsQueryResult>;

    public record GetProductsQueryResult(IEnumerable<Product> Products);
    

    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
    {
        public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetProductsQueryResult(products);
        }
    }
}
