using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class MovieFormatConfiguration : BaseConfiguration<MovieFormat>
    {
        public override void Configure(EntityTypeBuilder<MovieFormat> builder)
        {
            base.Configure(builder);
        }
    }
}
