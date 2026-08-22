using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Entities.RepositoryContracts;
using DataAccessLayer.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories;

public class ProductsRepository(ApplicationDbContext context) : IProductsRepository
{
    public Task<Product> AddProduct(Product product)
    {
        product.ProductID = Guid.NewGuid();
        //context.Products.Add(product);
        return Task.FromResult(product);
    }

    public Task<bool> DeleteProduct(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductByCondition(Func<Product, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product?> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
