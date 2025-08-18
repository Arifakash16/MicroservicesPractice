using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
        
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponRequest> GetDsicount(string productId)
        {
            var getDiscountData = new GetDiscountRequest() { ProductId = productId };
            return await _discountProtoService.GetDiscountAsync(getDiscountData);
        }
    }
}
