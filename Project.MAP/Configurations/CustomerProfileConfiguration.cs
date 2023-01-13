using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class CustomerProfileConfiguration : BaseConfiguration<CustomerProfile>
    {
        public override void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            base.Configure(builder);
        }
    }
}
