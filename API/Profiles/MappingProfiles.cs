using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Role, RoleDto>().ReverseMap();
        //CreateMap<PersonType, TypePDto>().ReverseMap();
        // CreateMap<Pet, FullPetDto>().ReverseMap();
        // CreateMap<Pet, PetStatDto>()
        // .ForMember(e => e.Breed, op => op.MapFrom(e => e.Breed.Name))
        // .ForMember(e => e.Species, op => op.MapFrom(e => e.Species.Name))
        CreateMap<Cart, G_CartDto>()
             .ForMember(e => e.User, op => op.MapFrom(e => e.UserNavigation.Name))
            .ReverseMap();
        
        CreateMap<CartItem, G_CartItemDto>().ReverseMap();
        CreateMap<CartItem, G_CartItemDto>().ReverseMap();
        CreateMap<Category, G_CategoryDto>().ReverseMap();
        CreateMap<Order, G_OrderDto>().ReverseMap();
        CreateMap<OrderItem, G_OrderItemDto>().ReverseMap();
        CreateMap<OrderItem, G_OrderItemDto>().ReverseMap();
        CreateMap<Payment, G_PaymentDto>().ReverseMap();
        CreateMap<PaymentMethod, G_PaymentMethodDto>().ReverseMap();
        CreateMap<Person, G_PersonDto>().ReverseMap();
        CreateMap<Product,G_ProductDto>().ReverseMap();
        CreateMap<Size,G_SizeDto>().ReverseMap();
        CreateMap<SizeHasProduct,G_SizeHasProductDto>().ReverseMap();
        CreateMap<Status,G_StatusDto>().ReverseMap();







        
    }
}