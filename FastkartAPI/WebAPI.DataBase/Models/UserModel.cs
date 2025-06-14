using FastkartAPI.DataBase.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public SexEnum Sex { get; set; } = SexEnum.None;

        public DateOnly DateOnly { get; set; } = new DateOnly();

        public int Phone { get; set; } = 0;

        public string Address { get; set; } = string.Empty;

        public RoleEnum Role { get; set; } = RoleEnum.Buyer;

        public static UserModel CreateModel(UserModel model, string password) 
        {
            return new UserModel()
            {
                Id = Guid.NewGuid(),
                FullName = model.FullName,
                Email = model.Email,
                Password = password,
                Sex = model.Sex,
                DateOnly = model.DateOnly,
                Phone = model.Phone,
                Address = model.Address,
                Role = model.Role
            };
        }
    }
}
