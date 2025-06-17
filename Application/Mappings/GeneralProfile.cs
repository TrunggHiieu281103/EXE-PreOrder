using Application.DTOs.Auth;
using Application.DTOs.Order;
using Application.DTOs.Payment;
using Application.DTOs.Shipping;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetBrandById;

using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Queries.GetAllCategory;
using Application.Features.Categories.Queries.GetCategoryById;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Orders.Queries.GetOrderByUserId;
using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Application.Features.ProductAssets.Queries.GetAssetsByProductId;

using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.UserAddress.Queries;
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

        // Map domain Order entity


    CreateMap<Orders, GetAllOrderViewModel>()
    .ForMember(dest => dest.Email,
        opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.CustomerName,
        opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
    .ForMember(dest => dest.Phone,
        opt => opt.MapFrom(src => src.User.Phone))
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src => src.Address)) // Assuming it's src.UserAddress = AddressId
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src =>
            src.Address != null
                ? string.Join(", ", new[]
                {
                    src.Address.AddressDetail,
                    src.Address.Ward,
                    src.Address.District,
                    src.Address.Province
                }.Where(s => !string.IsNullOrWhiteSpace(s)))
                : string.Empty
        ))
    .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
    .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shipping)); 

        CreateMap<Orders, GetOrderByIdViewModel>()
            .ForMember(dest => dest.Email,
        opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.CustomerName,
        opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
    .ForMember(dest => dest.Phone,
        opt => opt.MapFrom(src => src.User.Phone))
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src => src.Address)) // Assuming it's src.UserAddress = AddressId
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src =>
            src.Address != null
                ? string.Join(", ", new[]
                {
                    src.Address.AddressDetail,
                    src.Address.Ward,
                    src.Address.District,
                    src.Address.Province
                }.Where(s => !string.IsNullOrWhiteSpace(s)))
                : string.Empty
        ))
    .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
    .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shipping))
    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderProducts)); 

        CreateMap<Orders, GetOrderByUserIdViewModel>()
            .ForMember(dest => dest.Email,
        opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.CustomerName,
        opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
    .ForMember(dest => dest.Phone,
        opt => opt.MapFrom(src => src.User.Phone))
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src => src.Address)) // Assuming it's src.UserAddress = AddressId
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src =>
            src.Address != null
                ? string.Join(", ", new[]
                {
                    src.Address.AddressDetail,
                    src.Address.Ward,
                    src.Address.District,
                    src.Address.Province
                }.Where(s => !string.IsNullOrWhiteSpace(s)))
                : string.Empty
        ))
    .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
    .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shipping));


        CreateMap<OrderProducts, OrderItemDto>()
    //.ForMember(dest => dest.ProductName,
    //    opt => opt.MapFrom(src => src.Product.ProductName))// Nếu bạn muốn lấy tên sản phẩm
    .ForMember(dest => dest.TotalPrice,
        opt => opt.MapFrom(src => src.Price * src.Quantity));

        CreateMap<OrderItemDto, OrderProducts>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        //Map domain PreOrder entity 
        
            CreateMap<Orders, GetAllPreOrderViewModel>()
    .ForMember(dest => dest.Email,
        opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.CustomerName,
        opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
    .ForMember(dest => dest.Phone,
        opt => opt.MapFrom(src => src.User.Phone))
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src => src.Address)) // Assuming it's src.UserAddress = AddressId
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src =>
            src.Address != null
                ? string.Join(", ", new[]
                {
                    src.Address.AddressDetail,
                    src.Address.Ward,
                    src.Address.District,
                    src.Address.Province
                }.Where(s => !string.IsNullOrWhiteSpace(s)))
                : string.Empty
        ))
    .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
    .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shipping));

        //UserAddress
        CreateMap<UserAddresses, GetAddressByUserIdViewModel>();

        CreateMap<Payments, PaymentDto>();
        CreateMap<Shippings, ShippingDto>();
    }
}