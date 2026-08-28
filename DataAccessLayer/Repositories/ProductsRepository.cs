using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.Repositories;

public class ProductsRepository : IProductsRepository
{
    ApplicationDbContext _context;
    public ProductsRepository(ApplicationDbContext context) {
        _context = context;
    }
    public async Task<Product> AddProduct(Product product)
    {
        product.ProductID = Guid.NewGuid();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }


    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? existingProduct = await ((IQueryable<Product>)_context.Products).FirstOrDefaultAsync(
            temp => temp.ProductID == productId);
        if (existingProduct == null) { 
        return false;
        }
            _context.Products.Remove(existingProduct);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _context.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await ((IQueryable<Product>)_context.Products).ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _context.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        // 1. Validate input
        if (product == null)
            throw new ArgumentException("Invalid product data.");

        var existingProduct = await((IQueryable<Product>)_context.Products).FirstOrDefaultAsync(
           temp => temp.ProductID == product.ProductID);
        if (existingProduct == null)
        {
            return null;
        }
        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category = product.Category;
        await _context.SaveChangesAsync();
        return existingProduct;
    }
}
