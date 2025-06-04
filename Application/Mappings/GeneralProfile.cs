using Application.DTOs.Auth;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetBrandById;

using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Queries.GetAllCategory;
using Application.Features.Categories.Queries.GetCategoryById;

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
        //User
        CreateMap<Users, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                src.UserRoles != null
                    ? src.UserRoles.Select(ur => ur.Role != null ? ur.Role.RoleName : null).Where(r => r != null).ToList()
                    : new List<string>()
            ));
        // Map domain Category entity
        CreateMap<CreateCategoryCommand, Categories>();
        CreateMap<Categories, GetAllCategoryViewModel>();
        CreateMap<GetAllCategoryQuery, GetAllCategoryParameter>();
        CreateMap<Categories, GetCategoryByIdViewModel>();

    }
}