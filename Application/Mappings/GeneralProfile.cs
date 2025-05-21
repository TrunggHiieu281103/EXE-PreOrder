using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetBrandById;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProduct;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        // Map query request to paging parameter
        CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        // Map domain Product entity to ViewModel for output
        CreateMap<Products, GetAllProductsViewModel>();
        CreateMap<Products, GetAllProductsViewModel>();
        CreateMap<ProductAssets, ProductAssetViewModel>();
        CreateMap<GetAllProductsQuery, GetAllProductsParameter>();

        // Map domain Brand entity
        CreateMap<CreateBrandCommand, Brands>();
        CreateMap<Brands, GetAllBrandsViewModel>();
        CreateMap<Brands, GetBrandByIdViewModel>();

    }
}

