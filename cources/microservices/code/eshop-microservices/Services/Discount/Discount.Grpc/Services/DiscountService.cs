namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountContext> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
            {
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }

            logger.LogInformation($"Discount is retrieved for ProductName : ${coupon.ProductName}, Amount : {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            var response = coupon.Adapt<CouponModel>();
            return response;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var couponToDelete = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            var list = dbContext.Coupons.ToList();

            if (couponToDelete == null)
            {
                return new DeleteDiscountResponse { Success = false };
            }
            
            dbContext.Coupons.Remove(couponToDelete);
            await dbContext.SaveChangesAsync();
            
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
