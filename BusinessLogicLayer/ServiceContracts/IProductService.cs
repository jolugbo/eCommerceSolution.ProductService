using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts;

public interface IProductService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProductResponse>> GetProducts();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns></returns>
    Task<IEnumerable<ProductResponse?>> GetProductsByCondition(
        Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionExpression"></param>
    /// <returns></returns>
    Task<ProductResponse?> GetProductByCondition(
        Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productAddRequest"></param>
    /// <returns></returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productUpdateRequest"></param>
    /// <returns></returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<bool> DeleteProduct(Guid productId);
}
