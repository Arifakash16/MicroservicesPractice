using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository,ILogger<DiscountService> logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            }
            _logger.LogInformation("Discount is retrived for productName: {productName}, Amoount: {amount}", coupon.ProductName, coupon.Amount);
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var isSuccess = await _couponRepository.CreateDiscount(coupon);

            if (isSuccess == true)
            {
                _logger.LogInformation("Discount is successfully created. productName: {productName}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount creation failed");
            }
            
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var isSuccess = await _couponRepository.UpdateDiscount(coupon);

            if (isSuccess == true)
            {
                _logger.LogInformation("Discount is successfully updated. productName: {productName}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount updation failed");
            }

            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool isDeleted = await _couponRepository.DeleteDiscount(request.ProductId);

            if (isDeleted == true)
            {
                _logger.LogInformation("Discount is successfully deleted.");
            }
            else
            {
                _logger.LogInformation("Discount deletion failed");
            }

            return new DeleteDiscountResponse() { Success = isDeleted };

        }
    }
}
