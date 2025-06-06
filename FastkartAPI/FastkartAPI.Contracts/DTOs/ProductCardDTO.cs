using FastkartAPI.DataBase.Models.Enums;

namespace FastkartAPI.Contracts.DTOs
{
    public class ProductCardDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Weight { get; set; }

        public UnitsEnum unitsEnums { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }
    }
}
