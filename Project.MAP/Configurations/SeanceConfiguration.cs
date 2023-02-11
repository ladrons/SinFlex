using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class SeanceConfiguration : BaseConfiguration<Seance>
    {
        public override void Configure(EntityTypeBuilder<Seance> builder)
        {
            base.Configure(builder);
        }
    }
}
