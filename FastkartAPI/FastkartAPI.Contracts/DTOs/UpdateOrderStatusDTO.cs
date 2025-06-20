using FastkartAPI.DataBase.Models.Enums;

namespace FastkartAPI.Contracts.DTOs
{
    public class UpdateOrderStatusDTO
    {
        public Guid OrderId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
