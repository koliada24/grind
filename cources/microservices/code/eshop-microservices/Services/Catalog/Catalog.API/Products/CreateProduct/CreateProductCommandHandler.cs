namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Categories,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler
        (IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", request);

            var product = new Product
            {
                Name = request.Name,
                Category = request.Categories,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
