using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandQueryHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetProductByBrandQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var productByBrandList = await productRepository.GetProductsByBrand(request.BrandName);
            var productByBrandResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productByBrandList);
            return productByBrandResponseList;
        }
    }
}
