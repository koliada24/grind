namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("A8D42AEB-FAD7-4340-ACAD-45CCA1303730")), "Mehmet", "mehmet@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("6DDA6F1D-25DE-4B8E-8E8B-E0C660B19FC3")), "John", "john@gmail.com")
        };
    }
}
