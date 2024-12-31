using Catalog.Application.Commands;
using Catalog.Core.Contracts;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.DeleteProduct(request.Id);
        }
    }
}
