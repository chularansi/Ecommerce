using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Contracts;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IList<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;

        public GetBrandsQueryHandler(IBrandRepository brandRepository) 
        {
            this.brandRepository = brandRepository;
        }
        public async Task<IList<BrandResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await brandRepository.GetBrands();
            var brandResponseList = ProductMapper.Mapper.Map<IList<BrandResponse>>(brandList);
            return brandResponseList;
        }
    }
}
