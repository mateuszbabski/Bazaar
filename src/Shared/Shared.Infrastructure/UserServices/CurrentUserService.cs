﻿using Microsoft.AspNetCore.Http;
using Shared.Abstractions.UserServices;
using System.Security.Claims;

namespace Shared.Infrastructure.UserServices
{
    internal sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public ClaimsPrincipal User => _httpContext.HttpContext?.User;

        public Guid UserId => Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
