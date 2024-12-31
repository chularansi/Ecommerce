using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<Pagination<ProductResponse>>
    {
        public CatalogSpecificationsParams CatalogSpecsParams { get; set; }

        public GetProductsQuery(CatalogSpecificationsParams catalogSpecsParams)
        {
            CatalogSpecsParams = catalogSpecsParams;
        }
    }
}
