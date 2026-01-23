using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart cannot be null.");
            RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("Shopping cart must contain at least one item.");
        }
    }

    public class StoreBasketCommandHandler
        (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var cart = command.Cart;
            await DeductDiscounts(cart);

            await basketRepository.StoreBasketAsync(cart, cancellationToken);
            
            var result = new StoreBasketResult(cart.UserName);

            return result;
        }

        private async Task DeductDiscounts(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                });

                item.Price = item.Price - coupon.Amount;
            }
        }
    }
}
