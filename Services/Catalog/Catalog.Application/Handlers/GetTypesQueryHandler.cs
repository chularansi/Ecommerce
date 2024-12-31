using Catalog.Application.Mappers;
using Catalog.Application.Queries;
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
    public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, IList<TypeResponse>>
    {
        private readonly ITypeRepository typeRepository;

        public GetTypesQueryHandler(ITypeRepository typeRepository) 
        {
            this.typeRepository = typeRepository;
        }
        public async Task<IList<TypeResponse>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await typeRepository.GetTypes();
            var typeResponseList = ProductMapper.Mapper.Map<IList<TypeResponse>>(typeList);
            return typeResponseList;
        }
    }
}
