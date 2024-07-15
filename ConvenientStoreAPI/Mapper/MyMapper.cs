using ConvenientStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using ConvenientStoreAPI.Mapper.DTO;

namespace ConvenientStoreAPI.Mapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<OrderRequest, Order>().ReverseMap();
            CreateMap<OrderDetailRequest, Orderdetail>().ReverseMap();
        }
    }
}
