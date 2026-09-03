using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductService.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProducttToProductResponseMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidators>();
        services.AddScoped<IProductService, eCommerce.BusinessLogicLayer.Services.ProductService>();
        return services;
    }
}
