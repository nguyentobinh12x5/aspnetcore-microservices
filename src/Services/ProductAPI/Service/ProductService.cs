using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Models.Dto;
using ProductAPI.Service.IService;

namespace ProductAPI.Service;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(AppDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var products = await _db.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(int productId)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProduct(ProductDto productDto)
    {
        try
        {
            var product = _mapper.Map<Product>(productDto);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            if (productDto.Image != null)
            {
                string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
                string filePath = @"wwwroot\ProductImages\" + fileName;

                // Delete existing file if any
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);
                if (file.Exists)
                {
                    file.Delete();
                }

                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    productDto.Image.CopyTo(fileStream);
                }
                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host.Value}{request.PathBase.Value}";
                product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                product.ImageLocalPath = filePath;
            }
            else
            {
                product.ImageUrl = "https://placehold.co/600x400";
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<ProductDto> UpdateProduct(int productId, ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }

}
