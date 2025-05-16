﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
