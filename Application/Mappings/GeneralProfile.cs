using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetBrandById;
using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProduct;
using Application.Features.Products.Queries.GetProductById;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        // Product mapping
        CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        CreateMap<Products, GetAllProductsViewModel>();
        CreateMap<ProductAssets, ProductAssetViewModel>();
        CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        CreateMap<Products, GetProductByIdViewModel>();
        //create 
        CreateMap<CreateProductCommand, Products>();
        // Map domain Brand entity
        CreateMap<CreateBrandCommand, Brands>();
        CreateMap<Brands, GetAllBrandsViewModel>();
        CreateMap<Brands, GetBrandByIdViewModel>();
    }
}

