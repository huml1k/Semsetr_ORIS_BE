using FastkartAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Configurations
{
    public class ItemStoreConfiguration : IEntityTypeConfiguration<ItemStore>
    {
        public void Configure(EntityTypeBuilder<ItemStore> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
