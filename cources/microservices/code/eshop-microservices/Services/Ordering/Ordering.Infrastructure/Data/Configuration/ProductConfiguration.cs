namespace Ordering.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasConversion(
                productId => productId.Value,
                pId => ProductId.Of(pId));

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
