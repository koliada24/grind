
using JasperFx.Events.Daemon;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        List<string> Categories,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

            var product = await session.LoadAsync<Product>(command.Id);

            if (product == null)
            {
                throw new ProductNotFoundException();   
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Categories = command.Categories;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
