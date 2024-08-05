using AutoMapper;
using Decomposition.Worker.Dtos;
using Decomposition.Worker.Models;
using System.Text.Json;

namespace Decomposition.Worker.Mappers
{
    public class OrderWriteDtoToOrderModel : Profile
    {
        public OrderWriteDtoToOrderModel()
        {
            CreateMap<OrderWriteDto, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => Convert.ToInt32(src.Id)))
                .ForMember(dest => dest.UserObject, opt => opt.MapFrom(src => SerializeUser(src.UserData)));

            CreateMap<Order, OrderWriteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId.ToString()))
                .ForMember(dest => dest.UserData, opt => opt.MapFrom(src => DeserializeUser(src.UserObject)));
        }
        private string SerializeUser(User user)
        {
            return user != null ? JsonSerializer.Serialize(user) : null;
        }

        private User DeserializeUser(string userObject)
        {
            return !string.IsNullOrEmpty(userObject) ? JsonSerializer.Deserialize<User>(userObject) : null;
        }

    }
}
