using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Runtime.CompilerServices;

namespace eCommerce.ProductMicroService.API.APIEndPoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        //Get  /api/products
        app.MapGet("/api/products", async (IProductService productService) => {
            Console.WriteLine("Hereeeeeeeeee");
            IEnumerable<ProductResponse> products = await productService.GetProducts();
            return Results.Ok(products);
        });

        //Get  /api/products/search/{productId}
        app.MapGet("/api/product/search/product-id/{ProductID:guid}", async (IProductService productService, Guid ProductID) => {
            ProductResponse? product = await productService.GetProductByCondition(temp => temp.ProductID == ProductID);
            return Results.Ok(product);
        });

        //Get  /api/products/search/product-id/{productId}
        app.MapGet("/api/product/search/product-id/{SearchString}", async (IProductService productService, string SearchString) => {
            IEnumerable<ProductResponse?> productsByProductName = await productService.GetProductsByCondition(
                temp => temp.ProductName != null && temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            IEnumerable<ProductResponse?> productsByCategory = await productService.GetProductsByCondition(
                temp => temp.Category != null && temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByProductName.Union(productsByCategory);
            return Results.Ok(products);
        });

        //Post  /api/products
        app.MapPost("/api/products", async (IProductService productService, IValidator<ProductAddRequest> validator,ProductAddRequest newProduct) => {
            ValidationResult validatorResult = await validator.ValidateAsync(newProduct);
            if (!validatorResult.IsValid) {
               Dictionary<string,string[]> errors =  validatorResult.Errors.
                GroupBy(x => x.PropertyName).
                ToDictionary(grp =>grp.Key,
                grp => grp.Select(err =>err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }
            ProductResponse? productAddedResponse = await productService.AddProduct(newProduct);
            if (productAddedResponse!= null)
            {
                return Results.Created(
                    $"/api/products/search/product-id/{productAddedResponse.ProductID}", productAddedResponse
                    );
            }
            else
            {
                return Results.Problem("Error in adding Product");
            }
        });

        //PUT  /api/products
        app.MapPut("/api/products", async (IProductService productService, IValidator<ProductUpdateRequest> validator, ProductUpdateRequest updateProduct) => {
            ValidationResult validatorResult = await validator.ValidateAsync(updateProduct);
            if (!validatorResult.IsValid)
            {
                Dictionary<string, string[]> errors = validatorResult.Errors.
                 GroupBy(x => x.PropertyName).
                 ToDictionary(grp => grp.Key,
                 grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }
            ProductResponse? productUpdateResponse = await productService.UpdateProduct(updateProduct); 
            if (productUpdateResponse != null)
            {
                return Results.Ok( productUpdateResponse);
            }
            else
            {
                return Results.Problem("Error in updating Product");
            }
        });

        //Delete  /api/products/{productId}
        app.MapDelete("/api/products/{ProductId:guid}", async (IProductService productService, Guid ProductID) => {
            bool isDeleted = await productService.DeleteProduct(ProductID);
            if (isDeleted) { return Results.Ok(true); }
            else return Results.Problem("Error in deleting product");

        });
        return app;
    }
}
