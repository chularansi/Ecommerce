﻿using Catalog.Application.Mappers;
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
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetProductByNameQueryHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProductsByName(request.Name);
            var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);    
            return productResponseList;
        }
    }
}
