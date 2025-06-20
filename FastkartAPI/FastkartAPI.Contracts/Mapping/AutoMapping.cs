using AutoMapper;
using FastkartAPI.Contracts.Contracts;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Models;

namespace FastkartAPI.Services.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            this.CreateMap<ItemStore, ProductCardDTO>();
            this.CreateMap<ItemStore, CreateProductDTO>();

            this.CreateMap<CreateProductDTO, ItemStore>();

            this.CreateMap<LoginContract, UserModel>();
            this.CreateMap<RegitsterContract, UserModel>();

            this.CreateMap<UpdateOrderStatusDTO, OrderModel>();
        }
    }
}
