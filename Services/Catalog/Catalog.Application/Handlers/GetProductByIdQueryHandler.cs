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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProduct(request.Id);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(product);   
            return productResponse;
        }
    }
}
