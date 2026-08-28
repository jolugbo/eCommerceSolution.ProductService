using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContracts;

/// <summary>
/// Represents a repository for managing products table
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Retrieves all products asynchronously
    /// </summary>
    /// <returns>Returns all products from the table</returns>
    Task<IEnumerable<Product>> GetProducts();
    /// <summary>
    /// Retrieves all products based on the specified condition asynchronously 
    /// </summary>
    /// <param name="conditionExpression">The condition to filter products</param>
    /// <returns>Returning a collection of matching products</returns>
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Retrieves a single product based on the specified condition
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns>Returns a single product or null if not found</returns>
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Adds a  new product into the product table async
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Return the added procuct object or null if unsuccessful</returns>
    Task<Product?> AddProduct(Product product);
    /// <summary>
    /// Updates an existing product in the product table asynchronously
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Product?> UpdateProduct(Product product);
    /// <summary>
    /// Deletes a product from the product table based on the specified product ID asynchronously
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<bool> DeleteProduct(Guid productId);
}
