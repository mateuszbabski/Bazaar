using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UserServices
{
    public static class Extensions
    {
        public static IServiceCollection AddCurrentUserProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
