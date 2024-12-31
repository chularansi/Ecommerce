using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Contracts;
using Catalog.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ProductMapper.Mapper.Map<Product>(request);
            if (product == null)
            { 
                throw new ApplicationException("There is an issue with mapping while creating new product");
            }
            var newProduct = await productRepository.CreateProduct(product);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
            return productResponse;
        }
    }
}
