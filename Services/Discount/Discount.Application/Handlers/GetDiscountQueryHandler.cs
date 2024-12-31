using Discount.Application.Queries;
using Discount.Core.Contracts;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly ILogger<GetDiscountQueryHandler> logger;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository, ILogger<GetDiscountQueryHandler> logger)
        {
            this.discountRepository = discountRepository;
            this.logger = logger;
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await discountRepository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not found"));
            }

            var couponModel = new CouponModel
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName
            };

            logger.LogInformation($"Coupon for the {request.ProductName} is fetched");
            return couponModel;
        }
    }
}
