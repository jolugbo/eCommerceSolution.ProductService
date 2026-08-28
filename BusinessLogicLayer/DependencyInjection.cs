using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.ProductService.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //services.AddSingleton<IProductsRepository, ProductsRepository>();
        //services.AddTransient<ApplicationDbContext>();
        return services;
    }
}
