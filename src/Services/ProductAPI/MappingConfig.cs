using System;
using AutoMapper;
using ProductAPI.Models;
using ProductAPI.Models.Dto;

namespace ProductAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}
