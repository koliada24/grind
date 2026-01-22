using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
        : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var dbDeletionResult = await repository.DeleteBasketAsync(userName, cancellationToken);

            await cache.RemoveAsync(userName);

            return dbDeletionResult;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var cachedSerializedBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedSerializedBasket))
            {
                var cachedBasket = JsonSerializer.Deserialize<ShoppingCart>(cachedSerializedBasket);
                return cachedBasket!;
            }

            var basket = await repository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await repository.StoreBasketAsync(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
    }
}
