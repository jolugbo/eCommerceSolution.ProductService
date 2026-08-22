using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Entities.RepositoryContracts;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductByCondition(Func<Product, bool> predicate);
    Task<Product> AddProduct(Product product);
    Task<Product?> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid productId);
}
