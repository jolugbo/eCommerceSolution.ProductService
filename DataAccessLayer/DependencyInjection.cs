//using BusinessLogicLayer.Entities.RepositoryContracts;
using DataAccessLayer.DbContext;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace eCommerce.ProductsService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services) { 
        //services.AddSingleton<IProductsRepository, ProductsRepository>();
        services.AddTransient<ApplicationDbContext>();
        return services;
    }
}
