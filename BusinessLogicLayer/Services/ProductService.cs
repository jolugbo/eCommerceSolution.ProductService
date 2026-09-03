using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services;

public class ProductService : IProductService
{
    private readonly IValidator<ProductAddRequest> _ProductAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _ProductUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productRepository;
    public ProductService(
        IValidator<ProductAddRequest> ProductAddRequestValidator,
        IValidator<ProductUpdateRequest> ProductUpdateRequestValidator,
        IMapper mapper, IProductsRepository productRepository)
    {
        _ProductAddRequestValidator = ProductAddRequestValidator;
        _ProductUpdateRequestValidator = ProductUpdateRequestValidator;
        _mapper = mapper;
        _productRepository = productRepository;

    }
    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(ProductAddRequest));
        }
        ValidationResult validationResult = await _ProductAddRequestValidator.ValidateAsync(productAddRequest);
        if(!validationResult.IsValid)
        {
            string error = string.Join(", ",validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw  new ArgumentException(error);
        }
        var request = _mapper.Map<Product>(productAddRequest);
        var addedProduct = await _productRepository.AddProduct(request);
        if (addedProduct == null)
        {
            return null;
        }
        var response = _mapper.Map<ProductResponse>(addedProduct);
        return response;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var existingProduct = await _productRepository.GetProductByCondition(temp => temp.ProductID == productId);
        if(existingProduct == null) { return false; }
        var deleted = await _productRepository.DeleteProduct(productId);
        return deleted;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        var product = await _productRepository.GetProductByCondition(conditionExpression);
        if( product == null)
        {
            return null;
        }
        var productResponse = _mapper.Map<ProductResponse>(product);
        return productResponse;
    }

    public async Task<IEnumerable<ProductResponse>> GetProducts()
    {
        var productList = await _productRepository.GetProducts();
        var responseList = _mapper.Map<IEnumerable<ProductResponse>>(productList);
        return responseList;
    }

    public async Task<IEnumerable<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        var products = await _productRepository.GetProductsByCondition(conditionExpression);
        if (products == null)
        {
            return null;
        }
        var productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return productsResponse;
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        if (productUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(ProductUpdateRequest));
        }
        var products = await _productRepository.GetProductsByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);
        if (products == null)
        {
            throw new ArgumentException("Invalid Product ID");
        }
        ValidationResult validationResult = await _ProductUpdateRequestValidator.ValidateAsync(productUpdateRequest);
        if (!validationResult.IsValid)
        {
            string error = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(error);
        }
        var request = _mapper.Map<Product>(productUpdateRequest);
        var updateProduct = await _productRepository.AddProduct(request);
        if (updateProduct == null)
        {
            return null;
        }
        var response = _mapper.Map<ProductResponse>(updateProduct);
        return response;
    }
}
