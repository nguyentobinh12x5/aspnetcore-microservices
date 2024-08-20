using System;
using ProductAPI.Models.Dto;

namespace ProductAPI.Service.IService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();
    Task<ProductDto> GetProductById(int productId);

    Task<ProductDto> CreateProduct(ProductDto productDto);

    Task<ProductDto> UpdateProduct(int productId, ProductDto productDto);

    Task<bool> DeleteProduct(int productId);

}
