using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Contracts;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ProductMapper.Mapper.Map<Product>(request);
            if (product == null)
            {
                throw new ApplicationException("There is an issue with mapping while updating the product");
            }

            await productRepository.UpdateProduct(product);
            return true;
        }
    }
}
