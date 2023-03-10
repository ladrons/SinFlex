using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class MovieFormat : BaseEntity
    {
        public int MovieID { get; set; }
        public int FormatID { get; set; }

        //Relational Properties

        public virtual Movie Movie { get; set; }
        public virtual Format Format { get; set; }
    }
}
