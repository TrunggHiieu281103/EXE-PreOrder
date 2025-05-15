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
        CreateMap<CreateProductCommand, Products >().ReverseMap();
    }
}

