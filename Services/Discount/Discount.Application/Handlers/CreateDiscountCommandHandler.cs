using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Contracts;
using Discount.Core.Entities;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IMapper mapper;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            this.discountRepository = discountRepository;
            this.mapper = mapper;
        }

        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = mapper.Map<Coupon>(request);
            await discountRepository.CreateDiscount(coupon);
            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
