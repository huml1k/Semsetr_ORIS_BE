using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Models
{
    public class WishListModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Связь с пользователем
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        // Связь с товаром
        public Guid ItemId { get; set; }
        public ItemStore Item { get; set; }
    }
}
