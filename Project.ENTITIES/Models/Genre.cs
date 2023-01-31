using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Genre : BaseEntity
    {
        public string? Name { get; set; } //Tür adı

        //Relational Properties

        public virtual ICollection<Movie> Movies { get; set; }
    }
}