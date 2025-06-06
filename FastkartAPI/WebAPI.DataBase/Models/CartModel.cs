using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Models
{
    public class CartModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }

        // Навигационные свойства
        public UserModel User { get; set; }
        public ItemStore Item { get; set; }

        public int Qty { get; set; } = 1;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        // Добавляем свойства для отображения
        public string ItemName => Item?.Name;
        public decimal ItemPrice => Item?.Price ?? 0;
        public string ItemImage => Item?.Picture;
    }
}
