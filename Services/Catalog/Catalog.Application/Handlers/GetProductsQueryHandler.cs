using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Contracts;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProducts(request.CatalogSpecsParams);
            var productsResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            return productsResponseList;
        }
    }
}
