using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class BoxOfficeConfiguration : BaseConfiguration<BoxOffice>
    {
        public override void Configure(EntityTypeBuilder<BoxOffice> builder)
        {
            base.Configure(builder);
        }
    }
}
