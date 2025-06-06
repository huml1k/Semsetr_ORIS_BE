using FastkartAPI.DataBase.Models.Enums;

namespace FastkartAPI.DataBase.Models
{
    public class ItemStore
    {
        public Guid Id { get; set; }

        public string Picture { get; set; }

        public List<TypeItemEnum> TypeItem { get; set; }

        public UnitsEnum UnitEnums { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string SKU { get; set; }

        public DateOnly MFG { get; set; }

        public int Stock { get; set; }

        public ItemStore CreateItem(ItemStore itemStore) 
        {
            return new ItemStore
            {
                Id = Guid.NewGuid(),
                Picture = itemStore.Picture,
                TypeItem = itemStore.TypeItem,
                Name = itemStore.Name,
                Price = itemStore.Price,
                Description = itemStore.Description,
                Weight = itemStore.Weight,
                SKU = itemStore.SKU,
                MFG = itemStore.MFG,
                Stock = itemStore.Stock
            };

        }
    }
}
