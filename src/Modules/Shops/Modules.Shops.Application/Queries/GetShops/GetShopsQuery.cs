﻿using MediatR;
using Modules.Shops.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShops
{
    public class GetShopsQuery : IRequest<PagedList<ShopDto>>
    {
        #nullable enable
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
