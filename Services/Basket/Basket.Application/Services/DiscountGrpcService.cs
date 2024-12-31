using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.Application.Services
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoServiceClient discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoServiceClient discountProtoServiceClient)
        {
            this.discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
