﻿using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetTypesQuery : IRequest<IList<TypeResponse>>
    {
    }
}
