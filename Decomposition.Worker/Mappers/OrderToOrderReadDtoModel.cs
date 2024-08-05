using AutoMapper;
using Decomposition.Worker.Dtos;
using Decomposition.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Decomposition.Worker.Mappers
{
    public class OrderToOrderReadDtoModel : Profile
    {

        public OrderToOrderReadDtoModel()
        {
            // Map from Order to OrderReadDto
            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.OrderState, opt => opt.MapFrom(src => src.OrderState))
                .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
                .ForMember(dest => dest.TimeCreated, opt => opt.MapFrom(src => src.TimeCreated))
                .ForMember(dest => dest.UserObject, opt => opt.MapFrom(src => DeserializeUser(src.UserObject)));
        }

        private User DeserializeUser(string userObject)
        {
            return !string.IsNullOrEmpty(userObject) ? JsonSerializer.Deserialize<User>(userObject) : null;
        }

    }
}
