using FastkartAPI.DataBase.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Models
{
    public class OrderModel
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public string TrackingId { get; set; } = GenearateTackingId();

        public StatusEnum Status { get; set; } = StatusEnum.Pending;

        public decimal TotalPrice { get; set; }

        public UserModel User { get; set; }

        private static string GenearateTackingId()
        {
            return "#" + Guid.NewGuid().ToString().Substring(0, 10);
        }
    }
}
